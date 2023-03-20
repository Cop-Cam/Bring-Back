using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : GenericSingletonClass<QuestManager>
    {
        public Dictionary <string, QuestData> QuestList = new Dictionary <string, QuestData>();
        public Dictionary <string, QuestData> CurrentQuestList = new Dictionary <string, QuestData>();
        public Dictionary <string, QuestData> CompletedQuestList = new Dictionary <string, QuestData>();
        
        
        
        // public QuestData CurrentQuest;
        // private string CurrentQuestName;
        // private string CurrentQuestDescription;
        // private QuestData CurrentQuestWinCondition;
        // private QuestData CurrentQuestLoseCondition;
       
        //private int CurrentQuestTimeLimit;

        //objective, reward, nama quest, deskripsi, fail consequences, time limit
        // Start is called before the first frame update
        
    }

}
