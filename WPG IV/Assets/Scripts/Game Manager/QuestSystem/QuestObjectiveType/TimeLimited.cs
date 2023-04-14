
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "TimeLimited Objective", menuName = "Scriptable Objects/Quest Assets/Quest Objective/Timed Quest Objective")]
    public class TimeLimited : Objective
    {
        [System.Serializable]
        private struct TimeLimitSetting
        {
            [Tooltip("The last day for this objective to be active")]
            public int TimeLimitByDay;
        }
        [SerializeField] private TimeLimitSetting timeLimitSetting;

        //private int TimeLeft; 
        //public bool TimeIsUp {get; private set;}


        private void SubscribeToDayChanged()
        {
            //listen to OnDayChanged in TimeManager
            //so when ondaychanged fired, dayhaschanged run
            
            do
            {
                TimeManager.Instance.OnDayChanged += DayHasChanged;
            } 
            while (TimeManager.Instance == null);
            //TimeManager.Instance.OnDayChanged += DayHasChanged; 
        }
        private void UnSubscribeToDayChanged()
        {
            if(TimeManager.Instance == null) return;

            TimeManager.Instance.OnDayChanged -= DayHasChanged;
        }

        private void OnEnable()
        {
            SubscribeToDayChanged();
        }
        private void OnDisable()
        {
            UnSubscribeToDayChanged();
        }

        private void DayHasChanged() 
        {
            /*
            TimeLeft -= 1;
            if(TimeLeft <= 0)
            {
                TimeIsUp = true;
                EvaluateObjective();
            }
            */

            if(timeLimitSetting.TimeLimitByDay >= TimeManager.Instance.date)
            {
                EvaluateObjective();
            }


        }


        public override void InitializeObjective()
        {
            base.InitializeObjective();
            //TimeLeft = timeLimitSetting.TimeLimitByDay;    
            SubscribeToDayChanged();
        }

        public override void DeInitializeObjective()
        {  
            UnSubscribeToDayChanged();
        }
        
        
        //mungkin buat dishow di ui
        public override void GetObjectiveProgression()
        {
            Debug.Log("=========================================================");
            Debug.Log("Origin Time : " + timeLimitSetting.TimeLimitByDay);
            //Debug.Log("Time Left: " + TimeLeft);
            Debug.Log("IsObjectiveCompleted: "+IsObjectiveCompleted);
            Debug.Log("=========================================================");
        }
        
        protected override void EvaluateObjective()
        {
            IsObjectiveCompleted = CheckIsObjectiveCompleted();
            
            if(IsObjectiveCompleted)
            { 
                ObjectiveIsCompleted();
            }
            else if(IsObjectiveFailed)
            {
                ObjectiveIsFailed();
            }
        }

        
        ///<summary>
        ///Keep returning true so the quest can be completed by getting all its objective is completed bool. 
        ///If return false, then the quest will be failed instantly by EvaluateObjective method.
        ///</summary>
        private bool CheckIsObjectiveCompleted()
        {
            /*
            if(TimeIsUp)
            {
                IsObjectiveFailed = true;
                return false;
            }
            else
            {
                return true;
            }
            */

            if(timeLimitSetting.TimeLimitByDay <= TimeManager.Instance.date)
            {
                return true;
            }
            else
            {
                IsObjectiveFailed = true;
                return false;
            }
        }
        

        public override void AddProgressToObjective(object sendedData)
        {
            //EvaluateObjective();
        }
    }
}
