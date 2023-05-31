using System;
using UnityEngine;


public class PlayerResourceManager : GenericSingletonClass<PlayerResourceManager>
{
    //public static PlayerResourceManager instance { get; private set; }
    
    //resource money
    public int PlayerMoney { get; private set; }

    //resource energy
    public int PlayerEnergy { get; private set; }
    public int PlayerMaxEnergy { get; private set; }

    //currently collected item data
    //[SerializeField] private InventoryItemData PlayerSavedInventoryItem;
    
    public static event Action OnMoneyChange;
    public static event Action OnEnergyChange;


    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMoney(0);
        PlayerMaxEnergy = 100;
        ChangeEnergy(PlayerMaxEnergy);
    }


    public void ChangeMoney(int MoneyChange)
    {
        PlayerMoney += MoneyChange;
        OnMoneyChange();
    }
    public void ChangeEnergy(int EnergyChange)
    {
        PlayerEnergy += EnergyChange;
        
        if(PlayerEnergy >= PlayerMaxEnergy)
        {
            PlayerEnergy = PlayerMaxEnergy;
        }

        OnEnergyChange();
    }
    public void ChangeMaxEnergy(int EnergyChange)
    {
        PlayerMaxEnergy += EnergyChange;
    }
    public void ResetEnergy()
    {
        PlayerEnergy = PlayerMaxEnergy;
    }

    // public void IncreaseEnergy(float EnergyChange)
    // {
    //     PlayerEnergy += EnergyChange;
    // }
    // public void DecreaseEnergy(float EnergyChange)
    // {
    //     PlayerEnergy -= EnergyChange;
    // }
    /*
    public void SetSavedItemInInventory(InventoryItemData sendedInventoryItemData)
    {
        PlayerSavedInventoryItem = sendedInventoryItemData;
    }

    public void ConvertSavedInventoryDataToObjective()
    {
        if(PlayerSavedInventoryItem is FishItemData)
        {
            SortFishToObjective(PlayerSavedInventoryItem as FishItemData);
        } 
    }

    private void SortFishToObjective(FishItemData fish)
    {
        // if(fish.fishTypes == FishItemData.FishTypes.Endemic)
        // {
        //     //logic quest 
        // }

        // if(fish.fishTypes == FishItemData.FishTypes.Endemic)
        // {
        //     //logic quest 
        // }
    }
    */
}
