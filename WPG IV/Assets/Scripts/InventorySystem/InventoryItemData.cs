using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "SO/InventoryItemData", order = 0)]
public class InventoryItemData : ScriptableObject 
{
    public string id;
    public string displayName;
    public Sprite icon; 
    public string itemDescription;
    public int itemBuyPrice;
    public int itemSellPrice;
}



