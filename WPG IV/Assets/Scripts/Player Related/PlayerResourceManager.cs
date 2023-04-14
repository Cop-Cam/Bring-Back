using System;
using UnityEngine;


public class PlayerResourceManager : GenericSingletonClass<PlayerResourceManager>
{
    //public static PlayerResourceManager instance { get; private set; }
    
    //resource money
    public int PlayerMoney { get; private set; }

    //resource energy
    public float PlayerEnergy { get; private set; }

    //currently collected item data
    //[SerializeField] private InventoryItemData PlayerSavedInventoryItem;
    
    public static event Action OnMoneyChange;

    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMoney = 0;
        PlayerEnergy = 100;
    }

    //Digunakan di class lain yang membutuhkan
    // public void IncreaseMoney(int MoneyChange)
    // {
    //     PlayerMoney += MoneyChange;
    // }
    // public void DecreaseMoney(int MoneyChange)
    // {
    //     PlayerMoney -= MoneyChange;
    // }

    public void ChangeMoney(int MoneyChange)
    {
        PlayerMoney += MoneyChange;
        OnMoneyChange();
    }
    public void ChangeEnergy(float EnergyChange)
    {
        PlayerEnergy += EnergyChange;
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
