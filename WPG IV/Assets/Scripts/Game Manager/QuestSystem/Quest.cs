using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEditor;


namespace QuestSystem
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Objects/Quest Assets/Quest Data")]
    public class Quest : ScriptableObject
    {
        [System.Serializable]
        public struct QuestSetting
        {
            [Header("Quest Information")]

            [Tooltip("Quest Id [Recommended Format: Questline Id_This Quest Id_This Quest(Index/Stage) In Questline]")]
            public string QuestId;

            [Tooltip("Quest Name")]
            public string QuestName;

            [Tooltip("Quest Description")]
            public string QuestDescription;

            [Header("Quest Stats")]
            
            [Tooltip("All of This Quest Objective")]
            public List<Objective> QuestObjectives;

            [Tooltip("All Executed Events When Completing The Quest")]
            public List<QuestResultEvent> ExecutedEventOnQuestCompleted;

            [Tooltip("All Executed Event When Failing The Quest")]
            public List<QuestResultEvent> ExecutedEventOnQuestFailed;
        }
        public QuestSetting questSetting;

        /*
        [HideInInspector]public List<Objective> QuestObjectives;

        [HideInInspector]public List<QuestResultEvent> ExecutedEventOnQuestCompleted;

        [HideInInspector]public List<QuestResultEvent> ExecutedEventOnQuestFailed;
        */


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
            questSetting.QuestTimeLimit -= 1;
            if(questSetting.QuestTimeLimit <= 0)
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
                IsQuestCompleted = questSetting.QuestObjectives.All(objective => objective.IsObjectiveCompleted);

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
                IsQuestFailed = questSetting.QuestObjectives.Any(objective => objective.IsObjectiveFailed);

                if(IsQuestFailed)
                {
                    CheckQuestStatus();
                }
            }
        }

        
        public void InitializeQuest() //For starting/initializing the quest
        {
            IsQuestActive = true;

            foreach(Objective objective in questSetting.QuestObjectives)
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

            foreach(Objective objective in questSetting.QuestObjectives)
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
            if(questSetting.ExecutedEventOnQuestCompleted != null)
            {
                foreach(QuestResultEvent Event in questSetting.ExecutedEventOnQuestCompleted)
                {
                    Event.InvokeQuestResultEvent();
                }
            }
        }

        /// <summary> Invoke all event to punish player if not null </summary>
        private void RewardOnQuestFailed()
        {
            if(questSetting.ExecutedEventOnQuestFailed != null)
            {
                foreach(QuestResultEvent Event in questSetting.ExecutedEventOnQuestFailed)
                {
                    Event.InvokeQuestResultEvent();
                }
            }
        }
        
        /// <summary> sending all collected data to objective </summary>
        public void SendProgressFromQuestToObjectives(object sendedData)
        {
            foreach(Objective questObjective in questSetting.QuestObjectives)
            {
                questObjective.AddProgressToObjective(sendedData);
            } 
        }
        
    }

}

/*
#if UNITY_EDITOR
[CustomEditor(typeof(QuestSystem.Quest))]
public class QuestEditor : Editor
{
    private SerializedProperty QuestIdProperty;
    private SerializedProperty QuestNameProperty;
    private SerializedProperty QuestDescriptionProperty;

    private List<string> QuestObjectiveType;
    private SerializedProperty QuestObjectiveListProperty;

    private List<string> QuestEventType;
    private SerializedProperty QuestRewardListProperty;
    private SerializedProperty QuestPunishmentListProperty;


    void OnEnable()
    {
        
        QuestIdProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.QuestName));

        QuestNameProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.QuestName));

        QuestDescriptionProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.QuestDescription));
        
        QuestObjectiveListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.QuestObjectives));

        QuestRewardListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.ExecutedEventOnQuestCompleted));
        
        QuestPunishmentListProperty = serializedObject.FindProperty(nameof(QuestSystem.Quest.questSetting.ExecutedEventOnQuestFailed));
        

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
        QuestEventType.AddRange( System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(LookUpOnFailedQuestEvent))
            .Select(type => type.Name)
            .ToList() );
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(QuestIdProperty);
        EditorGUILayout.PropertyField(QuestNameProperty);
        EditorGUILayout.PropertyField(QuestDescriptionProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("List 1:");
        CreateMenu("Quest Objectives",QuestObjectiveType, QuestObjectiveListProperty);
        

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("List 2:");
        CreateMenu("Quest Completed Events",QuestEventType, QuestRewardListProperty);
        

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("List 3:");
        CreateMenu("Quest Failed Events",QuestEventType,QuestPunishmentListProperty);


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
*/







