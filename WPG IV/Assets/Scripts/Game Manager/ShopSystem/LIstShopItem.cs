using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ListShopItem : GenericSingletonClass<ListShopItem>
{
    public List<InventoryItemData> ListItem {get; private set;}
    //public static Dictionary<string, InventoryItemData> ListItem;
    // Start is called before the first frame update
    
    public override void Awake() 
    {
        base.Awake();
        //ListItem = new Dictionary<string, InventoryItemData>();
        ListItem = new List<InventoryItemData>();
    }
    private void Start()
    {
        SettingUpListShopFromGameDatabaseDictionary(ListItem);

        // ListItem = new Dictionary<string, InventoryItemData>();
        // while(true)
        // {
        //     if(GameDatabase.isGameDatabaseReady)
        //     {
        //         SettingUpDictionary();
        //         break;
        //     }
        // }


        // List<ScriptableObject> tempList = new List<ScriptableObject>();

        // AddressablesManager.Instance.LoadAllAssetsByKey("shopitem", tempList);

        // List<InventoryItemData> sortedTempList = new List<InventoryItemData>();
        
        // SortItemFromAddressable(tempList, sortedTempList);

        // SettingUpList(sortedTempList);

    }

    // private void AssignItemToShopableItem(List<InventoryItemData> itemList)
    // {
    //     foreach(var itemData in itemList)
    //     {
    //         ListItem.Add(itemData);
    //     }
    // }

    private void SettingUpListShopFromGameDatabaseDictionary(List<InventoryItemData> itemList)
    {
        foreach(FishFeedItemData feed in GameDatabase.Instance.DB_FishFeeds.Values)
        {
            ListItem.Add(feed as InventoryItemData);
        }

        foreach(FishSeedItemData seed in GameDatabase.Instance.DB_FishSeeds.Values)
        {
            ListItem.Add(seed as InventoryItemData);
        }
        
        if(itemList == null || itemList.Count == 0)
        {
            Debug.LogWarning("There Is No Item Master!");
        }
        
        
        // foreach(InventoryItemData item in itemList)
        // {
        //     if(item is FishFeedItemData)
        //     {
        //         itemList.Add(item);
        //     }
        //     if(item is FishSeedItemData)
        //     {
        //         itemList.Add(item);
        //     }
        // }
    }
    /*
    private void SettingUpList(List<InventoryItemData> sortedList)
    {
        //List<InventoryItemData> itemList = new List<InventoryItemData>();

        SettingUpListShopFromGameDatabaseDictionary(sortedList);
        //GetAllFishes(itemList);
        //GetAllFishesSeed(itemList);
        //GetAllFishesFeed(itemList);

        if(sortedList != null)
        {
            AssignItemToShopableItem(sortedList);
        }
        else if(sortedList == null)
        {
            Debug.LogWarning("There Is No Item Master!");
        }
    }

    // public InventoryItemData GetItemFromDictionary(string key)
    // {
    //     return ListItem[key];
    // }

    private void SortItemFromAddressable(List<ScriptableObject> list, List<InventoryItemData> sortedList)
    {
        foreach (ScriptableObject SO in list)
        {
            if(SO is not InventoryItemData)
            {
                list.Remove(SO);
            }
            else
            {
                sortedList.Add(SO as InventoryItemData);
            }
        }

        list = null;
    }
    */
}
