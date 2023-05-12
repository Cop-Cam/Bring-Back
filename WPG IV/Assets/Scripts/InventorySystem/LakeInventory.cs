using System.Collections.Generic;
using UnityEngine;
using System;

public class LakeInventory : LocalInventory
{
    [Tooltip("Masukkan Semua Jenis Ikan Yang Bisa Muncul Pada Danau Ini")]
    [SerializeField] private InventoryItemData[] InvansiveFishesInThisLake;

    [SerializeField] private int EnergyNeeded = 10;

    /*
    protected override void Start() 
    {
        base.Start();
        
    }
    */

    public override void OnInteracted()
    {
        if(InvansiveFishesInThisLake == null || InvansiveFishesInThisLake.Length == 0) 
        {
            Debug.LogWarning("There is no fish in this lake!");
            return;
        }

        if(PlayerResourceManager.Instance.PlayerEnergy-EnergyNeeded >= 0)
        {
            InputManager.Instance.IsPlayerAllowedToMove(false);
            InputManager.Instance.IsPlayerAllowedToInteract(false);


            PlayerResourceManager.Instance.ChangeEnergy(-(EnergyNeeded));
            //Debug.Log("panjang arr: "+InvansiveFishesInThisLake.Length);
            int rand = UnityEngine.Random.Range(0, InvansiveFishesInThisLake.Length);
            //Debug.Log("rand: "+rand);
            currentSavedItem = InvansiveFishesInThisLake[rand];

            LakeUIController.Instance.OpenLakeUI(this);

            //InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false);
        }
        else
        {
            Debug.Log("energy tidak cukup");
            InputManager.Instance.IsPlayerAllowedToInteract(true);
            InputManager.Instance.IsPlayerAllowedToMove(true);
        }
    }

    
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
