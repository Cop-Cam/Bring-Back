using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Journal : GenericSingletonClass<Journal>
{
    private QuestSystem.QuestManager questManager;

    [SerializeField] private GameObject QuestButtonPrefab;
    [SerializeField] private GameObject QuestButtonGrid;
    [SerializeField] private GameObject QuestDescriptionPrefab;
    [SerializeField] private GameObject QuestDescriptionGrid;

    private void Start()
    {
        questManager = QuestSystem.QuestManager.Instance;
    }

    public void OpenJournal()
    {
        OpenActiveQuestJournalTab();
    }

    public void OpenActiveQuestJournalTab()
    {
        foreach(QuestSystem.Quest quest in questManager.CurrentActivatedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;
        }
    }

    public void OpenFailedQuestJournalTab()
    {
        foreach(QuestSystem.Quest quest in questManager.CurrentFailedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;
        }
    }

    public void OpenCompletedQuestJournalTab()
    {
        foreach(QuestSystem.Quest quest in questManager.CurrentCompletedQuestList)
        {
            var button = Instantiate(QuestButtonPrefab, QuestButtonGrid.transform);

            button.transform.Find("Canvas")
                .transform.Find("Background")
                .transform.Find("QuestName")
                .GetComponent<TextMeshProUGUI>().text = quest.questSetting.QuestName;
        }
    }
}
