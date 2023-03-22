
using System;
using UnityEngine;


namespace QuestSystem
{
    public class StartNewQuest : QuestResultEvent
    {
        [SerializeField] private Quest NewQuest;

        public override void InvokeQuestResultEvent()
        {
            NewQuest.InitializeQuest();
        }
    }
}