using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Journal : GenericSingletonClass<Journal>
{
    [SerializeField]private QuestSystem.Quest testingQuest;
    private QuestSystem.QuestManager questManager;

    [SerializeField] private GameObject QuestButtonPrefab;
    [SerializeField] private GameObject QuestButtonGrid;
    [SerializeField] private GameObject QuestObjectivePrefab;
    [SerializeField] private GameObject QuestObjectiveGrid;
    [SerializeField] private GameObject JournalCanvas;

    private void Start()
    {
        questManager = QuestSystem.QuestManager.Instance;
        testingQuest.InitializeQuest();
        //questManager.InitializeQuest("1");
        OpenJournal();
    }

    public void OpenJournal()
    {
        JournalCanvas.SetActive(true);
        OpenActiveQuestJournalTab();
    }
    
    public void CloseJournal()
    {
        ClearQuestButtonGrid();
        ClearQuestObjectiveGrid();

        JournalCanvas.SetActive(false);
    }
    
    private void ClearQuestButtonGrid()
    {
        foreach(Transform QuestButtonPrefab in QuestButtonGrid.transform)
        {
            GameObject.Destroy(QuestButtonPrefab.gameObject);
        }
    }

    //spawn objective and information of quest
    private void OnQuestButtonClickedEvent(QuestSystem.Quest currentClickedQuest)
    {
        //Debug.Log("quest button clicked");
        ClearQuestObjectiveGrid();
        
        foreach(QuestSystem.Objective objective in currentClickedQuest.questSetting.QuestObjectives)
        {
            if(objective is QuestSystem.CollectFishesPoint)
            {
                QuestSystem.CollectFishesPoint fishPointObjective = objective as QuestSystem.CollectFishesPoint;
                var objectiveUI = Instantiate(QuestObjectivePrefab, QuestObjectiveGrid.transform);

                objectiveUI//.transform.Find("Canvas")
                    .transform.Find("Background")
                    .transform.Find("ObjectiveTypeText")
                    .GetComponent<TextMeshProUGUI>().text = fishPointObjective.fishObjectiveSetting.fishType.ToString();

                objectiveUI//.transform.Find("Canvas")
                    .transform.Find("Background")
                    .transform.Find("ProgressText")
                    .GetComponent<TextMeshProUGUI>().text = 
                    string.Format("{0}/{1}", fishPointObjective.GetObjectiveProgression(), fishPointObjective.fishObjectiveSetting.FishObjectivePointNeeded);

                objectiveUI//.transform.Find("Canvas")
                    .transform.Find("Background")
                    .transform.Find("Slider")
                    .GetComponent<Slider>().value = fishPointObjective.GetObjectiveProgression();

                objectiveUI//.transform.Find("Canvas")
                    .transform.Find("Background")
                    .transform.Find("Slider")
                    .GetComponent<Slider>().value = fishPointObjective.fishObjectiveSetting.FishObjectivePointNeeded;
            }
        }
    }

    private void ClearQuestObjectiveGrid()
    {
        foreach(Transform QuestDescriptionPrefab in QuestObjectiveGrid.transform)
        {
            GameObject.Destroy(QuestDescriptionPrefab.gameObject);
        }
    }

    public void OpenActiveQuestJournalTab()
    {
        ClearQuestButtonGrid();

        foreach(QuestSystem.Quest quest in questManager.CurrentActivatedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button//.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;

            //if(quest.questSetting.QuestObjectives.Contains(QuestSystem.Objective objective is TimeLimited))
            if (quest.questSetting.QuestObjectives.Any(objective => objective is QuestSystem.TimeLimited))
            {
                var timeLimitedObjective = quest.questSetting.QuestObjectives.OfType<QuestSystem.TimeLimited>().FirstOrDefault();

                button//.transform.Find("Canvas")
                    .transform.Find("TimelimitBG")
                    .transform.Find("TimelimitText")
                    .GetComponent<TextMeshProUGUI>().text = timeLimitedObjective.GetObjectiveProgression().ToString();
            }

            button.GetComponent<ButtonScript>().onClick.AddListener(() => OnQuestButtonClickedEvent(quest));
        }
    }

    public void OpenFailedQuestJournalTab()
    {
        ClearQuestButtonGrid();

        foreach(QuestSystem.Quest quest in questManager.CurrentFailedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button//.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;

            button.GetComponent<ButtonScript>().onClick.AddListener(() => OnQuestButtonClickedEvent(quest));
            
        }
    }

    public void OpenCompletedQuestJournalTab()
    {
        ClearQuestButtonGrid();

        foreach(QuestSystem.Quest quest in questManager.CurrentCompletedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button//.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;

            button.GetComponent<ButtonScript>().onClick.AddListener(() => OnQuestButtonClickedEvent(quest));
            
        }
    }
}
