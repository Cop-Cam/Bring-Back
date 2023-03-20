
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "GetFishesPoint_ObjectiveType", menuName = "SO/QuestData/QuestObjectiveData")]
    public class GetFishesPoint_ObjectiveType : QuestObjectiveData
    {
        [System.Serializable]
        public struct FishObjectiveSetting
        {
            public FishItemData.FishTypes fishType;
            public int FishObjectivePointNeeded;
        }
        public FishObjectiveSetting fishObjectiveSetting;

        private int FishObjectiveCollectedPoint;

        private int SendedFishObjectivePoint;
        
        private void Start()
        {
            PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        }

        //mungkin buat dishow di ui
        public override void GetObjectiveProgression()
        {
            
        }

        public override void EvaluateObjective()
        {
            if(CheckIsObjectiveCompleted())
            {
                //do logic
            }
            else if(!CheckIsObjectiveCompleted())
            {
                //do logic
            }
            IsObjectiveCompleted = CheckIsObjectiveCompleted();
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

        public override void AddProgressToObjective()
        {
            FishObjectiveCollectedPoint += SendedFishObjectivePoint;

            EvaluateObjective();
        }

        public void GetSendedPoint(int sendedPoint)
        {
            SendedFishObjectivePoint = sendedPoint;
        }
    }

}
