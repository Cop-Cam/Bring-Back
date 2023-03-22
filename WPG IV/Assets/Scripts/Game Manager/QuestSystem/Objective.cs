
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    public abstract class Objective : ScriptableObject
    {
        //public enum ObjectiveType {None, GetItem}
        //public ObjectiveType objectiveType;
        public bool IsObjectiveCompleted { get; protected set; }
        [HideInInspector] public UnityEvent ObjectiveCompleted;

        //public abstract ObjectiveType GetObjectiveType();
        protected abstract void EvaluateObjective();
        public abstract void AddProgressToObjective(object Data);
        public abstract void GetObjectiveProgression();
        //public abstract bool GetIsObjectiveCompleted();

        public virtual void InitializeObjective()
        {
            IsObjectiveCompleted = false;
            ObjectiveCompleted = new UnityEvent();
        }

        public virtual void DeinitializeObjective()
        {
            ObjectiveCompleted = null;
        }

        protected virtual void ObjectiveIsCompleted()
        {
            IsObjectiveCompleted = true;
            ObjectiveCompleted.Invoke();
            ObjectiveCompleted.RemoveAllListeners();
        }

    }

}
