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
            else
            {
                Debug.Log("QuestDictionary size: "+QuestDictionary.Count);
            }

            //SettingUpQuestlineDictionary();

            CurrentActivatedQuestList = new List<Quest>();
            CurrentFailedQuestList = new List<Quest>();
            CurrentCompletedQuestList = new List<Quest>();
        }

        private void Start() 
        {
            QuestEventUICanvas.SetActive(false);
        }

        #region Quest Notification Methods
        private IEnumerator QuestIsStartedNotif(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Started A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        private IEnumerator QuestIsFailedNotif(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Failed A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        private IEnumerator QuestIsCompletedNotif(QuestSystem.Quest currentHandledQuest)
        {
            QuestEventUICanvas.SetActive(true);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = currentHandledQuest.questSetting.QuestName;
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "Completed A Quest";

            yield return new WaitForSecondsRealtime(QuestUIDisplayDuration);

            QuestEventUICanvas.SetActive(false);
            QuestName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            EventName.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        #endregion
    
        
        #region  Quest Event Handler Methods
        private void HandleActivatedQuest(Quest questInstance)
        {
            if(!CurrentFailedQuestList.Contains(questInstance) && !CurrentCompletedQuestList.Contains(questInstance))
            {
                if(!CurrentActivatedQuestList.Contains(questInstance))
                {
                    StartCoroutine(QuestIsStartedNotif(questInstance));

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
            StartCoroutine(QuestIsFailedNotif(questInstance));

            CurrentActivatedQuestList?.Remove(questInstance);
            CurrentCompletedQuestList?.Remove(questInstance);
            CurrentFailedQuestList.Add(questInstance);

            //ShowListConditionForDebug();
        }

        private void HandleCompletedQuest(Quest questInstance)
        {
            StartCoroutine(QuestIsCompletedNotif(questInstance));

            CurrentActivatedQuestList?.Remove(questInstance);
            CurrentCompletedQuestList.Add(questInstance);
            CurrentFailedQuestList?.Remove(questInstance);

            //ShowListConditionForDebug();
        }
        #endregion

        public void InitQuestFromQuestManager(string questId)
        {
            QuestDictionary[questId].InitializeQuest();
        }

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
            Debug.Log("subscribing to all quest");
            foreach(Quest quest in QuestDictionary.Values)
            {
                quest.OnQuestCompletedEvent += HandleCompletedQuest;
                quest.OnQuestActivatedEvent += HandleActivatedQuest;
                quest.OnQuestFailedEvent += HandleFailedQuest;
            }
        }

        private void OnDisable() 
        {
            Debug.Log("unsubscribing to all quest");

            foreach(Quest quest in QuestDictionary.Values)
            {
                quest.OnQuestCompletedEvent -= HandleCompletedQuest;
                quest.OnQuestActivatedEvent -= HandleActivatedQuest;
                quest.OnQuestFailedEvent -= HandleFailedQuest;
            }
        }
            
        
    }
}
