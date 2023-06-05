using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LakeInventory : LocalInventory
{
    [Tooltip("Masukkan Semua Jenis Ikan Yang Bisa Muncul Pada Danau Ini")]
    [SerializeField] private InventoryItemData[] InvansiveFishesInThisLake; //template for what fish will be included in runtime
    private List<InventoryItemData> InstantiatedInvansiveFishesInThisLake; //runtime fish
    [SerializeField] private int EnergyNeeded = 10;

    protected override void Start()
    {
        base.Start();

        if(InvansiveFishesInThisLake == null || InvansiveFishesInThisLake.Length == 0) 
        {
            Debug.LogWarning("There is no fish in this lake!");
            return;
        }

        //Basically, SO will save everything that happen in runtime to SO asset
        //To getting around this, I made fish to get the id because using "is" will not work
        InstantiatedInvansiveFishesInThisLake = new List<InventoryItemData>();
        foreach(InventoryItemData fishItem in GameDatabase.Instance.DB_FishItems.Values)
        {
            if (InvansiveFishesInThisLake.Any(fish => fish.id == fishItem.id))
            {
                //Debug.LogWarning("fish added");
                InstantiatedInvansiveFishesInThisLake.Add(fishItem);
            }


            // foreach(InventoryItemData fish in InvansiveFishesInThisLake)
            // {
            //     if(fish.id == fishItem.id)
            //     {
            //         InstantiatedInvansiveFishesInThisLake.Add(fishItem as InventoryItemData);
            //     }
            // }
        }
    }

    public override void OnInteracted()
    {
        if(InvansiveFishesInThisLake == null || InvansiveFishesInThisLake.Length == 0) 
        {
            //Debug.LogWarning("There is no fish in this lake!");
            return;
        }

        if(PlayerResourceManager.Instance.PlayerEnergy - EnergyNeeded >= 0)
        {
            // InputManager.Instance.IsPlayerAllowedToMove(false);
            InputManager.Instance.IsPlayerAllowedToInteract(false);

            PlayerResourceManager.Instance.ChangeEnergy(-(EnergyNeeded));
            //Debug.Log("panjang arr: "+InvansiveFishesInThisLake.Length);
            int rand = UnityEngine.Random.Range(0, InstantiatedInvansiveFishesInThisLake.Count);
            Debug.Log("rand: "+rand);
            Debug.Log("size: "+InstantiatedInvansiveFishesInThisLake.Count);
            currentSavedItem = InstantiatedInvansiveFishesInThisLake[rand];

            LakeUIController.Instance.OpenLakeUI(this);
        }
        else
        {
            Debug.Log("energy tidak cukup");
            // InputManager.Instance.IsPlayerAllowedToInteract(true);
            // InputManager.Instance.IsPlayerAllowedToMove(true);
        }
    }

}
