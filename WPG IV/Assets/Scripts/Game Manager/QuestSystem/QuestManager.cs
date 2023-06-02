using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QuestSystem
{
    public class QuestManager : GenericSingletonClass<QuestManager>
    {
        [SerializeField] private float QuestUIDisplayDuration;
        [SerializeField] private GameObject QuestEventUICanvas;
        [SerializeField] private GameObject QuestName;
        [SerializeField] private GameObject EventName;

        private Dictionary<string, Quest> QuestDictionary;
        public List<Quest> CurrentActivatedQuestList {get; private set;}
        public List<Quest> CurrentFailedQuestList {get; private set;}
        public List<Quest> CurrentCompletedQuestList {get; private set;}

        public override void Awake()
        {
            base.Awake();
            QuestDictionary = GameDatabase.Instance.DB_Quests;
            
            if(GameDatabase.Instance.DB_Quests.Count == 0)
            {
                Debug.LogWarning("database quest dict kosong");
            }

            //SettingUpQuestlineDictionary();

            CurrentActivatedQuestList = new List<Quest>();
            CurrentFailedQuestList = new List<Quest>();
            CurrentCompletedQuestList = new List<Quest>();
        }

        private void Start() 
        {
            //UIManager.Instance.RegisterMenu(this, this.gameObject);

            QuestEventUICanvas.SetActive(false);
        }

        private IEnumerator QuestIsStartedEvent(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Started A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        private IEnumerator QuestIsFailedEvent(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Failed A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        private IEnumerator QuestIsCompletedEvent(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Completed A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    /*
    #if UNITY_EDITOR
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
    #endif
    */

        private void HandleActivatedQuest(Quest questInstance)
        {
            if(!CurrentFailedQuestList.Contains(questInstance) && !CurrentCompletedQuestList.Contains(questInstance))
            {
                if(!CurrentActivatedQuestList.Contains(questInstance))
                {
                    StartCoroutine(QuestIsStartedEvent(questInstance));

                    CurrentActivatedQuestList?.Add(questInstance);
                }
                else
                {
                    Debug.LogWarning("Quest is already activated!");
                }
            }
            else
            {
                Debug.LogWarning("Quest is already completed or failed!");
                questInstance.DeinitializeQuest();
            }
        }

        private void HandleFailedQuest(Quest questInstance)
        {
            StartCoroutine(QuestIsFailedEvent(questInstance));

            CurrentActivatedQuestList?.Remove(questInstance);
            CurrentCompletedQuestList?.Remove(questInstance);
            CurrentFailedQuestList.Add(questInstance);

            //ShowListConditionForDebug();
        }
        private void HandleCompletedQuest(Quest questInstance)
        {
            StartCoroutine(QuestIsCompletedEvent(questInstance));

            CurrentActivatedQuestList?.Remove(questInstance);
            CurrentCompletedQuestList.Add(questInstance);
            CurrentFailedQuestList?.Remove(questInstance);

            //ShowListConditionForDebug();
        }

        /*
        private void ShowListConditionForDebug()
        {
            Debug.Log("CurrentActivatedQuestList size : " + CurrentActivatedQuestList.Count);
            Debug.Log("All active Quest List is : ");
            foreach(Quest quest in CurrentActivatedQuestList)
            {
                Debug.Log("Quest Name: " + quest.questInformation.QuestName);
            }

            Debug.Log("CurrentCompletedQuestList size : " + CurrentCompletedQuestList.Count);
            Debug.Log("All completed Quest List is : ");
            foreach(Quest quest in CurrentCompletedQuestList)
            {
                Debug.Log("Quest Name: " + quest.questInformation.QuestName);
            }

            Debug.Log("CurrentFailedQuestList size : " + CurrentFailedQuestList.Count);
            Debug.Log("All failed Quest List is : ");
            foreach(Quest quest in CurrentFailedQuestList)
            {
                Debug.Log("Quest Name: " + quest.questInformation.QuestName);
            }
        }
        */
        public void SendProgressFromQuestManagerToQuest(object sendedData)
        {
            if(sendedData != null)
            {
                // foreach(Quest quest in QuestDictionary.Values)
                // {
                //     if(quest.IsQuestActive && !quest.IsQuestFailed && !quest.IsQuestCompleted)
                //     {
                //         quest.SendProgressFromQuestToObjectives(sendedData);
                //     }
                // }

                foreach(Quest quest in CurrentActivatedQuestList)
                {
                    quest.SendProgressFromQuestToObjectives(sendedData);
                }
            }
            else
            {
                Debug.LogWarning("sended data is null");    
            }
        }


        private void OnEnable()
        {
            foreach(Quest quest in QuestDictionary.Values)
            {
                quest.OnQuestCompletedEvent += HandleCompletedQuest;
                quest.OnQuestActivatedEvent += HandleActivatedQuest;
                quest.OnQuestFailedEvent += HandleFailedQuest;
            }
        }

        private void OnDisable() 
        {
            foreach(Quest quest in QuestDictionary.Values)
            {
                quest.OnQuestCompletedEvent -= HandleCompletedQuest;
                quest.OnQuestActivatedEvent -= HandleActivatedQuest;
                quest.OnQuestFailedEvent -= HandleFailedQuest;
            }
        }
            
        
    }
}
