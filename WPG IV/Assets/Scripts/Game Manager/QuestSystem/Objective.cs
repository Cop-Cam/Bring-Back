using System;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    public abstract class Objective : ScriptableObject
    {
        public bool IsObjectiveCompleted { get; protected set; }
        public event Action OnObjectiveCompletedEvent; //for sending event to quest so it evaluate when an objective is completed 
        
        public bool IsObjectiveFailed { get; protected set; }
        public event Action OnObjectiveFailedEvent; //for sending event to quest so it evaluate when an objective is failed 
        protected abstract void EvaluateObjective();
        public abstract void AddProgressToObjective(object Data);
        public abstract void GetObjectiveProgression();

        public virtual void InitializeObjective()
        {
            IsObjectiveCompleted = false;
            IsObjectiveFailed = false;
        }

        public virtual void DeInitializeObjective() {}

        protected virtual void ObjectiveIsCompleted()
        {
            OnObjectiveCompletedEvent();
            //ObjectiveCompletedEvent.RemoveAllListeners();
        }

        protected virtual void ObjectiveIsFailed()
        {
            OnObjectiveFailedEvent();
        }

    }

}
