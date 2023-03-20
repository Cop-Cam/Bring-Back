
using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestObjectiveData : ScriptableObject
    {
        protected GameObject PlayerGameObject;
        public bool IsObjectiveCompleted;
        public abstract void EvaluateObjective();
        public abstract void AddProgressToObjective();
        public abstract void GetObjectiveProgression();
    }

}
