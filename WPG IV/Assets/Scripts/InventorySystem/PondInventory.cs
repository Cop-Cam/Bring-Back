using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PondInventory : LocalInventory
{
    protected FishItemData currentSavedFish;
    protected FishFeedItemData currentSavedFeed;
    private int FishDaysToMatureDecrement;

    void OnEnable()
    {
        TimeManager.Instance.OnDayChanged += DayHasChanged;
    }
    void OnDisable()
    {
        TimeManager.Instance.OnDayChanged -= DayHasChanged;
    }
    void DayHasChanged()
    {
        //Check if there is any fish in pond
        if(!IsInventoryAvailable())
        {
            FishMaturingMethod();
        }
    }

    protected override void Start()
    {
        base.Start();
        currentSavedFish = null;
        currentSavedFeed = null;
        FishDaysToMatureDecrement = 0;
    }
    // Update is called once per frame
    protected override void Update()
    {
        ShowInventoryStatus();
        ShowItemParticle();
    }

    public override void OnInteracted()
    {
        ShopSystem.Instance.OpenShopMenu(this); 
    }

    public override void InsertItem(InventoryItemData insertedItem)
    {
        base.InsertItem(insertedItem);
        if(currentSavedItem is FishSeedItemData)
        {
            Debug.Log("beli ikan");
            FishSeedItemData currentSavedFishSeed = insertedItem as FishSeedItemData;
            currentSavedFish = ConvertSeedToFish(currentSavedFishSeed);
        }
        else if(currentSavedItem is FishFeedItemData)
        {
            Debug.Log("beli pakan");
            currentSavedFeed = insertedItem as FishFeedItemData;
            FishDaysToMatureDecrement = currentSavedFeed.FishFeedEffectiveness;
            //StartCoroutine(FishMaturingMethod());
        }
        currentSavedItem = null;
    }

    
    //Konversi SO seed ikan menjadi SO ikan
    FishItemData ConvertSeedToFish(FishSeedItemData currentSavedFishSeed)
    {
        return currentSavedFishSeed.SendFishDataFromSeed();
    }

    
    //Remove data
    public override InventoryItemData RemoveItem()
    {
        InventoryItemData sendedSavedItem = currentSavedFish as InventoryItemData;
        currentSavedFish = null;
        return sendedSavedItem;
    }

    //Menunjukkan Status Inventory
    protected override void ShowInventoryStatus()
    {
        if(IsInventoryAvailable())
        {
            
        }
    }
    protected override void ShowItemParticle()
    {
        if(IsInventoryAvailable())
        {
            //muncul partikel penuh
        }
        else
        {
            //tidak muncul partikel penuh
        }
    }

    //idk
    public override void ShowInventoryItem()
    {
        if(IsInventoryAvailable())
        {
            
        }
        else if(!IsInventoryAvailable())
        {
            Debug.Log("Inventory kosong!");
        }
    }

    //mengecek kepenuhan inventory
    public override bool IsInventoryAvailable()
    {
        if(currentSavedFish == null) //jika tidak ada ikan
        {
            return true;
        }
        else //jika ada ikan
        {
            return false;
        }
    }

    public bool isPondFishFeeded()
    {
        if(currentSavedFeed == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsItemReadyToSellorCollect()
    {
        //If inventory is empty
        if(IsInventoryAvailable())
        {
            return false;
        }
        else
        {
            //Send wether fish is matured or not
            return IsFishMatured();
        }
    }

    public override InventoryItemData GetCurrentSavedItemData()
    {
        return currentSavedFish;
    }

    public bool IsFishMatured()
    {
        if(currentSavedFish.daysToMatured <= 0)
        {
            Debug.Log("fish is matured");
            //currentSavedFish.isFishMatured = true;
            return true;
        }
        else
        { 
            return false;
        }
    }

    void FishMaturingMethod()
    {
        Debug.Log("start maturing fish");

        if(IsFishMatured())
        {
            Debug.Log("Fish is matured");
        }
        else if(!IsFishMatured() && isPondFishFeeded()) //Harus dieksekusi hari berikutnya
        {
            currentSavedFish.daysToMatured -= FishDaysToMatureDecrement;
            currentSavedFeed = null;
            Debug.Log("berhasil");
        }
        
    }
}
