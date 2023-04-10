using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Untuk letak jenis-jenis ikan yang bisa dibeli
public class ShopManager : GenericSingletonClass<ShopManager>
{
    [Tooltip("Masukkan GameObject parent untuk UI")]
    public GameObject ShopUI;
    
    [SerializeField]private GameObject BuyGridLayout;
    [SerializeField]private GameObject SellGridLayout;
    [SerializeField]private GameObject CollectGridLayout;

    [Tooltip("Masukkan Prefab Button Untuk Beli")]
    [SerializeField]private GameObject BuyableItemPrefab;

    [Tooltip("Masukkan Prefab Button Untuk Jual")]
    [SerializeField]private GameObject SellableItemPrefab;

    [Tooltip("Masukkan Prefab Button Untuk Collect")]
    [SerializeField]private GameObject CollectableItemPrefab;

    [Tooltip("Masukkan Text untuk Uang")]
    [SerializeField]private TextMeshProUGUI MoneyText;
    
    LocalInventory currentOpenedInventory;
   

    // Start is called before the first frame update
    private void Start()
    {
        ShopUI.SetActive(false);
        currentOpenedInventory = null;
        //UIManager.Instance.AddUiObjToList(ShopUI);
        
        UIManager.Instance.AddGameObjectToDictionary(transform.parent.gameObject);
    }

    private bool CheckResourceMoney(InventoryItemData itemData)
    {
        if(PlayerResourceManager.Instance.PlayerMoney >= itemData.itemBuyPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OpenShopMenu(LocalInventory otherLocalInventory)
    {
        InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false); //pemain tidak boleh bergerak

        MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();

        currentOpenedInventory = otherLocalInventory; //membuka inventory pada object

        SettingUpShop(); //Menyeting isi shop

        ShopUI.SetActive(true);
        //UIManager.Instance.ShopUI.SetActive(true);


        //StartCoroutine(RefreshShop());
        StartCoroutine(RefreshMoney());
    }
    public void CloseShopMenu()
    {
        //StopCoroutine(RefreshShop());
        StopAllCoroutines();

        currentOpenedInventory = null; //inventory yang dibuka dihapus

        ClearingUpShop();
        
        ShopUI.SetActive(false); //Menutup tab menu pilihan beli atau jual
        //UIManager.Instance.ShopUI.SetActive(false);

        InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(true); //pemain boleh bergerak
    }

    
    #region Button Events For Shop        
    
    private void ButtonEventBuyItem(InventoryItemData itemData)
    {
        if(CheckResourceMoney(itemData))
        {
            //PlayerResourceManager.Instance.DecreaseMoney(itemData.itemBuyPrice);
            PlayerResourceManager.Instance.ChangeMoney(-(itemData.itemBuyPrice));
            currentOpenedInventory.InsertItem(itemData);
        }
        RefreshShopOnClick();
    }
    private void ButtonEventSellItem()
    {
        if(currentOpenedInventory.IsItemReadyToSellorCollect())
        {
            InventoryItemData soldItem = currentOpenedInventory.RemoveItem();
            PlayerResourceManager.Instance.ChangeMoney(soldItem.itemSellPrice);
        }
        else
        {
            Debug.Log("Item is not ready to sell");
        }
        RefreshShopOnClick();
    }
    private void ButtonEventCollectItem()
    {
        if(currentOpenedInventory.IsItemReadyToSellorCollect())
        {
            InventoryItemData collectedItem = currentOpenedInventory.RemoveItem();

            //send collected item to playerinventory
            //PlayerResourceManager.Instance.SetSavedItemInInventory(collectedItem);
            QuestSystem.QuestManager.Instance.SendProgressFromQuestManagerToQuest(collectedItem);
        }
        else
        {
            Debug.Log("Item is not ready to collect");
        }
        RefreshShopOnClick();
    }

    #endregion


    //setting up semua barang yang ada di shop buat beli maupun jual
    private void SettingUpShop()
    {
        SetBuyItemInShop();
        SetSellItemInShop();
        SetCollectItemInShop();
    }

    #region SettingUpItemInShopMethods

    private void SetBuyItemInShop()
    {
        foreach(KeyValuePair<string, InventoryItemData> listItem in ListShopItem.Instance.ListItem)
        {
            var _button = Instantiate(BuyableItemPrefab, BuyGridLayout.transform);

            _button.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = listItem.Value.itemBuyPrice.ToString();
            _button.transform.Find("Icon").GetComponent<Image>().sprite = listItem.Value.icon;
            //BuyableItemPrefab.GetComponent<BuyButtonScript>().SetButtonItemData(listItem.Value);
            _button.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventBuyItem(listItem.Value));


            if(currentOpenedInventory.IsInventoryAvailable())
            {
                _button.GetComponent<ButtonScript>().interactable = true;
            }
            else if(!currentOpenedInventory.IsInventoryAvailable())
            {
                _button.GetComponent<ButtonScript>().interactable = false;
            }

            //Khusus pond inventory //mengecekj adakah pakan pada pond
            if(currentOpenedInventory is PondInventory && listItem.Value is FishFeedItemData)
            {
                PondInventory otherInventory = currentOpenedInventory as PondInventory;
                if(otherInventory.isPondFishFeeded())
                {
                    _button.GetComponent<ButtonScript>().interactable = false;
                }
                else if(!otherInventory.isPondFishFeeded())
                {
                    _button.GetComponent<ButtonScript>().interactable = true;
                }
            }
            
            // Instantiate(BuyableItemPrefab, BuyGridLayout.transform);
        }
    }

