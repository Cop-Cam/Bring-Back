using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PondInventory : LocalInventory
{
    [SerializeField] protected FishItemData currentSavedFish;
    protected FishFeedItemData currentSavedFeed;
    [SerializeField] private int currentSavedFishDaysBeforeMatured;

    [Tooltip("Model yang digunakan untuk keadaan kosong")]
    [SerializeField] private GameObject OnEmptyModel;
    
    [Tooltip("Model yang digunakan untuk keadaan penuh")]
    [SerializeField] private GameObject OnFullModel;

    [Tooltip("Model Default yang digunakan saat ini")]
    [SerializeField] private GameObject DefaultModel;

    
    private void ChangeModel(GameObject otherModel)
    {
        //if(DefaultModel != otherModel) Destroy(DefaultModel);
        if(DefaultModel == otherModel) return;

        LayerMask temp = DefaultModel.layer;

        Destroy(DefaultModel);

        DefaultModel = Instantiate(otherModel, transform.parent.position, transform.parent.rotation, transform.parent);

        DefaultModel.layer = temp;
    }

    void OnEnable()
    {   
        TimeManager.OnDayChanged += DayHasChanged;
    }

    void OnDisable()
    {
        TimeManager.OnDayChanged -= DayHasChanged;
    }
    
    void DayHasChanged()
    {
        //Check if there is any fish in pond
        if(!IsInventoryAvailable())
        {
            FishMaturingMethod();
        }
    }

    protected override void Start()
    {
        base.Start();
        currentSavedFish = null;
        currentSavedFeed = null;

        //for changing model 
        IsInventoryAvailable();
    }
    
    public override void OnInteracted()
    {
        ShopManager.Instance.OpenShopMenu(this); 
    }

    public override void InsertItem(InventoryItemData insertedItem)
    {
        //time is used for inserting fish
        TimeManager.Instance.IncrementHour(1);

        base.InsertItem(insertedItem);
        if(currentSavedItem is FishSeedItemData)
        {
            //Debug.Log("beli ikan");
            FishSeedItemData currentSavedFishSeed = insertedItem as FishSeedItemData;
            currentSavedFish = ConvertSeedToFish(currentSavedFishSeed);

            currentSavedFishDaysBeforeMatured = currentSavedFish.daysToMatured;
        }
        else if(currentSavedItem is FishFeedItemData)
        {
            //Debug.Log("beli pakan");
            currentSavedFeed = insertedItem as FishFeedItemData;
            //StartCoroutine(FishMaturingMethod());
        }
        currentSavedItem = null;

        IsInventoryAvailable();
    }

    //Konversi SO seed ikan menjadi SO ikan
    FishItemData ConvertSeedToFish(FishSeedItemData currentSavedFishSeed)
    {
        return currentSavedFishSeed.SendFishDataFromSeed();
    }
    
    //Remove data
    public override InventoryItemData RemoveItem()
    {
        InventoryItemData sendedSavedItem = currentSavedFish as InventoryItemData;
        currentSavedFish = null;
        IsInventoryAvailable();
        return sendedSavedItem;
    }

    //mengecek kepenuhan inventory
    public override bool IsInventoryAvailable()
    {
        if(currentSavedFish == null) //jika tidak ada ikan
        {
            ChangeModel(OnEmptyModel);
  
            return true;
        }
        else //jika ada ikan
        {
            ChangeModel(OnFullModel);

            return false;
        }
    }

    public bool isPondFishFeeded()
    {
        if(currentSavedFeed == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsItemReadyToSellorCollect()
    {
        //If inventory is empty
        if(IsInventoryAvailable())
        {
            return false;
        }
        else
        {
            //Send wether fish is matured or not
            return IsFishMatured();
        }
    }

    public override InventoryItemData GetCurrentSavedItemData()
    {
        return currentSavedFish;
    }

    public bool IsFishMatured()
    {
        //if(currentSavedFish.daysToMatured <= 0)
        if(currentSavedFishDaysBeforeMatured <= 0)
        {
            Debug.Log("fish is matured");
            //currentSavedFish.isFishMatured = true;
            return true;
        }
        else
        { 
            return false;
        }
    }

    private void FishMaturingMethod()
    {
        //Debug.Log("start maturing fish");

        // if(IsFishMatured())
        // {
        //     Debug.Log("Fish is matured");
        //     return;
        // }

        if(!IsFishMatured() && isPondFishFeeded()) //Harus dieksekusi hari berikutnya
        {
            //currentSavedFish.daysToMatured -= FishDaysToMatureDecrement;
            currentSavedFishDaysBeforeMatured -= currentSavedFeed.FishFeedEffectiveness;
           
            currentSavedFeed = null;

            Debug.Log("berhasil");
        }
        
    }
}
