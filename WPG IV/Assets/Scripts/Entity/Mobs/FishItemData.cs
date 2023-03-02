using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishItemData", menuName = "WPG IV/FishItemData", order = 1)]
public class FishItemData : InventoryItemData 
{
    public enum FishTypes {Endemic, Invasif};
    public FishTypes fishTypes; //tipe ikan (endemic atau invansif)
    public int daysToFeed;
    public int daysToMatured; //lamanya ikan bertumbuh
    private float hoursToMatured; //berbasis timesOfDay pada TimeManager
    public int fishBuyPrice; //harga ikan dibeli
    public int fishSellPrice; //harga ikan dijual;
    public bool isActivated;
    public bool isMatured = false;
    
    //mungkin seharusnya di ensiklopedia
    //public bool isUnlocked = false; //mengecek apakah yang ditangkap adalah ikan baru //
    void Awake()
    {
        hoursToMatured = daysToMatured * 24;
    }

    void Update() 
    {
        FishMaturingMethod();
    }

    public void InitializeFish()
    {
        isActivated = true;
    }

    void FishMaturingMethod()
    {
        hoursToMatured -= TimeManager.Instance.CalculateTimeOfDay();
    }
}

