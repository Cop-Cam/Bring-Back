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



