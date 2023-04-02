using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
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

            /*
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

        [HideInInspector]public List<QuestResultEvent> ExecutedEventOnQuestCompleted;

        [HideInInspector]public List<QuestResultEvent> ExecutedEventOnQuestFailed;



        public bool IsQuestActive { get; private set; }
        //[HideInInspector] public UnityEvent QuestStartedEvent; //tell when quest is started
        public event Action<Quest> OnQuestActivatedEvent; //tell when quest is started

        public bool IsQuestCompleted { get; private set; }
        //[HideInInspector] public UnityEvent QuestCompletedEvent; //tell when quest is completed
        public event Action<Quest> OnQuestCompletedEvent; //tell when quest is completed

        public bool IsQuestFailed { get; private set; }
        //[HideInInspector] public UnityEvent QuestFailedEvent; //tell when quest is failed
        public event Action<Quest> OnQuestFailedEvent; //tell when quest is failed


        /*
        private void SubscribeToDayChanged()
        {
            TimeManager.Instance.OnDayChanged += DayHasChanged;
        }
        private void UnSubscribeToDayChanged()
        {
            TimeManager.Instance.OnDayChanged -= DayHasChanged;
        }
        private void DayHasChanged()
        {
            questInformation.QuestTimeLimit -= 1;
            if(questInformation.QuestTimeLimit <= 0)
            {
                IsQuestFailed = true;

                QuestFailedEvent.Invoke();
                
                CheckQuestStatus();
            }
        }
        */
        
        /// <summary> Check all Objectives in Quest </summary>
        private void CheckIfAllObjectiveCompleted()
        {
            if(IsQuestActive)
            {
                IsQuestCompleted = QuestObjectives.All(objective => objective.IsObjectiveCompleted);

                if(IsQuestCompleted)
                {
                    CheckQuestStatus();
                }
            }
        }

        private void CheckIfAnyObjectiveFailed()
        {
            if(IsQuestActive)
            {
                IsQuestFailed = QuestObjectives.Any(objective => objective.IsObjectiveFailed);

                if(IsQuestFailed)
                {
                    CheckQuestStatus();
                }
            }
        }

        
        public void InitializeQuest() //For starting/initializing the quest
        {
            IsQuestActive = true;

            foreach(Objective objective in QuestObjectives)
            {
                objective.InitializeObjective();

                //Add listener to ObjectiveCompleted UnityEvent so that everytime ObjectiveCompleted get invoked, CheckObjective get ran
                //objective.ObjectiveCompletedEvent.AddListener(delegate { CheckAllObjectives(); }); 
                objective.OnObjectiveCompletedEvent += CheckIfAllObjectiveCompleted; 
                objective.OnObjectiveFailedEvent += CheckIfAnyObjectiveFailed;
            }

            OnQuestActivatedEvent?.Invoke(this);
        }

        public void DeinitializeQuest()
        {
            IsQuestActive = false;

            foreach(Objective objective in QuestObjectives)
            {
                objective.DeInitializeObjective();
                //objective.ObjectiveCompletedEvent.RemoveAllListeners();
                objective.OnObjectiveCompletedEvent -= CheckIfAllObjectiveCompleted;
                objective.OnObjectiveFailedEvent -= CheckIfAnyObjectiveFailed;

            }

            //UnSubscribeToDayChanged();
        }


        /// <summary> Check wether quest is completed or failed </summary>
        private void CheckQuestStatus()
        {
            if(IsQuestActive)
            {
                if(IsQuestCompleted)
                {
                    RewardOnQuestCompleted();
                    
                    DeinitializeQuest();

                    OnQuestCompletedEvent?.Invoke(this);
                }
                else if(IsQuestFailed)
                {
                    RewardOnQuestFailed();
                    
                    DeinitializeQuest();

                    OnQuestFailedEvent?.Invoke(this);
                }
                else
                {
                    Debug.Log("This Quest is "+IsQuestActive);
                    Debug.Log("This Quest is not yet completed or failed!");
                }
            }
        }

        /// <summary> Invoke all event to reward player if not null </summary>
        private void RewardOnQuestCompleted()
        {
            if(ExecutedEventOnQuestCompleted != null)
            {
                foreach(QuestResultEvent Event in ExecutedEventOnQuestCompleted)
                {
                    Event.InvokeQuestResultEvent();
                }
            }
        }

        /// <summary> Invoke all event to punish player if not null </summary>
        private void RewardOnQuestFailed()
        {
            if(ExecutedEventOnQuestFailed != null)
            {
                foreach(QuestResultEvent Event in ExecutedEventOnQuestFailed)
                {
                    Event.InvokeQuestResultEvent();
                }
            }
        }
        
        /// <summary> sending all collected data to objective </summary>
        public void SendProgressFromQuestToObjectives(object sendedData)
        {
            foreach(Objective questObjective in QuestObjectives)
            {
                questObjective.AddProgressToObjective(sendedData);
            } 
        }
        
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

        QuestRewardListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.ExecutedEventOnQuestCompleted));

        QuestPunishmentListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.ExecutedEventOnQuestFailed));

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


