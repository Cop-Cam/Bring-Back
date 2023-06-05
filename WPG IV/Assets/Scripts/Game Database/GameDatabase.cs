using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(-1000)]
public class GameDatabase : DontDestroyOnLoadSingletonClass<GameDatabase>
{

    [SerializeField] private List<ScriptableObject> itemList = new List<ScriptableObject>();

    //public Dictionary<string, InventoryItemData> DB_InventoryItems{get; private set;}

    public Dictionary<string, FishItemData> DB_FishItems {get; private set;} = new Dictionary<string, FishItemData>();
    public Dictionary<string, FishSeedItemData> DB_FishSeeds {get; private set;} = new Dictionary<string, FishSeedItemData>();
    public Dictionary<string, FishFeedItemData> DB_FishFeeds {get; private set;} = new Dictionary<string, FishFeedItemData>();
    public Dictionary<string, QuestSystem.Quest> DB_Quests {get; private set;} = new Dictionary<string, QuestSystem.Quest>();

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
        //Debug.Log("itemlist size: "+itemList.Count);
        SortAllItemInGame(itemList);

        if(DB_FishItems.Count == 0) Debug.LogError("DB_FishItems is empty");
        if(DB_FishSeeds.Count == 0) Debug.LogError("DB_FishSeeds is empty");
        if(DB_FishFeeds.Count == 0) Debug.LogError("DB_FishFeeds is empty");
        if(DB_Quests.Count == 0) Debug.LogError("DB_Quests is empty");
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

    //Basically, SO will save everything that happen in runtime to SO asset
    //To getting around this, I made a copy for every included SO to get assigned in their own database
    private void SortAllItemInGame(List<ScriptableObject> list)
    {
        foreach (ScriptableObject SO in list)
        {
            ScriptableObject instantiatedInRuntime = Instantiate(SO);
            if(instantiatedInRuntime is InventoryItemData)
            {
                InventoryItemData tempInventoryItem = instantiatedInRuntime as InventoryItemData;

                if(tempInventoryItem is FishFeedItemData)
                {
                    FishFeedItemData temp = tempInventoryItem as FishFeedItemData;
                    DB_FishFeeds.Add(temp.id, temp);
                }
                else if(tempInventoryItem is FishItemData)
                {
                    FishItemData temp = tempInventoryItem as FishItemData;
                    DB_FishItems.Add(temp.id, temp);
                }
                else if(tempInventoryItem is FishSeedItemData)
                {
                    FishSeedItemData temp = tempInventoryItem as FishSeedItemData;
                    DB_FishSeeds.Add(temp.id, temp);
                }
            }

            if(instantiatedInRuntime is QuestSystem.Quest)
            {
                QuestSystem.Quest temp = instantiatedInRuntime as QuestSystem.Quest;
                DB_Quests.Add(temp.questSetting.QuestId, temp);
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
        itemList.Clear();
        //itemList = new List<InventoryItemData>();
        GetAllFishes(itemList);
        GetAllFishesFeed(itemList);
        GetAllFishesSeed(itemList);
        GetAllQuest(itemList);

        if(itemList == null || itemList.Count == 0)
        {
            Debug.LogError("templist is empty");
            return;
        }

        //remove duplication in list
        itemList = itemList.Distinct().ToList();
    }


    //mengambil data SO fish pada folder yang ditentukan
    void GetAllFishes(List<ScriptableObject> fishItemList)
    {
        string[] assetNamesEndemic = AssetDatabase.FindAssets("t:FishItemData t:FishItemData.fishTypes.Endemic", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Endemic"});
        foreach(string SOName in assetNamesEndemic)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character as ScriptableObject);
        }

        string[] assetNamesInvansive = AssetDatabase.FindAssets("t:FishItemData t:FishItemData.fishTypes.Invansive", new[]{"Assets/ScriptableObjects/Fishes/FishesItem/Invansive"});
        foreach(string SOName in assetNamesInvansive)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishItemData>(SOpath);
            fishItemList.Add(character as ScriptableObject);
        }
    }
    void GetAllFishesSeed(List<ScriptableObject> fishSeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("t:FishSeedItemData", new[]{"Assets/ScriptableObjects/Fishes/FishesSeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishSeedItemData>(SOpath);
            fishSeedList.Add(character as ScriptableObject);
        }
    }
    void GetAllFishesFeed(List<ScriptableObject> fishFeedList)
    {
        string[] assetNames = AssetDatabase.FindAssets("t:FishFeedItemData", new[]{"Assets/ScriptableObjects/Fishes/FishesFeed"});
        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<FishFeedItemData>(SOpath);
            fishFeedList.Add(character as ScriptableObject);
        }
    }
    
    private void GetAllQuest(List<ScriptableObject> questList)
    {
        //List<QuestSystem.Quest> questList = new List<QuestSystem.Quest>();

        GetAllQuestSOAssets(questList, "Assets/ScriptableObjects/Quests");

        // if(questList != null)
        // {
        //     AssignQuestToQuestDictionary(questList, DB_Quests);
        // }
        // else if(questList == null)
        // {
        //     Debug.Log("There Is No Quest Master!");
        // }
    }

    private void GetAllQuestSOAssets(List<ScriptableObject> questList, string path)
    {
        string[] pathList = GetSubFoldersRecursive(path);

        foreach(string assetPath in pathList)
        {
            string[] assetNames = AssetDatabase.FindAssets("t:Quest", new[]{assetPath});
            foreach(string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var character = AssetDatabase.LoadAssetAtPath<QuestSystem.Quest>(SOpath);

                // if(questList.Contains(character as ScriptableObject))
                // {
                //     Debug.Log("this quest is contained already");
                //     continue;
                // }

                questList.Add(character as ScriptableObject);
            }
        }
    }

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
        
    

    // private void AssignQuestToQuestDictionary(List<QuestSystem.Quest> questList, Dictionary<string, QuestSystem.Quest> questDictionary)
    // {
    //     foreach(QuestSystem.Quest questData in questList)
    //     {
    //         if(!questDictionary.ContainsValue(questData))
    //         {
    //             questDictionary.Add(questData.questSetting.QuestId, questData);
    //         }
    //     }
    // }
    #endif

   
    
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