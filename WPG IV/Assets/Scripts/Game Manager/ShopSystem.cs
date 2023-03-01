using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Untuk letak jenis-jenis ikan yang bisa dibeli
public class ShopSystem : MonoBehaviour
{
    public static ShopSystem instance { get; private set; }
    LocalInventory localInventory;

    //[SerializeField] private PlayerResourceManager playerResourceManager;
    //public GameObject other; //

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("there is another ShopSystem");
        }
        instance = this;
    }

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

    bool CheckResource(FishItemData shopFish)
    {
        if(PlayerResourceManager.instance.PlayerMoney >= shopFish.fishBuyPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowShopMenu(LocalInventory otherLocalInventory)
    {
        localInventory = otherLocalInventory;
    }

    void BuyFish(FishItemData shopFish)
    {
        if(CheckResource(shopFish)) //berhasil dan memenuhi kriteria membeli
        {
            PlayerResourceManager.instance.DecreaseMoney(shopFish.fishBuyPrice);
            localInventory.InsertFish(shopFish);
        }
        else
        {
            Debug.Log("Uang tidak cukup!");
        }
    }

    //peletakkan tidak jelas nanti
    void SellFish(LocalInventory otherLocalInventory)
    {
        FishItemData soldFish = otherLocalInventory.RemoveFish();
    }
}
