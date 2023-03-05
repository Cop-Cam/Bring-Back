using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Untuk letak jenis-jenis ikan yang bisa dibeli
public class ShopSystem : GenericSingletonClass<ShopSystem>
{
    [Tooltip("Masukkan GameObject parent untuk UI")]
    [SerializeField]private GameObject ShopUI;
    
    [SerializeField]private GameObject BuyGridLayout;
    [SerializeField]private GameObject SellGridLayout;

    [Tooltip("Masukkan Prefab Button Untuk Beli")]
    [SerializeField]private GameObject BuyableItemPrefab;

    [Tooltip("Masukkan Prefab Button Untuk Jual")]
    [SerializeField]private GameObject SellableItemPrefab;

    [Tooltip("Masukkan Prefab Button Untuk Collect")]
    [SerializeField]private GameObject CollectableItemPrefab;

    [Tooltip("Masukkan Text untuk Uang")]
    [SerializeField]private TextMeshProUGUI MoneyText;
    
    LocalInventory currentOpenedInventory;

    InventoryItemData currentSO;

   

    // Start is called before the first frame update
    void Start()
    {
        ShopUI.SetActive(false);
    }


    bool CheckResourceMoney(InventoryItemData itemData)
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

        StartCoroutine(RefreshShop());
    }

    public void CloseShopMenu()
    {
        StopCoroutine(RefreshShop());

        currentOpenedInventory = null; //inventory yang dibuka dihapus

        ClearingUpShop();
        
        ShopUI.SetActive(false); //Menutup tab menu pilihan beli atau jual

        InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(true); //pemain boleh bergerak
    }

    
    //Dipasang pada button beli
    public void ButtonEventBuyItem(InventoryItemData itemData)
    {
        if(CheckResourceMoney(itemData))
        {
            PlayerResourceManager.Instance.DecreaseMoney(itemData.itemBuyPrice);
            currentOpenedInventory.InsertItem(itemData);
        }
    }

    //Dipasang pada button jual
    public void ButtonEventSellItem()
    {
        if(currentOpenedInventory.IsItemReadyToSellorCollect())
        {
            InventoryItemData soldItem = currentOpenedInventory.RemoveItem();
            PlayerResourceManager.Instance.IncreaseMoney(soldItem.itemSellPrice);
        }
        else
        {
            Debug.Log("Item is not ready to sell");
        }
    }

    //Dipasang pada button collect
    void CollectItem(LocalInventory otherLocalInventory)
    {
        
    }

    //setting up semua barang yang ada di shop buat beli maupun jual
    void SettingUpShop()
    {
        SetBuyItemInShop();
        SetSellItemInShop();
    }

    void SetBuyItemInShop()
    {
        foreach(KeyValuePair<string, InventoryItemData> listItem in ListShopItem.Instance.ListItem)
        {
            Debug.Log("listItem.Value: "+listItem.Value.name);

            BuyableItemPrefab.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = listItem.Value.itemBuyPrice.ToString();
            BuyableItemPrefab.transform.Find("Icon").GetComponent<Image>().sprite = listItem.Value.icon;
            BuyableItemPrefab.GetComponent<BuyButtonScript>().SetButtonItemData(listItem.Value);

            if(currentOpenedInventory.IsInventoryAvailable())
            {
                BuyableItemPrefab.GetComponent<Button>().interactable = true;
            }
            else if(!currentOpenedInventory.IsInventoryAvailable())
            {
                BuyableItemPrefab.GetComponent<Button>().interactable = false;
            }
            Instantiate(BuyableItemPrefab, BuyGridLayout.transform);
        }
    }

    void SetSellItemInShop()
    {
        if(currentOpenedInventory.IsInventoryAvailable())
        {
            Debug.Log("invetory kososng");
            //SellableItemPrefab.GetComponent<Button>().interactable = false;
        }
        else if(!currentOpenedInventory.IsInventoryAvailable())
        {
            Debug.Log("invetory breisi");

            SellableItemPrefab.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = currentOpenedInventory.GetCurrentSavedItemData().itemBuyPrice.ToString();
            SellableItemPrefab.transform.Find("Icon").GetComponent<Image>().sprite = currentOpenedInventory.GetCurrentSavedItemData().icon;
            SellableItemPrefab.GetComponent<SellButtonScript>().SetButtonItemData(currentOpenedInventory.GetCurrentSavedItemData());

            //SellableItemPrefab.GetComponent<Button>().interactable = true;
        }

        Instantiate(SellableItemPrefab, SellGridLayout.transform);
    }

    void ClearingUpShop()
    {
        ClearingUpBuyGrid();
        ClearingUpSellGrid();
    }

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

    // public InventoryItemData GetCurrentItem()
    // {
    //     return currentSO;
    // }

    IEnumerator RefreshShop()
    {
        while(true)
        {
            //Menghentikan error ketika CloseShop()
            if(currentOpenedInventory==null || PlayerResourceManager.Instance==null)
            {
                Debug.Log("break");
                yield break;
            }

            MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
            
            //Mengecek kepenuhan inventory
            if(!currentOpenedInventory.IsInventoryAvailable()) //Jika inventory berisi
            {
                if(currentOpenedInventory.IsItemReadyToSellorCollect()) //jika siap di jual / collect
                {
                    // foreach(Transform item in CollectGridLayout.GetComponentInChildren<Transform>())
                    // {
                    //     item.GetComponent<Button>().interactable = true;
                    // }

                    foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
                    {
                        item.GetComponent<Button>().interactable = true;
                    }
                }
                else if(!currentOpenedInventory.IsItemReadyToSellorCollect()) //jika tidak siap di jual / collect
                {
                    // foreach(Transform item in CollectGridLayout.GetComponentInChildren<Transform>())
                    // {
                    //     item.GetComponent<Button>().interactable = false;
                    // }

                    foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
                    {
                        item.GetComponent<Button>().interactable = false;
                    }
                }

                foreach(Transform item in BuyGridLayout.GetComponentInChildren<Transform>())
                {
                    item.GetComponent<Button>().interactable = false;
                }

            }
            else if(currentOpenedInventory.IsInventoryAvailable()) //Jika inventory kosong
            {
                foreach(Transform item in BuyGridLayout.GetComponentInChildren<Transform>())
                {
                    item.GetComponent<Button>().interactable = true;
                }

                foreach(Transform item in SellGridLayout.GetComponentInChildren<Transform>())
                {
                    item.GetComponent<Button>().interactable = false;
                }
            }
            yield return null;
        }
    }
}
