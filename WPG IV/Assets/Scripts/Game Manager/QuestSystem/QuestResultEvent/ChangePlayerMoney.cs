
using System;
using UnityEngine;


namespace QuestSystem
{
    [CreateAssetMenu(fileName = "ChangePlayerMoney Event", menuName = "Scriptable Objects/Quest Assets/Quest Event/Change Player Money")]
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