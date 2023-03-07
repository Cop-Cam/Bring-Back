using System.Collections.Generic;
using UnityEngine;

public class LakeInventory : LocalInventory
{
    [Tooltip("Masukkan Semua Jenis Ikan Yang Bisa Muncul Pada Danau Ini")]
    [SerializeField] private InventoryItemData[] InvansiveFishesInThisLake;

    [SerializeField] private int EnergyNeeded = 0;

    protected override void Start() 
    {
        base.Start();
        if(EnergyNeeded == 0)
        {
            EnergyNeeded = 15;
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        ShowInventoryStatus();
        ShowItemParticle();
    }

    public override void OnInteracted()
    {
        if(PlayerResourceManager.Instance.PlayerEnergy-EnergyNeeded >= 0)
        {
            PlayerResourceManager.Instance.DecreaseEnergy(10);
            Debug.Log("panjang arr: "+InvansiveFishesInThisLake.Length);
            int rand = UnityEngine.Random.Range(0, InvansiveFishesInThisLake.Length);
            Debug.Log("rand: "+rand);
            currentSavedItem = InvansiveFishesInThisLake[rand];
            Debug.Log("mendapat: "+InvansiveFishesInThisLake[rand].displayName);
            InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(true);
            //send fiish to player
        }
        else
        {
            Debug.Log("energy tidak cukup");
        }
    }

    // void Init()
    // {
    //     foreach(InventoryItemData item in GameDatabase.Instance.List_InventoryItemData_AllItem.Values)
    //     {
    //         if(item is FishItemData)
    //         {
    //             FishItemData currentFish = item as FishItemData; 
    //             if(currentFish.fishTypes == FishItemData.FishTypes.Invansive)
    //             {
    //                 ListInvansiveFishes.Add(currentFish);
    //             }
    //         }
    //     }
    // }

    //Inserting Item Method, can use method overloader
    public override void InsertItem(InventoryItemData insertedItem)
    {
        currentSavedItem = insertedItem;
    }

    //Remove data
    public override InventoryItemData RemoveItem()
    {
        InventoryItemData sendedSavedItem = currentSavedItem;
        currentSavedItem = null;
        return sendedSavedItem;
    }

    //Menunjukkan status inventory
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
        if(currentSavedItem == null) //jika tidak ada item
        {
            return true;
        }
        else //jika ada item
        {
            return false;
        }
    }

    //mengecek apakah item bisa dijual atau dicollect
    public override bool IsItemReadyToSellorCollect()
    {
        if(IsInventoryAvailable())
        {
            return false;
        }
        else
        {
            if(currentSavedItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override InventoryItemData GetCurrentSavedItemData()
    {
        return currentSavedItem;
    }
}
