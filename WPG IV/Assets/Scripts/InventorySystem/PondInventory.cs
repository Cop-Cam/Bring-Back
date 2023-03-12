using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PondInventory : LocalInventory
{
    protected FishItemData currentSavedFish;
    protected FishFeedItemData currentSavedFeed;
    private int FishDaysToMatureDecrement;

    protected override void Start()
    {
        base.Start();
        currentSavedFish = null;
        currentSavedFeed = null;
        FishDaysToMatureDecrement = 0;
        SetInteractIcon();
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
            StartCoroutine(FishMaturingMethod());
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
        if(IsInventoryAvailable())
        {
            return false;
        }
        else
        {
            return CheckFishMature();
        }
    }

    public override InventoryItemData GetCurrentSavedItemData()
    {
        return currentSavedFish;
    }

    public bool CheckFishMature()
    {
        if(currentSavedFish.daysToMatured <= 0)
        {
            Debug.Log("fish is matured");
            currentSavedFish.isFishMatured = true;
            return true;
        }
        else
        { 
            return false;
        }
    }

    private IEnumerator FishMaturingMethod()
    {
        Debug.Log("start maturing fish");
        while(true)
        {
            if(CheckFishMature())
            {
                yield break;
            }

            if(TimeManager.Instance.daychanged)
            {
                //Harus dieksekusi hari berikutnya
                if(!CheckFishMature() && isPondFishFeeded())
                {
                    currentSavedFish.daysToMatured -= FishDaysToMatureDecrement;
                    currentSavedFeed = null;
                    Debug.Log("berhasil");
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
