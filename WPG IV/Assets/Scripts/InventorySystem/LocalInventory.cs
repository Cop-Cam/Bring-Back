using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInventory : MonoBehaviour
{
    private InventoryItemData currentSavedItem;
    public FishItemData currentSavedFish {get; private set;}
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

    //Inserting Item Method, can use method overloader
    public void InsertItem(FishItemData insertedFish)
    {
        currentSavedFish = insertedFish;
    }

    //Removing Item Method, can use method overloader
    public FishItemData RemoveItem()
    {
        FishItemData sendedSavedFish = currentSavedFish;
        currentSavedFish = null;
        return sendedSavedFish;
    }

    void ShowInventoryStatus()
    {
        if(IsInventoryAvailable())
        {
            
        }
    }

    void ShowItemParticle()
    {
        if(IsInventoryAvailable())
        {
            //muncul partikel penuh
        }
        else
        {
            //tidak muncul partikel penuh
        }
    }

    public void ShowInventoryItem()
    {
        if(IsInventoryAvailable())
        {
            
        }
        else if(!IsInventoryAvailable())
        {
            Debug.Log("Inventory kosong!");
        }
    }

    //mengecek kepenuhan inventory
    public bool IsInventoryAvailable()
    {
        if(currentSavedFish != null) //jika ada ikan
        {
            return true;
        }
        else //jika tidak ada ikan
        {
            return false;
        }
    }
}
