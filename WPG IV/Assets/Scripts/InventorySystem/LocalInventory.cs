using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInventory : MonoBehaviour
{
    private InventoryItemData currentSavedItem;
    private FishItemData currentSavedFish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowInventoryStatus();
        ShowItemParticle();
    }

    // public void InsertItem(InventoryItemData insertedItem)
    // {
    //     currentSavedItem = insertedItem;
    // }

    // public InventoryItemData RemoveItem()
    // {
    //     InventoryItemData sendedSavedItem = currentSavedItem;
    //     currentSavedItem = null;
    //     return sendedSavedItem;
    // }

    public void InsertFish(FishItemData insertedFish)
    {
        currentSavedFish = insertedFish;
    }

    public FishItemData RemoveFish()
    {
        FishItemData sendedSavedFish = currentSavedFish;
        currentSavedFish = null;
        return sendedSavedFish;
    }

    void ShowInventoryStatus()
    {
        if(currentSavedFish != null)
        {
            
        }
    }

    void ShowItemParticle()
    {
        if(currentSavedFish != null)
        {
            //muncul partikel penuh
        }
        else
        {
            //tidak muncul partikel penuh
        }
    }
}
