
using UnityEngine;

namespace QuestSystem
{
    public class GetFishesPoint : Objective
    {
        [System.Serializable]
        public struct FishObjectiveSetting
        {
            public FishItemData.FishTypes fishType;
            public int FishObjectivePointNeeded;
        }
        public FishObjectiveSetting fishObjectiveSetting;

        private int FishObjectiveCollectedPoint;

        
        //mungkin buat dishow di ui
        public override void GetObjectiveProgression()
        {
            Debug.Log("=========================================================");
            Debug.Log("Required Fish Types: " + fishObjectiveSetting.fishType);
            Debug.Log("CurrentFishPoint/RequiredFishPoint: " + FishObjectiveCollectedPoint + (" / ") + fishObjectiveSetting.FishObjectivePointNeeded);
            Debug.Log("IsObjectiveCompleted: "+IsObjectiveCompleted);
            Debug.Log("=========================================================");
        }
        
        protected override void EvaluateObjective()
        {
            IsObjectiveCompleted = CheckIsObjectiveCompleted();
        
            if(IsObjectiveCompleted)
            {
                //ObjectiveCompletedEvent.Invoke();
                ObjectiveIsCompleted();
            }
        
        }
        
        private bool CheckIsObjectiveCompleted()
        {
            if(FishObjectiveCollectedPoint >= this.fishObjectiveSetting.FishObjectivePointNeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void AddProgressToObjective(object sendedData)
        {
            if(sendedData is FishItemData)
            {
                FishItemData currentSendedFish = sendedData as FishItemData;
                
                if(currentSendedFish.fishTypes == fishObjectiveSetting.fishType && FishObjectiveCollectedPoint < fishObjectiveSetting.FishObjectivePointNeeded)
                {
                    FishObjectiveCollectedPoint += currentSendedFish.fishPoint;
                }
            }

            EvaluateObjective();
        }

        /*
        public override bool GetIsObjectiveCompleted()
        {
            return IsObjectiveCompleted;
        }*/

        // public void GetSendedPoint(int sendedPoint)
        // {
        //     SendedFishObjectivePoint = sendedPoint;
        // }

        // public override ObjectiveType GetObjectiveType()
        // {
        //     return objectiveType;
        // }
    }

}
