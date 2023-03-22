
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    [CreateAssetMenu(fileName ="Quesline Data", menuName ="Scriptable Objects/Quest Assets/Quesline Data",order = 0)]
    public class Quesline : ScriptableObject
    {
        [System.Serializable]
        public struct QuestlineSetting
        {
            [Header("QuestLine Settings")]

            [Tooltip("QuestLine Id")]
            public string QuestlineId;

            [Tooltip("QuestLine Name")]
            public string QuestlineName;

            public Quest[] QuestlineQuests;
            
        }
        public int CurrentHandledQuestIndex {get; set;}

        public QuestlineSetting questlineSetting;

        void Start()
        {
            CurrentHandledQuestIndex = 0;
        }
        

        public void DoQuestStuff(Quest currentHandledQuestData)
        {
            
        }

        public void QuestProgression()
        {

        }    
    }
}
