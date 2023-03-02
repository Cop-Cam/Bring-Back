using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Untuk letak jenis-jenis ikan yang bisa dibeli
public class ShopSystem : GenericSingletonClass<ShopSystem>
{
    [SerializeField]private GameObject ShopUI;
    [SerializeField]private Button SellButton;
    [SerializeField]private Button BuyButton;
    [SerializeField]private Button CollectButton;
    
    //public static ShopSystem instance { get; private set; }
    LocalInventory currentOpenedInventory;

    //[SerializeField] private PlayerResourceManager playerResourceManager;
    //public GameObject other; //

    // void Awake()
    // {
    //     if(instance != null)
    //     {
    //         Debug.Log("there is another ShopSystem");
    //     }
    //     instance = this;

    //     ShopUI.SetActive(false);
    // }

    // Start is called before the first frame update
    void Start()
    {
        //playerResourceManager = other.GetComponent<PlayerResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(PlayerResourceManager.instance.PlayerMoney < 1000)
        // {
        //     PlayerResourceManager.instance.IncreaseMoney(100);
        // }
    }

    bool CheckResourceMoney(FishItemData shopFish)
    {
        if(PlayerResourceManager.Instance.PlayerMoney >= shopFish.fishBuyPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // bool CheckFishMaturity(FishItemData shopFish)
    // {
    //     if(shopFish.isMatured)
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    public void OpenShopMenu(LocalInventory otherLocalInventory)
    {
        currentOpenedInventory = otherLocalInventory;
        //buka tab menu pilihan beli atau jual
        ShopUI.SetActive(true);

        if(currentOpenedInventory == null)
        {
            Debug.Log("tidak ada inventory");
            return;
        }
        else if(currentOpenedInventory != null)
        {
            Debug.Log("ada inventory");
        }

        //Mengecek kepenuhan kolam
        if(!currentOpenedInventory.IsInventoryAvailable()) //Jika tidak ada ikan di kolam
        {
            CollectButton.interactable = false;
            SellButton.interactable = false;
            BuyButton.interactable = true;
        }
        else if(currentOpenedInventory.IsInventoryAvailable()) //Jika ada ikan di kolam
        {
            BuyButton.interactable = false;
            //Mengecek kematangan ikan
            if(!currentOpenedInventory.currentSavedFish.isMatured)
            {
                CollectButton.interactable = false;
                SellButton.interactable = false;
            }
            else if(currentOpenedInventory.currentSavedFish.isMatured)
            {
                CollectButton.interactable = true;
                SellButton.interactable = true;
            }
        }
    }

    public void CloseShopMenu()
    {
        currentOpenedInventory = null; //inventory yang dibuka dihapus

        ShopUI.SetActive(false); //Menutup tab menu pilihan beli atau jual

        //GameManager.instance.IsPlayerAllowedToMove(true); //pemain boleh bergerak
        GameManager.Instance.IsPlayerAllowedToMove(true); //pemain boleh bergerak
    }

    // public void ChooseShopMenu(int indexChoose)
    // {
    //     if (indexChoose == 0)
    //     {
    //         BuyShopMenu(currentOpenedInventory);
    //     }
    //     else if (indexChoose == 1)
    //     {
    //         SellShopMenu(currentOpenedInventory);
    //     }
    // }

    // public void BuyShopMenu(LocalInventory otherLocalInventory)
    // {
    //     if(otherLocalInventory.IsInventoryAvailable())
    //     {

    //     }
    //     else if (!otherLocalInventory.IsInventoryAvailable())
    //     {

    //     }
    // }

    // public void SellShopMenu(LocalInventory otherLocalInventory)
    // {
    //     otherLocalInventory.ShowInventoryItem();
    // }
    
    //Dipasang pada button beli
    void BuyFish(FishItemData shopFish)
    {
        if(CheckResourceMoney(shopFish)) //berhasil dan memenuhi kriteria membeli
        {
            PlayerResourceManager.Instance.DecreaseMoney(shopFish.fishBuyPrice);
            currentOpenedInventory.InsertItem(shopFish);
        }
        else
        {
            Debug.Log("Uang tidak cukup!");
        }
    }

    //Dipasang pada button jual
    void SellFish()
    {
        //if(CheckFishMaturity(otherLocalInventory.currentSavedFish))
        if(currentOpenedInventory.currentSavedFish.isMatured)
        {
            FishItemData soldFish = currentOpenedInventory.RemoveItem();
            PlayerResourceManager.Instance.IncreaseMoney(soldFish.fishSellPrice);
        }
        else if(!currentOpenedInventory.currentSavedFish.isMatured)
        {
            Debug.Log("Ikan belum matang!");
        }
    }

    //Dipasang pada button collect
    void CollectFish(LocalInventory otherLocalInventory)
    {
        
    }
}
