using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FishFeedItemData", menuName = "SO/FishFeedItemData", order = 3)]
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

