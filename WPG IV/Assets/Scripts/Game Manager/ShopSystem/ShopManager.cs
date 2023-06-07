using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Untuk letak jenis-jenis ikan yang bisa dibeli
public class ShopManager : GenericSingletonClass<ShopManager>, IMenuHandler
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
   
    public bool isShopOpened {get; private set;} = false; 

    public void AddMoneyTemp(int money)
    {
        PlayerResourceManager.Instance.ChangeMoney(money);
    }

    public override void Awake()
    {
        base.Awake();

        UIManager.Instance.RegisterMenu(this, ShopUI);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //ShopUI.SetActive(false);
        currentOpenedInventory = null;
    }

    #region IMenuHandlerImplementation
    public void OpeningMenu()
    {
        OpenShopMenuMethod();
    }
    public void ClosingMenu()
    {
        CloseShopMenuMethod();
    }
    #endregion

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
        currentOpenedInventory = otherLocalInventory; //membuka inventory pada object

        UIManager.Instance.OpenMenu(this);
    }
    public void CloseShopMenu()
    {
        UIManager.Instance.CloseMenu(this);
     
        currentOpenedInventory = null; //inventory yang dibuka dihapus
    }

    private void OpenShopMenuMethod()
    {
        //GameManager.Instance.PauseGame(true);

        if(isShopOpened)
        {
            CloseShopMenu();
            return;
        }

        isShopOpened = true;
        //InputManager.Instance.IsPlayerAllowedToMove(false); //hanya mematikan pergerakkan pemain

        MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();

        // currentOpenedInventory = otherLocalInventory; //membuka inventory pada object

        SettingUpShop(); //Menyeting isi shop

        //ShopUI.SetActive(true);
        
        PlayerResourceManager.OnMoneyChange += RefreshMoney;
    }
    private void CloseShopMenuMethod()
    {
        isShopOpened = false;

        //GameManager.Instance.PauseGame(false);

        //StopAllCoroutines();

        // currentOpenedInventory = null; //inventory yang dibuka dihapus

        ClearingUpShop();
        
        //ShopUI.SetActive(false); //Menutup tab menu pilihan beli atau jual

        //InputManager.Instance.IsPlayerAllowedToMove(true); //pemain boleh bergerak

        PlayerResourceManager.OnMoneyChange -= RefreshMoney;
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
            
            if(soldItem is IDiscoverable discoverable && !discoverable.isItemDiscovered())
            {
                //discoverable.UpdateDiscoveredStatus(true);
                GameDatabase.Instance.DB_FishItems[soldItem.id].UpdateDiscoveredStatus(true);
            }
            
            PlayerResourceManager.Instance.ChangeMoney(soldItem.itemSellPrice);
            
            //delete the instance
            UnityEngine.Object.Destroy(soldItem);
        }
    
        RefreshShopOnClick();
    }
    private void ButtonEventCollectItem()
    {
        if(currentOpenedInventory.IsItemReadyToSellorCollect())
        {
            InventoryItemData collectedItem = currentOpenedInventory.RemoveItem();
            
            if(collectedItem is IDiscoverable discoverable && !discoverable.isItemDiscovered())
            {
                //discoverable.UpdateDiscoveredStatus(true);
                GameDatabase.Instance.DB_FishItems[collectedItem.id].UpdateDiscoveredStatus(true);

            }
            
            QuestSystem.QuestManager.Instance.SendProgressFromQuestManagerToQuest(collectedItem);

            //delete the instance
            UnityEngine.Object.Destroy(collectedItem);
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
        //foreach(KeyValuePair<string, InventoryItemData> listItem in ListShopItem.ListItem)
        foreach(InventoryItemData listItem in ListShopItem.Instance.ListItem)
        {
            var _button = Instantiate(BuyableItemPrefab, BuyGridLayout.transform);

            //testing
            _button.transform.Find("Name").transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = listItem.displayName;

            _button.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = listItem.itemBuyPrice.ToString();
            _button.transform.Find("Icon").GetComponent<Image>().sprite = listItem.icon;
            //BuyableItemPrefab.GetComponent<BuyButtonScript>().SetButtonItemData(listItem.Value);
            _button.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventBuyItem(listItem));


            if(currentOpenedInventory.IsInventoryAvailable())
            {
                _button.GetComponent<ButtonScript>().interactable = true;
            }
            else if(!currentOpenedInventory.IsInventoryAvailable())
            {
                _button.GetComponent<ButtonScript>().interactable = false;
            }

            //Khusus pond inventory 
            //mengecekj adakah pakan pada pond
            if(currentOpenedInventory is PondInventory && listItem is FishFeedItemData)
            {
                PondInventory otherInventory = currentOpenedInventory as PondInventory;
                
                //membatasi agar pemain tidak dapat membeli pakan jika ikan sudah siap untuk dipanen
                if(otherInventory.IsItemReadyToSellorCollect())
                {
                    _button.GetComponent<ButtonScript>().interactable = false;
                    continue;
                }
                
                //membatasi agar pemain tidak dapat membeli pakan jika ikan sudah dipakani
                if(otherInventory.isPondFishFeeded())
                {
                    _button.GetComponent<ButtonScript>().interactable = false;
                }
                else if(!otherInventory.isPondFishFeeded())
                {
                    _button.GetComponent<ButtonScript>().interactable = true;
                }
            }
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
            //_button.transform.Find("Icon").GetComponent<Image>().sprite = currentOpenedInventory.GetCurrentSavedItemData().icon;
            
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
            
            if(currentOpenedInventory.GetCurrentSavedItemData() is FishItemData)
            {
                FishItemData fishItemData = currentOpenedInventory.GetCurrentSavedItemData() as FishItemData;
                _button.transform.Find("Collect").GetComponentInChildren<TextMeshProUGUI>().text = fishItemData.fishPoint.ToString();
            }
            //_button.transform.Find("Icon").GetComponent<Image>().sprite = currentOpenedInventory.GetCurrentSavedItemData().icon;
            
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

    private void RefreshMoney()
    {
        MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
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
