
using UnityEngine;


[CreateAssetMenu(fileName = "FishSeedItemData", menuName = "Scriptable Objects/FishRelated/FishSeedItemData", order = 2)]
public class FishSeedItemData : InventoryItemData 
{
    [SerializeField] private FishItemData FishData;
    

    public FishItemData SendFishDataFromSeed()
    {
        return Instantiate(FishData);
    }
}



