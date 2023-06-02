using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Journal : GenericSingletonClass<Journal>, IMenuHandler
{
    private QuestSystem.QuestManager questManager;

    [SerializeField] private GameObject QuestButtonPrefab;
    [SerializeField] private GameObject QuestButtonGrid;
    [SerializeField] private GameObject QuestObjectivePrefab;
    [SerializeField] private GameObject QuestObjectiveGrid;
    [SerializeField] private GameObject QuestName;
    [SerializeField] private GameObject QuestDescriptionArea;
    [SerializeField] private GameObject QuestObjectiveArea;
    [SerializeField] private GameObject JournalCanvas;

    private bool IsJournalOpened;

    private void Start()
    {
        UIManager.Instance.RegisterMenu(this, JournalCanvas);

        questManager = QuestSystem.QuestManager.Instance;
        JournalCanvas.SetActive(false);
        TurnOnRightPage(false);
        //OpenJournal();
    }

    #region IMenuHandlerImplementation
    public void OpeningMenu()
    {
        OpenJournalMethod();
    }
    public void ClosingMenu()
    {
        CloseJournalMethod();
    }
    #endregion

    //dipanggil oleh siapapun yang mau membuka
    public void OpenJournal()
    {
        UIManager.Instance.OpenMenu(this);
    }
    public void CloseJournal()
    {
        UIManager.Instance.CloseMenu(this);
    }

    private void OpenJournalMethod()
    {
        //GameManager.Instance.ClosePauseMenu();

        if(IsJournalOpened)
        {
            CloseJournal();
            return;
        }
        //shouldGameBePaused(true);

        TurnOnRightPage(false);

        //JournalCanvas.SetActive(true);
        OpenActiveQuestJournalTab();
    }
    
    private void CloseJournalMethod()
    {
        //shouldGameBePaused(false);

        ClearQuestButtonGrid();
        ClearQuestObjectiveGrid();
        
        TurnOnRightPage(false);

        //JournalCanvas.SetActive(false);
    }
    

    private void ClearQuestButtonGrid()
    {
        foreach(Transform QuestButtonPrefab in QuestButtonGrid.transform)
        {
            GameObject.Destroy(QuestButtonPrefab.gameObject);
        }
    }

    private void TurnOnRightPage(bool condition)
    {
        QuestName.SetActive(condition);
        QuestDescriptionArea.SetActive(condition);
        QuestObjectiveArea.SetActive(condition);
    }

    //spawn objective and information of quest
    private void OnQuestButtonClickedEvent(QuestSystem.Quest currentClickedQuest)
    {
        //Debug.Log("quest button clicked");
        ClearQuestObjectiveGrid();

        //setting up quest information
        TurnOnRightPage(true);
        //QuestName.SetActive(true);
        QuestName.transform.GetComponentInChildren<TextMeshProUGUI>().text = currentClickedQuest.questSetting.QuestName;
        //QuestDescriptionArea.SetActive(true);
        QuestDescriptionArea.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = currentClickedQuest.questSetting.QuestDescription;
        
        //setting up objective of quest
        foreach(QuestSystem.Objective objective in currentClickedQuest.questSetting.QuestObjectives)
        {
            if(objective is QuestSystem.CollectFishesPoint)
            {
                QuestSystem.CollectFishesPoint fishPointObjective = objective as QuestSystem.CollectFishesPoint;
                var objectiveUI = Instantiate(QuestObjectivePrefab, QuestObjectiveGrid.transform);

                objectiveUI.transform.Find("Panel")
                    //.transform.Find("Background")
                    .transform.Find("ObjectiveTypePanel")
                    .GetComponentInChildren<TextMeshProUGUI>().text = fishPointObjective.fishObjectiveSetting.fishType.ToString();

                objectiveUI.transform.Find("Panel")
                    //.transform.Find("Background")
                    .transform.Find("ProgressPanel")
                    .GetComponentInChildren<TextMeshProUGUI>().text = 
                    string.Format("{0}/{1}", fishPointObjective.GetObjectiveProgression(), fishPointObjective.fishObjectiveSetting.FishObjectivePointNeeded);

                objectiveUI.transform.Find("Panel")
                    //.transform.Find("Background")
                    .transform.Find("SliderPanel")
                    .GetComponentInChildren<Slider>().value = fishPointObjective.GetObjectiveProgression();

                objectiveUI.transform.Find("Panel")
                    //.transform.Find("Background")
                    .transform.Find("SliderPanel")
                    .GetComponentInChildren<Slider>().maxValue = fishPointObjective.fishObjectiveSetting.FishObjectivePointNeeded;
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
