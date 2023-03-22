
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace QuestSystem
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Objects/Quest Assets/Quest Data")]
    public class Quest : ScriptableObject
    {
        [System.Serializable]
        public struct QuestInformation
        {
            //[Header("Quest Information")]

            [Tooltip("Quest Id [Recommended Format: Questline Id_This Quest Id_This Quest(Index/Stage) In Questline]")]
            public string QuestId;

            [Tooltip("Quest Name")]
            public string QuestName;

            [Tooltip("Quest Description")]
            public string QuestDescription;

            [Tooltip("If false, the QuestTimeLimit option will be ignored")]
            public bool IsQuestHasLimit;

            [Tooltip("Quest Time Limit (Day)")]
            public int QuestTimeLimit;
            
            /*
            [Header("Quest Objective")]

            [Tooltip("All of This Quest Objective")]
            public List<Objective> QuestObjectives;

            [Tooltip("If set to false, just skip the QuestTimeLimit option")]
            public bool IsQuestHasLimit;

            [Tooltip("Quest Time Limit (Day)")]
            public int QuestTimeLimit;

            [Tooltip("All Executed Events When Completing The Quest")]
            public UnityEvent OnQuestCompletedEvent;

            [Tooltip("All Executed Event When Failing The Quest")]
            public UnityEvent OnQuestFailedEvent;
            */
        }
        public QuestInformation questInformation;

        [HideInInspector]public List<Objective> QuestObjectives;

        [HideInInspector]public List<QuestResultEvent> OnQuestCompletedEvent;

        [HideInInspector]public List<QuestResultEvent> OnQuestFailedEvent;

        /*
        [System.Serializable]
        public struct QuestEvent
        {
            //[Header("Quest Events")]

            [Tooltip("All Executed Events When Completing The Quest")]
            //public UnityEvent OnQuestCompletedEvent;
            public List<UnityEvent> OnQuestCompletedEvent;

            [Tooltip("All Executed Event When Failing The Quest")]
            //public UnityEvent OnQuestFailedEvent;
            public List<UnityEvent> OnQuestFailedEvent;
        }
        public QuestEvent questEvent;
        */

        public bool IsQuestActive { get; private set; }

        public bool IsQuestCompleted { get; private set; }
        //[HideInInspector] public UnityEvent QuestCompletedEvent;

        public bool IsQuestFailed { get; private set; }
        //[HideInInspector] public UnityEvent QuestFailedEvent;


        private void DayHasChanged()
        {
            questInformation.QuestTimeLimit -= 1;
            if(questInformation.QuestTimeLimit <= 0)
            {
                IsQuestFailed = true;
                CheckQuestStatus();
            }
        }

        private void CheckObjective()
        {
            IsQuestCompleted = QuestObjectives.All(objective => objective.IsObjectiveCompleted);

            if(IsQuestCompleted)
            {
                CheckQuestStatus();
            }
        }

        
        public void InitializeQuest() //For starting/initializing the quest
        {
            IsQuestActive = true;

            foreach(Objective objective in QuestObjectives)
            {
                objective.InitializeObjective();

                //Add listener to ObjectiveCompleted UnityEvent so that everytime ObjectiveCompleted get invoked, CheckObjective get ran
                objective.ObjectiveCompleted.AddListener(delegate { CheckObjective(); }); 
            }

            //Set Listerner to TimeManager if the quest has timelimit
            if(questInformation.IsQuestHasLimit)
            {
                TimeManager.Instance.OnDayChanged += DayHasChanged;
            }
            else
            {
                TimeManager.Instance.OnDayChanged -= DayHasChanged;
            }
        }

        public void DeinitializeQuest()
        {
            IsQuestActive = false;

            foreach(Objective objective in QuestObjectives)
            {
                objective.DeinitializeObjective();

                objective.ObjectiveCompleted.RemoveAllListeners();
            }

            TimeManager.Instance.OnDayChanged -= DayHasChanged;
        }

        private void CheckQuestStatus()
        {
            if(IsQuestCompleted)
            {
                RewardOnQuestCompleted();
                //IsQuestActive = false;
                DeinitializeQuest();
            }
            else if(IsQuestFailed)
            {
                RewardOnQuestFailed();
                //IsQuestActive = false;
                DeinitializeQuest();
            }
            else
            {
                Debug.Log("This Quest is "+IsQuestActive);
                Debug.Log("This Quest is not yet completed or failed!");
            }
        }

        private void RewardOnQuestCompleted()
        {
            if(OnQuestCompletedEvent != null)
            {
                foreach(QuestResultEvent Event in OnQuestCompletedEvent)
                {
                    Event.InvokeQuestResultEvent();
                }
                //questEvent.OnQuestCompletedEvent.Invoke();
            }
        }
        private void RewardOnQuestFailed()
        {
            if(OnQuestFailedEvent != null)
            {
                foreach(QuestResultEvent Event in OnQuestFailedEvent)
                {
                    Event.InvokeQuestResultEvent();
                }
                //questEvent.OnQuestFailedEvent.Invoke();
            }
        }
        
        public void SendProgressToObjectives(object sendedData)
        {
            foreach(Objective questObjective in QuestObjectives)
            {
                questObjective.AddProgressToObjective(sendedData);
            } 
        }

        /*
        private int GetThisQuestObjectiveSize()
        {
            return questSetting.QuestObjectives.Count-1;
        }
        *

        private void EvaluateQuestObjectives()
        {
            int ThisQuestUnfinishedObjective = GetThisQuestObjectiveSize();

            foreach(Objective questObjective in questSetting.QuestObjectives)
            {
                if(questObjective.GetIsObjectiveCompleted())
                {
                    ThisQuestUnfinishedObjective--;
                }
            }

            if(ThisQuestUnfinishedObjective == 0)
            {
                IsQuestCompleted = true;
            }
        }
        */
    }

}





