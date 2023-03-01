using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "WPG IV/InventoryItemData", order = 0)]
public class InventoryItemData : ScriptableObject 
{
    public string id;
    public string displayName;
    public Sprite icon; 
    public string itemDescription;
    
}

[CreateAssetMenu(fileName = "FishItemData", menuName = "WPG IV/FishItemData", order = 1)]
public class FishItemData : InventoryItemData 
{
    public enum FishTypes {Endemic, Invasif};
    public FishTypes fishTypes; //tipe ikan (endemic atau invansif)
    public int daysToFeed;
    public int daysToMatured; //lamanya ikan bertumbuh
    public int fishBuyPrice; //harga ikan dibeli
    public int fishSellPrice; //harga ikan dijua;

}

