
using UnityEngine;


[CreateAssetMenu(fileName = "FishSeedItemData", menuName = "SO/FishRelated/FishSeedItemData", order = 2)]
public class FishSeedItemData : InventoryItemData 
{
    [SerializeField] private FishItemData FishData;
    

    public FishItemData SendFishDataFromSeed()
    {
        return FishData;
    }
}



