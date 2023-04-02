
using System.Collections.Generic;
using UnityEngine;


namespace QuestSystem
{
    public class StartNewQuest : QuestResultEvent
    {
        [Tooltip("List of any Quest that will be initialized")]
        [SerializeField] private List<Quest> NewQuest;

        public override void InvokeQuestResultEvent()
        {
            if(NewQuest != null)
            {
                foreach(Quest quest in NewQuest)
                {
                    quest.InitializeQuest();
                }
            }
            
        }
    }
}