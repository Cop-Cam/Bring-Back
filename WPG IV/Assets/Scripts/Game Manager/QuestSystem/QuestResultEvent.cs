
using System;
using UnityEngine;
//using UnityEngine.Events;


namespace QuestSystem
{
    public abstract class QuestResultEvent : ScriptableObject
    {
        public abstract void InvokeQuestResultEvent();

    }
}
