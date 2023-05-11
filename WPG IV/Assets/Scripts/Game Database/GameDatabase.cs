using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameDatabase : GenericSingletonClass<GameDatabase>
{

    [SerializeField] private List<InventoryItemData> itemList; //= new List<InventoryItemData>();

    //public Dictionary<string, InventoryItemData> DB_InventoryItems{get; private set;}


    public Dictionary<string, FishItemData> DB_FishItems{get; private set;}
    public Dictionary<string, FishSeedItemData> DB_FishSeeds{get; private set;}
    public Dictionary<string, FishFeedItemData> DB_FishFeeds{get; private set;}

    //public static bool isGameDatabaseReady{get; private set;}

    [Header("Sprite")]
    public Sprite InteractIconDefault_Sprite;
    public Color InteractIconActivatedDefault_Color;
    public Color InteractIconDeactivatedDefault_Color;


    public override void Awake()
    {
        base.Awake();
        //DB_InventoryItems = new Dictionary<string, InventoryItemData>();
        //SettingUpDatabaseDictionary();
        //isGameDatabaseReady = true;
    }

    /*
    private void SettingUpDatabaseDictionary()
    {
        List<FishItemData> fishTempList = new List<FishItemData>();
        //StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey("shopitems", tempList));
        StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey<FishItemData>("FishItem", fishTempList, typeof(FishItemData)));
        foreach(FishItemData fish in fishTempList)
        {
            DB_FishItems.Add(fish.id, fish);
        }

        List<FishSeedItemData> seedTempList = new List<FishSeedItemData>();
        //StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey("shopitems", tempList));
        StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey<FishSeedItemData>("FishSeed", seedTempList, typeof(FishSeedItemData)));
        foreach(FishSeedItemData seed in seedTempList)
        {
            DB_FishSeeds.Add(seed.id, seed);
        }

        List<FishFeedItemData> feedTempList = new List<FishFeedItemData>();
        //StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey("shopitems", tempList));
        StartCoroutine(AddressablesManager.Instance.LoadAllAssetsByKey<FishFeedItemData>("FishFeed", feedTempList, typeof(FishFeedItemData)));

        foreach(FishFeedItemData feed in feedTempList)
        {
            DB_FishFeeds.Add(feed.id, feed);
        }

        //SortItemFromAddressable(tempList);

        
        // GetAllFishes(itemList);
        // GetAllFishesSeed(itemList);
        // GetAllFishesFeed(itemList);

        // if(itemList != null)
        // {
        //     AssignItemToDictionary(itemList, List_InventoryItemData_AllItem);
        // }
        // else if(itemList == null)
        // {
        //     Debug.Log("There Is No Item Master!");
        // }
    }
    */


    private void SortItemFromAddressable(List<InventoryItemData> list)
    {
        DB_FishItems = new Dictionary<string, FishItemData>();
        DB_FishSeeds = new Dictionary<string, FishSeedItemData>();
        DB_FishFeeds = new Dictionary<string, FishFeedItemData>();

        foreach (InventoryItemData SO in list)
        {
            if(SO is not InventoryItemData)
            {
                continue;
            }

            if(SO is FishFeedItemData)
            {
                FishFeedItemData temp = SO as FishFeedItemData;
                DB_FishFeeds.Add(temp.id, temp);
            }
            else if(SO is FishItemData)
            {
                FishItemData temp = SO as FishItemData;
                DB_FishItems.Add(temp.id, temp);
            }
            else if(SO is FishSeedItemData)
            {
                FishSeedItemData temp = SO as FishSeedItemData;
                DB_FishSeeds.Add(temp.id, temp);
            }
            
        }

        //list = null;
    }

    

    // private void AssignItemToDictionary(List<InventoryItemData> itemList, Dictionary<string, InventoryItemData> thisDict)
    // {
    //     foreach(var itemData in itemList)
    //     {
    //         thisDict.Add(itemData.id, itemData);
    //     }
    // }


    #if UNITY_EDITOR

    void OnValidate()
    {
        itemList = new List<InventoryItemData>();
        GetAllFishes(itemList);
        GetAllFishesFeed(itemList);
        GetAllFishesSeed(itemList);

        if(itemList == null || itemList.Count == 0)
        {
            Debug.LogWarning("templist is empty");
            return;
        }
        SortItemFromAddressable(itemList);
    }

    #endif

    //mengambil data SO fish pada folder yang ditentukan
   
    void GetAllFishes(List<InventoryItemData> fishItemList)
    {
        string[] assetNamesEndemic = AssetDatabase.FindAssets("t:FishItemData t:FishItemData.fishTypes.Endemic", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Endemic"});
        foreach(string SOName in assetNamesEndemic)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character);
        }

        string[] assetNamesInvansive = AssetDatabase.FindAssets("t:FishItemData t:FishItemData.fishTypes.Invansive", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Invansive"});
        foreach(string SOName in assetNamesInvansive)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character);
        }
    }
    void GetAllFishesSeed(List<InventoryItemData> fishSeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("t:FishSeedItemData", new[]{"Assets/ScriptableObjects/Fishes/FishesSeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishSeedItemData>(SOpath);
            fishSeedList.Add(character);
        }
    }
    void GetAllFishesFeed(List<InventoryItemData> fishFeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("t:FishFeedItemData", new[]{"Assets/ScriptableObjects/Fishes/FishesFeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishFeedItemData>(SOpath);
            fishFeedList.Add(character);
        }
    }
    
    /*
    
     public Dictionary<string, Quest> QuestDictionary {get; private set;}
        public List<Quest> CurrentActivatedQuestList;// {get; private set;}
        public List<Quest> CurrentFailedQuestList; //{get; private set;}
        public List<Quest> CurrentCompletedQuestList; //{get; private set;}

        public override void Awake()
        {
            base.Awake();
            QuestDictionary = new Dictionary<string, Quest>();
            
            SettingUpQuestlineDictionary();

            CurrentActivatedQuestList = new List<Quest>();
            CurrentFailedQuestList = new List<Quest>();
            CurrentCompletedQuestList = new List<Quest>();
        }

        private void Start() 
        {
            UIManager.Instance.AddGameObjectToDictionary(transform.parent.gameObject);
        }

        //For Getting and Assigning all Quest SO Assets
        #region SettingUpMethods
        
        private static string[] GetSubFoldersRecursive(string root)
        {
            var paths = new List<string>();

            // If there are no further subfolders then AssetDatabase.GetSubFolders returns 
            // an empty array => foreach will not be executed
            // This is the exit point for the recursion
            foreach (var path in AssetDatabase.GetSubFolders(root))
            {
                // add this subfolder itself
                paths.Add(path);

                // If this has no further subfolders then simply no new elements are added
                paths.AddRange(GetSubFoldersRecursive(path));
            }

            return paths.ToArray();
        }

        private void SettingUpQuestlineDictionary()
        {
            List<Quest> questList = new List<Quest>();

            GetAllQuestSOAssets(questList, "Assets/ScriptableObjects/Quests");

            if(questList != null)
            {
                AssignQuestToQuestDictionary(questList, QuestDictionary);
            }
            else if(questList == null)
            {
                Debug.Log("There Is No Quest Master!");
            }
        }
        
        private void GetAllQuestSOAssets(List<Quest> questList, string path)
        {
            string[] pathList = GetSubFoldersRecursive(path);

            foreach(string assetPath in pathList)
            {
                string[] assetNames = AssetDatabase.FindAssets("t:Quest", new[]{assetPath});
                foreach(string SOName in assetNames)
                {
                    var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                    var character = AssetDatabase.LoadAssetAtPath<Quest>(SOpath);
                    questList.Add(character);
                }
            }
        }

        private void AssignQuestToQuestDictionary(List<Quest> questList, Dictionary<string, Quest> questDictionary)
        {
            foreach(Quest questData in questList)
            {
                if(!questDictionary.ContainsValue(questData))
                {
                    questDictionary.Add(questData.questSetting.QuestId, questData);
                }
            }
        }

        #endregion

    
    */
    
    /*
    void SettingUpBaseDictionary()
    {
        //AddressablesManager.Instance.LoadAllAssetsByKey("shopitem", itemList as List<ScriptableObject>);
        //List<InventoryItemData> itemList = new List<InventoryItemData>();

        // GetAllFishes(itemList);
        // GetAllFishesSeed(itemList);
        // GetAllFishesFeed(itemList);

        if(itemList != null)
        {
            AssignItemToDictionary(itemList, List_InventoryItemData_AllItem);
        }
        else if(itemList == null)
        {
            Debug.Log("There Is No Item Master!");
        }
    }
    */

    
}



/*
public class ListOfSprites : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] private string spriteFolder = "Textures/Enemies";

    private void OnValidate() {
        string fullPath = $"{Application.dataPath}/{spriteFolder}";
        if (!System.IO.Directory.Exists(fullPath)) {            
            return;
        }

        var folders = new string[]{$"Assets/{spriteFolder}"};
        var guids = AssetDatabase.FindAssets("t:Sprite", folders);

        var newSprites = new Sprite[guids.Length];

        bool mismatch;
        if (enemySprites == null) {
            mismatch = true;
            enemySprites = newSprites;
        } else {
            mismatch = newSprites.Length != enemySprites.Length;
        }

        for (int i = 0; i < newSprites.Length; i++) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newSprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            mismatch |= (i < enemySprites.Length && enemySprites[i] != newSprites[i]);
        }

        if (mismatch) {
            enemySprites = newSprites;
            Debug.Log($"{name} sprite list updated.");
        }        
    }
    #endif

    public Sprite[] enemySprites;
}
*/