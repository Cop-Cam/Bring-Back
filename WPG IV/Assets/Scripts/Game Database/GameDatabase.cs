using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameDatabase : GenericSingletonClass<GameDatabase>
{
    public Dictionary<string, InventoryItemData> List_InventoryItemData_AllItem{get; private set;}
    public bool isGameDatabaseReady{get; private set;}

    [Header("Sprite")]
    public Sprite InteractIconDefault_Sprite;
    public Color InteractIconActivatedDefault_Color;
    public Color InteractIconDeactivatedDefault_Color;
    // public Dictionary<string, InventoryItemData> List_InventoryItemData_EndemicFish;
    // public Dictionary<string, InventoryItemData> List_InventoryItemData_InvansiveFish;
    // public Dictionary<string, InventoryItemData> List_InventoryItemData_FishSeed;

    //Sementara disini karna bingung harus dimana anjing
    //public Dictionary<string, InventoryItemData> ListInvansiveFishes{get; private set;}


    public override void Awake()
    {
        base.Awake();
        List_InventoryItemData_AllItem = new Dictionary<string, InventoryItemData>();
        SettingUpBaseDictionary();
        isGameDatabaseReady = true;
    }


    // void Start()
    // {
    //     foreach(InventoryItemData item in List_InventoryItemData_AllItem.Values)
    //     {
    //         if(item is FishItemData)
    //         {
    //             FishItemData currentFish = item as FishItemData; 
    //             if(currentFish.fishTypes == FishItemData.FishTypes.Invansive)
    //             {
    //                 ListInvansiveFishes.Add(currentFish.id, currentFish);
    //             }
    //         }
    //     }
    // }

    // Start is called before the first frame update
    // void Start()
    // {
    //     List_InventoryItemData_AllItem = new Dictionary<string, InventoryItemData>();
    //     SettingUpBaseDictionary();

    //     // List_InventoryItemData_EndemicFish = new Dictionary<string, InventoryItemData>();
    //     // List_InventoryItemData_InvansiveFish = new Dictionary<string, InventoryItemData>();
    //     // List_InventoryItemData_FishSeed = new Dictionary<string, InventoryItemData>();
    //     // SettingUpFishDictionaryFromBaseDictionary();

    //     //ListShopItem.Instance.Init();
    //     isGameDatabaseReady = true;
    // }

    //mengambil data SO fish pada folder yang ditentukan
    void GetAllFishes(List<InventoryItemData> fishItemList)
    {
        string[] assetNamesEndemic = AssetDatabase.FindAssets("Fish", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Endemic"});
        foreach(string SOName in assetNamesEndemic)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character);
        }

        string[] assetNamesInvansive = AssetDatabase.FindAssets("Fish", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Invansive"});
        foreach(string SOName in assetNamesInvansive)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character);
        }
    }
    void GetAllFishesSeed(List<InventoryItemData> fishSeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("FishSeed", new[]{"Assets/ScriptableObjects/Fishes/FishesSeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishSeedItemData>(SOpath);
            fishSeedList.Add(character);
        }
    }
    void GetAllFishesFeed(List<InventoryItemData> fishFeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("FishFeed", new[]{"Assets/ScriptableObjects/Fishes/FishesFeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishFeedItemData>(SOpath);
            fishFeedList.Add(character);
        }
    }


    void AssignItemToDictionary(List<InventoryItemData> itemList, Dictionary<string, InventoryItemData> thisDict)
    {
        foreach(var itemData in itemList)
        {
            thisDict.Add(itemData.id, itemData);
        }
    }

    void SettingUpBaseDictionary()
    {
        List<InventoryItemData> itemList = new List<InventoryItemData>();

        GetAllFishes(itemList);
        GetAllFishesSeed(itemList);
        GetAllFishesFeed(itemList);

        if(itemList != null)
        {
            AssignItemToDictionary(itemList, List_InventoryItemData_AllItem);
        }
        else if(itemList == null)
        {
            Debug.Log("There Is No Item Master!");
        }
    }

    // void SettingUpFishDictionaryFromBaseDictionary()
    // {
    //     foreach(InventoryItemData item in List_InventoryItemData_AllItem.Values)
    //     {
    //         if(item is FishItemData)
    //         {
    //             FishItemData currentData = item as FishItemData;
    //             if(currentData.fishTypes == FishItemData.FishTypes.Endemic)
    //             {
    //                 List_InventoryItemData_EndemicFish.Add(item.id, item);
    //             }
    //             else if(currentData.fishTypes == FishItemData.FishTypes.Invansive)
    //             {
    //                 List_InventoryItemData_InvansiveFish.Add(item.id, item);
    //             }
    //         }
    //         if(item is FishSeedItemData)
    //         {
    //             List_InventoryItemData_FishSeed.Add(item.id, item);
    //         }
    //     }
    // }
}
