using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ListShopItem : GenericSingletonClass<ListShopItem>
{
    public Dictionary<string, InventoryItemData> ListItem;
    // Start is called before the first frame update
    void Start()
    {
        ListItem = new Dictionary<string, InventoryItemData>();
        while(true)
        {
            if(GameDatabase.Instance.isGameDatabaseReady)
            {
                SettingUpDictionary();
                break;
            }
            Debug.Log("not ready");
        }
    }

    // public void Init()
    // {
    //     ListItem = new Dictionary<string, InventoryItemData>();
    //     SettingUpDictionary();
    // }

    // //mengambil data SO fish pada folder yang ditentukan
    // void GetAllFishes(List<InventoryItemData> fishItemList)
    // {
    //     string[] assetNames = AssetDatabase.FindAssets("Fish", new[]{"Assets/ScriptableObjects/Fishes/FishesItem"});
    //     foreach(string SOName in assetNames)
    //     {
    //         var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //         var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
    //         fishItemList.Add(character);
    //     }
    // }

    // void GetAllFishesSeed(List<InventoryItemData> fishSeedList)
    // {
    //     string[] assetNames = AssetDatabase.FindAssets("FishSeed", new[]{"Assets/ScriptableObjects/Fishes/FishesSeed"});
    //     foreach(string SOName in assetNames)
    //     {
    //         var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //         var character = AssetDatabase.LoadAssetAtPath<FishSeedItemData>(SOpath);
    //         fishSeedList.Add(character);
    //     }
    // }

    // void GetAllFishesFeed(List<InventoryItemData> fishFeedList)
    // {
    //     string[] assetNames = AssetDatabase.FindAssets("FishFeed", new[]{"Assets/ScriptableObjects/Fishes/FishesFeed"});
    //     foreach(string SOName in assetNames)
    //     {
    //         var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //         var character = AssetDatabase.LoadAssetAtPath<FishFeedItemData>(SOpath);
    //         fishFeedList.Add(character);
    //     }
    // }

    void AssignItemToDictionary(List<InventoryItemData> itemList)
    {
        foreach(var itemData in itemList)
        {
            ListItem.Add(itemData.id, itemData);
        }
    }

    void SettingUpListShopFromGameDatabaseDictionary(List<InventoryItemData> itemList)
    {
        foreach(InventoryItemData item in GameDatabase.Instance.List_InventoryItemData_AllItem.Values)
        {
            if(item is FishFeedItemData)
            {
                itemList.Add(item);
            }
            if(item is FishSeedItemData)
            {
                itemList.Add(item);
            }
        }
    }

    void SettingUpDictionary()
    {
        List<InventoryItemData> itemList = new List<InventoryItemData>();

        SettingUpListShopFromGameDatabaseDictionary(itemList);
        //GetAllFishes(itemList);
        //GetAllFishesSeed(itemList);
        //GetAllFishesFeed(itemList);

        if(itemList != null)
        {
            AssignItemToDictionary(itemList);
        }
        else if(itemList == null)
        {
            Debug.Log("There Is No Item Master!");
        }
    }

    public InventoryItemData GetItemFromDictionary(string key)
    {
        return ListItem[key];
    }
}