    private void SetSellItemInShop()
    {
        var _button = Instantiate(SellableItemPrefab, SellGridLayout.transform);

        if(currentOpenedInventory.IsInventoryAvailable())
        {
            _button.GetComponent<ButtonScript>().interactable = false;   
        }
        else if(!currentOpenedInventory.IsInventoryAvailable())
        {
            _button.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = currentOpenedInventory.GetCurrentSavedItemData().itemSellPrice.ToString();
            _button.transform.Find("Icon").GetComponent<Image>().sprite = currentOpenedInventory.GetCurrentSavedItemData().icon;
            //SellableItemPrefab.GetComponent<SellButtonScript>().SetButtonItemData(currentOpenedInventory.GetCurrentSavedItemData());
            _button.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventSellItem());

            
            if(currentOpenedInventory.IsItemReadyToSellorCollect())
            {
                _button.GetComponent<ButtonScript>().interactable = true;
            }
            else if(!currentOpenedInventory.IsItemReadyToSellorCollect())
            {
                _button.GetComponent<ButtonScript>().interactable = false;
            }
        }

        // Instantiate(SellableItemPrefab, SellGridLayout.transform);
    }

    private void SetCollectItemInShop()
    {
        var _button = Instantiate(CollectableItemPrefab, CollectGridLayout.transform);
        if(currentOpenedInventory.IsInventoryAvailable())
        {
            _button.GetComponent<ButtonScript>().interactable = false;   
        }
        else if(!currentOpenedInventory.IsInventoryAvailable())
        {
            _button.transform.Find("Icon").GetComponent<Image>().sprite = currentOpenedInventory.GetCurrentSavedItemData().icon;
            //CollectableItemPrefab.GetComponent<CollectButtonScript>().SetButtonItemData(currentOpenedInventory.GetCurrentSavedItemData());
            _button.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventCollectItem());

            
            if(currentOpenedInventory.IsItemReadyToSellorCollect())
            {
                _button.GetComponent<ButtonScript>().interactable = true;
            }
            else if(!currentOpenedInventory.IsItemReadyToSellorCollect())
            {
                _button.GetComponent<ButtonScript>().interactable = false;
            }
        }

        //Instantiate(CollectableItemPrefab, CollectGridLayout.transform);
    }

    #endregion

    void RefreshShopOnClick()
    {
        ClearingUpShop();
        SettingUpShop();
    }

    void ClearingUpShop()
    {
        ClearingUpBuyGrid();
        ClearingUpSellGrid();
        ClearingUpCollectGrid();
    }

    #region ClearingUpItemInShopMethods
    void ClearingUpBuyGrid()
    {
        foreach(Transform BuyableItemPrefab in BuyGridLayout.transform)
        {
            GameObject.Destroy(BuyableItemPrefab.gameObject);
        }
    }

    void ClearingUpSellGrid()
    {
        foreach(Transform SellableItemPrefab in SellGridLayout.transform)
        {
            GameObject.Destroy(SellableItemPrefab.gameObject);
        }
    }

    void ClearingUpCollectGrid()
    {
        foreach(Transform CollectableItemPrefab in CollectGridLayout.transform)
        {
            GameObject.Destroy(CollectableItemPrefab.gameObject);
        }
    }
    #endregion

    private IEnumerator RefreshMoney()
    {
        while(true)
        {
            MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
            yield return new WaitForEndOfFrame();
        }
    }

    // IEnumerator RefreshShop()
    // {
    //     while(true)
    //     { 
    //         //Menghentikan error ketika CloseShop()
    //         if(currentOpenedInventory==null || PlayerResourceManager.Instance==null)
    //         {
    //             Debug.Log("break");
    //             yield break;
    //         }

    //         MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
            
    //         //Mengecek kepenuhan inventory
    //         if(!currentOpenedInventory.IsInventoryAvailable()) //Jika inventory berisi
    //         {
    //             if(currentOpenedInventory.IsItemReadyToSellorCollect()) //jika siap di jual / collect
    //             {
    //                 ClearingUpSellGrid();
    //                 SetSellItemInShop();

    //                 foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
    //                 {
    //                     item.GetComponent<Button>().interactable = true;
    //                 }
    //             }
    //             else if(!currentOpenedInventory.IsItemReadyToSellorCollect()) //jika tidak siap di jual / collect
    //             {
    //                 ClearingUpSellGrid();
    //                 SetSellItemInShop();

    //                 foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
    //                 {
    //                     item.GetComponent<Button>().interactable = false;
    //                 }
    //             }

    //             foreach(Transform item in BuyGridLayout.GetComponentInChildren<Transform>())
    //             {
    //                 item.GetComponent<Button>().interactable = false;
    //             }

    //         }
    //         else if(currentOpenedInventory.IsInventoryAvailable()) //Jika inventory kosong
    //         {
    //             foreach(Transform item in BuyGridLayout.GetComponentInChildren<Transform>())
    //             {
    //                 item.GetComponent<Button>().interactable = true;
    //             }

    //             foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
    //             {
    //                 ClearingUpSellGrid();
    //                 SetSellItemInShop();
    //                 item.GetComponent<Button>().interactable = false;
    //             }
    //         }
    //         yield return null;
    //     }
    // }
}
