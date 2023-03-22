using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FishFeedItemData", menuName = "Scriptable Objects/FishRelated/FishFeedItemData", order = 0)]
public class FishFeedItemData : InventoryItemData 
{
    public int FishFeedEffectiveness = 0;

    void Start() 
    {
        if(FishFeedEffectiveness == 0)
        {
            FishFeedEffectiveness = 1;
        }
    }
}

