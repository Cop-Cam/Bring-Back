using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private InventoryItemData currentSavedItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowItemStatus();
        ShowItemParticle();
    }

    void InsertItem(InventoryItemData insertedItem)
    {
        currentSavedItem = insertedItem;
    }

    void ShowItemStatus()
    {
        if(currentSavedItem != null)
        {
            
        }
    }

    void ShowItemParticle()
    {
        if(currentSavedItem != null)
        {
            //muncul partikel penuh
        }
        else
        {
            //tidak muncul partikel penuh
        }
    }
}
