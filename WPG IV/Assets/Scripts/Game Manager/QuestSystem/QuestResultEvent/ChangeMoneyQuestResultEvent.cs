
using System;
using UnityEngine;


namespace QuestSystem
{
    public class ChangeMoneyQuestResultEvent : QuestResultEvent
    {
        [SerializeField] private int moneyChange;

        public override void InvokeQuestResultEvent()
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }
    }
}