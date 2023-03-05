using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FishFeedItemData", menuName = "SO/FishFeedItemData", order = 3)]
public class FishFeedItemData : InventoryItemData 
{
    public int FishFeedEffectiveness {get; private set;}
}

