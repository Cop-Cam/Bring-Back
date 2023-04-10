using System;
using UnityEngine;

public class LocalInventory : InteractableObjects
{
    protected InventoryItemData currentSavedItem;

    protected override void Start()
    {
        base.Start();
        currentSavedItem = null;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        ShowInventoryStatus();
        ShowItemParticle();
    }

    public override void OnInteracted()
    {
        throw new NotImplementedException();
    }

    //Inserting Item Method, can use method overloader
    public virtual void InsertItem(InventoryItemData insertedItem)
    {
        currentSavedItem = insertedItem;
    }

    //Remove data
    public virtual InventoryItemData RemoveItem()
    {
        InventoryItemData sendedSavedItem = currentSavedItem;
        currentSavedItem = null;
        return sendedSavedItem;
    }

    //Menunjukkan status inventory
    protected virtual void ShowInventoryStatus()
    {
        if(IsInventoryAvailable())
        {
            
        }
    }
    protected virtual void ShowItemParticle()
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

    public virtual void ShowInventoryItem()
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
    public virtual bool IsInventoryAvailable()
    {
        if(currentSavedItem == null) //jika tidak ada item
        {
            return true;
        }
        else //jika ada item
        {
            return false;
        }
    }

    //mengecek apakah item bisa dijual atau dicollect
    public virtual bool IsItemReadyToSellorCollect()
    {
        if(IsInventoryAvailable())
        {
            return false;
        }
        else
        {
            if(currentSavedItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual InventoryItemData GetCurrentSavedItemData()
    {
        return currentSavedItem;
    }
}
