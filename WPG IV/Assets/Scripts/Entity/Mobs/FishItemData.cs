
using UnityEngine;

[CreateAssetMenu(fileName = "FishItemData", menuName = "Scriptable Objects/FishRelated/FishItemData", order = 1)]
public class FishItemData : InventoryItemData, IDiscoverable
{
    public enum FishTypes {Endemic, Invansive};
    //public Sprite fishPicture { get; private set; }//Foto ikan dewasa
    public FishTypes fishTypes; //tipe ikan (endemic atau invansif)
    private bool isFishDiscovered;
    public int daysToMatured; //lamanya ikan bertumbuh
    //public bool isFishMatured { get; set; }
    public int fishPoint; //objective point
    
    public bool isItemDiscovered()
    {
        return isFishDiscovered;
    }

    public void UpdateDiscoveredStatus(bool status)
    {
        isFishDiscovered = status;
    }

    public bool GetFishDiscoveredStatus()
    {
        return isFishDiscovered;
    }

    
}

