using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "SO/QuestData", order = 4)]
    public class QuestData : ScriptableObject
    {
        [System.Serializable]
        public struct QuestSetting
        {
            [Header("Quest Settings")]

            [Tooltip("Quest Id [Recommended Format: QuestLineName_ThisQuestId_ThisQuestIndexInQuestLine]")]
            public string QuestId;

            [Tooltip("Quest Name")]
            public string QuestName;

            [Tooltip("Quest Description")]
            public string QuestDescription;
            
            [Header("Quest Objective")]
            
            [Tooltip("All of This Quest Objective")]
            public QuestObjectiveData[] ThisQuestObjective;

            // [Tooltip("Required Completion Point For Invansive Fishes")]
            // public int RequiredInvansiveFishesPoint;
            // [Tooltip("Required Completion Point For Endemic Fishes")]
            // public int RequiredEndemicFishesPoint;

            [Tooltip("Quest Time Limit (Day)")]
            public int QuestTimeLimit;

            [Tooltip("The Reward of The Quest")]
            public UnityEvent OnQuestCompletedEvent;

            [Tooltip("The Punishment of The Quest")]
            public UnityEvent OnQuestFailedEvent;
        }
        public QuestSetting questSetting;
        
        private int currentInvansiveFishesPoint;
        private int currentEndemicFishesPoint;

        void Awake()
        {
            currentInvansiveFishesPoint = 0;
            currentEndemicFishesPoint = 0;
        }

        void Start()
        {
            
        }

        private void RefreshQuestProgress()
        {
            foreach(QuestObjectiveData questObjective in questSetting.ThisQuestObjective)
            {
                questObjective.EvaluateObjective();
            }
            // if(currentInvansiveFishesPoint == questSetting.RequiredInvansiveFishesPoint)
            // {
                
            // }
            // if(currentEndemicFishesPoint == questSetting.RequiredEndemicFishesPoint)
            // {
                
            // }
        }

        /*
        public void AddInvansiveFishQuestObjectivePoint(int addedPoint)
        {
            this.questSetting.RequiredInvansiveFishesPoint += addedPoint;
        }
        public void AddEndemicFishQuestObjectivePoint(int addedPoint)
        {
            this.questSetting.RequiredEndemicFishesPoint += addedPoint;
        }*/

        private void CheckCompletionOfThisQuestObjectives()
        {

        }

        public void ChangePlayerMoney(int moneyChange)
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }

        public void ThisQuestIsCompleted()
        {
            if(questSetting.OnQuestCompletedEvent != null)
            {
                questSetting.OnQuestCompletedEvent.Invoke();
            }
        }

        public void ThisQuestIsFailed()
        {
            if(questSetting.OnQuestFailedEvent != null)
            {
                questSetting.OnQuestFailedEvent.Invoke();
            }
        }
    }
}
