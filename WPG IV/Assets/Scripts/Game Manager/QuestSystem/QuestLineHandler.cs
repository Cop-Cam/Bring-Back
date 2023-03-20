using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    public class QuestLineHandler : ScriptableObject
    {
        public struct QuestLineSetting
        {
            [Header("QuestLine Settings")]

            [Tooltip("QuestLine Id")]
            public string QuestLineId;

            [Tooltip("QuestLine Name")]
            public string QuestLineName;

            public QuestData[] QuestLineQuestDatas;
            
            public int CurrentHandledQuestIndex;
            
        }
        public QuestLineSetting questLineSetting;

        void Start()
        {
            questLineSetting.CurrentHandledQuestIndex = 0;
        }
        

        public void ChangePlayerMoney(int moneyChange)
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }

        public void DoQuestStuff(QuestData currentHandledQuestData)
        {
            
        }

        public void QuestProgression()
        {

        }    
    }
}
