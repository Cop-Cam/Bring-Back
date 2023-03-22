
using System;
using UnityEngine;
//using UnityEngine.Events;


namespace QuestSystem
{
    public abstract class QuestResultEvent : ScriptableObject
    {
        public abstract void InvokeQuestResultEvent();

    }
    /*
    public class QuestEventManager : GenericSingletonClass<QuestEventManager>
    {
        void Start()
        {
            
        }
        /*
        public void AddInvansiveFishQuestObjectivePoint(QuestData questData, int addedPoint)
        {
            questData.questSetting.RequiredInvansiveFishesPoint += addedPoint;
        }
        public void AddEndemicFishQuestObjectivePoint(QuestData questData, int addedPoint)
        {
            questData.questSetting.RequiredEndemicFishesPoint += addedPoint;
        }*/
/*
        public void ChangePlayerMoney(int moneyChange)
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }
    }
*/
/*
    public static class QuestEventManager 
    {
        
        public static void AddInvansiveFishQuestObjectivePoint(QuestData questData, int addedPoint)
        {
            questData.questSetting.RequiredInvansiveFishesPoint += addedPoint;
        }
        public static void AddEndemicFishQuestObjectivePoint(QuestData questData, int addedPoint)
        {
            questData.questSetting.RequiredEndemicFishesPoint += addedPoint;
        }

        public static void ChangePlayerMoney(int moneyChange)
        {
            PlayerResourceManager.Instance.ChangeMoney(moneyChange);
        }
    }*/
}