#if UNITY_EDITOR
[CustomEditor(typeof(QuestSystem.Quest))]
public class QuestEditor : Editor
{
    //SerializedProperty QuestInfoProperty;

    List<string> QuestObjectiveType;
    SerializedProperty QuestObjectiveListProperty;

    List<string> QuestEventType;
    SerializedProperty QuestRewardListProperty;
    SerializedProperty QuestPunishmentListProperty;


    //[MenuItem("Asset/Quest", priority = 0)]
    // [MenuItem("Test/Quest", priority = 0)]
    // public static void CreateQuest()
    // {
    //     var newQuest = CreateInstance<QuestSystem.Quest>();
        
    //     ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
    // }

    void OnEnable()
    {
        //QuestInfoProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questInformation));

        QuestRewardListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.OnQuestCompletedEvent));

        QuestPunishmentListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.OnQuestFailedEvent));

        QuestObjectiveListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.QuestObjectives));
        

        var LookUpQuestObjective = typeof(QuestSystem.Objective);
        QuestObjectiveType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(LookUpQuestObjective))
            .Select(type => type.Name)
            .ToList();

        var LookUpOnCompletedQuestEvent = typeof(QuestSystem.QuestResultEvent);
        QuestEventType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(LookUpOnCompletedQuestEvent))
            .Select(type => type.Name)
            .ToList();

        var LookUpOnFailedQuestEvent = typeof(QuestSystem.QuestResultEvent);
        QuestEventType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(LookUpOnFailedQuestEvent))
            .Select(type => type.Name)
            .ToList();
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //serializedObject.Update();
        DrawDefaultInspector();
        // var child = QuestInfoProperty.Copy();
        // var depth = child.depth;
        // child.NextVisible(true);

        //EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(QuestInfoProperty);

        //EditorGUILayout.LabelField("Quest Reward", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(QuestRewardProperty);
        
        
        // while(child.depth > depth)
        // {
        //     EditorGUILayout.PropertyField(child, true);
        //     child.NextVisible(true);
        // }

        /*
        int choice = EditorGUILayout.Popup("Add new Quest Objective", -1, QuestObjectiveType.ToArray());
        if(choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(QuestObjectiveType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);

            QuestObjectiveListProperty.InsertArrayElementAtIndex(QuestObjectiveListProperty.arraySize);
            QuestObjectiveListProperty.GetArrayElementAtIndex(QuestObjectiveListProperty.arraySize-1).objectReferenceValue = newInstance;
        }


        Editor ed = null;
        int toDelete = -1;
        
        for(int i=0; i<QuestObjectiveListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = QuestObjectiveListProperty.GetArrayElementAtIndex(i);
            SerializedObject SO = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if(GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if(toDelete != -1)
        {
            var item = QuestObjectiveListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            //Need to do it twice, first to nullify the entry, second to actually remove it
            QuestObjectiveListProperty.DeleteArrayElementAtIndex(toDelete);
            QuestObjectiveListProperty.DeleteArrayElementAtIndex(toDelete);
        }
        */
        CreateMenu("Quest Objectives",QuestObjectiveType, QuestObjectiveListProperty);


        CreateMenu("Quest Completed Events",QuestEventType, QuestRewardListProperty);


        CreateMenu("Quest Failed Events",QuestEventType, QuestPunishmentListProperty);


        serializedObject.ApplyModifiedProperties();
    }

    void CreateMenu(string MenuName, List<string> type, SerializedProperty property)
    {
        int choice = EditorGUILayout.Popup("Add New " + MenuName, -1, type.ToArray());
        if(choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(type[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target); //Making SO get Intantiated inside current SO

            property.InsertArrayElementAtIndex(property.arraySize);
            property.GetArrayElementAtIndex(property.arraySize-1).objectReferenceValue = newInstance;
        }


        Editor ed = null;
        int toDelete = -1;
        
        for(int i=0; i<property.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = property.GetArrayElementAtIndex(i);
            SerializedObject SO = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            //if(GUILayout.Button("-", GUILayout.Width(32)))
            if(GUILayout.Button("Delete"))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if(toDelete != -1)
        {
            var item = property.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            //Need to do it twice, first to nullify the entry, second to actually remove it
            property.DeleteArrayElementAtIndex(toDelete);
            property.DeleteArrayElementAtIndex(toDelete);
        }
    }
    
}

#endif


