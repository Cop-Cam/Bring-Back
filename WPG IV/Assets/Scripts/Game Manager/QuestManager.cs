using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : GenericSingletonClass<QuestManager>
{
    public ScriptableObject CurrentQuest;
    private string CurrentQuestName;
    private string CurrentQuestDescription;
    private int CurrentQuestTimeLimit;
    //private int CurrentQuestTimeLimit;

    //objective, reward, nama quest, deskripsi, fail consequences, time limit
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
