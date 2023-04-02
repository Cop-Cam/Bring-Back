
using System;
using UnityEngine;


namespace QuestSystem
{
    public class ChangePlayerMoney : QuestResultEvent
    {
        [Tooltip("How much the money will change")]
        [SerializeField] private int moneyChange;

        public override void InvokeQuestResultEvent()
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }
    }
}