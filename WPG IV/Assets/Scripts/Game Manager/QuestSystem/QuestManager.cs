using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QuestSystem
{
    public class QuestManager : GenericSingletonClass<QuestManager>
    {
        public Dictionary <string, Quest> QuestDictionary;
        public List <Quest> CurrentActiveQuestList;
        public List <Quest> CompletedQuestList;
        
        public override void Awake()
        {
            base.Awake();
            QuestDictionary = new Dictionary <string, Quest>();
            SettingUpQuestDictionary();
            GetAllActivatedQuestFromQuestList();
            GetAllCompletedQuestFromQuestList();
        }

        private void Start()
        {
            
        }
        
        private void GetAllActivatedQuestFromQuestList()
        {
            foreach(Quest questData in QuestDictionary.Values)
            {
                if(questData.IsQuestActive)
                {
                    CurrentActiveQuestList.Add(questData);
                }
            }
        }

        private void GetAllCompletedQuestFromQuestList()
        {
            foreach(Quest questData in QuestDictionary.Values)
            {
                if(questData.IsQuestCompleted)
                {
                    CurrentActiveQuestList.Remove(questData);
                    CompletedQuestList.Add(questData);
                }
            }
        }

        void SettingUpQuestDictionary()
        {
            List<Quest> questList = new List<Quest>();

            GetAllQuestData(questList);

            if(questList != null)
            {
                AssignItemToDictionary(questList, QuestDictionary);
            }
            else if(questList == null)
            {
                Debug.Log("There Is No Item Master!");
            }
        }

        void GetAllQuestData(List <Quest> questList)
        {
            string[] assetNames = AssetDatabase.FindAssets("FishFeed", new[]{"Assets/ScriptableObjects/Quest/QuestData"});
            foreach(string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var character = AssetDatabase.LoadAssetAtPath<Quest>(SOpath);
                questList.Add(character);
            }
        }

        void AssignItemToDictionary(List<Quest> questList, Dictionary<string, Quest> questDictionary)
        {
            foreach(Quest questData in questList)
            {
                questDictionary.Add(questData.questInformation.QuestId, questData);
            }
        }
        
        public void SendProgressToQuestDatas(Object sendedObject)
        {
            foreach(Quest questData in CurrentActiveQuestList)
            {
                questData.SendProgressToObjectives(sendedObject);
            }
        }
    }

}
