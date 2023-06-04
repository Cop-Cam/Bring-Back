using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaManager : GenericSingletonClass<EncyclopediaManager>, IMenuHandler
{
    private List<FishItemData> endemicFishes = new List<FishItemData>();
    private List<FishItemData> invansiveFishes = new List<FishItemData>();
    private bool isEncyclopediaOpened;

    [Header("Main Canvas Setting")]
    [SerializeField] private GameObject EncyclopediaCanvas;

    [Header("Main Menu Setting")]
    [SerializeField] private GameObject EncyclopediaMainMenuContent;

    [Header("Fish Menu Setting")]
    [SerializeField] private GameObject EncyclopediaFishMenuContent;
    [SerializeField] private GameObject FishMenuGrid;
    [SerializeField] private GameObject FishMenuButtonPrefab;

    [Header("Fish Information Menu Setting")]
    [SerializeField] private GameObject EncyclopediaFishInformationMenuCanvas;
    [SerializeField] private TextMeshProUGUI FishNameText;
    [SerializeField] private Image FishImage;
    [SerializeField] private TextMeshProUGUI FishDescriptionText;
    [SerializeField] private TextMeshProUGUI FishTypeText;
    [SerializeField] private TextMeshProUGUI FishLocationText;
    [SerializeField] private TextMeshProUGUI FishPointText;
    [SerializeField] private TextMeshProUGUI FishCostText;


    public override void Awake()
    {
        base.Awake();
        UIManager.Instance.RegisterMenu(this, EncyclopediaCanvas);
    }

    private void SortingFishesData()
    {
        ClearFishesData();

        foreach(FishItemData fishItem in GameDatabase.Instance.DB_FishItems.Values)
        {
            if(fishItem.fishTypes == FishItemData.FishTypes.Endemic)
            {
                endemicFishes.Add(fishItem);
                continue;
            }

            if(fishItem.fishTypes == FishItemData.FishTypes.Invansive)
            {
                invansiveFishes.Add(fishItem);
                continue;
            }
        }
    }
    private void ClearFishesData()
    {
        endemicFishes.Clear();
        invansiveFishes.Clear();
    }


    #region IMenuHandlerImplementation
    public void OpeningMenu()
    {
        OpenEncyclopediaUIMethod();
    }
    public void ClosingMenu()
    {
        CloseEncyclopediaUIMethod();
    }
    #endregion

    public void OpenEncyclopediaUI()
    {
        if(isEncyclopediaOpened)
        {
            CloseEncyclopediaUI();
        }
        UIManager.Instance.OpenMenu(this);
    }
    public void CloseEncyclopediaUI()
    {
        UIManager.Instance.CloseMenu(this);
    }

    private void OpenEncyclopediaUIMethod()
    {
        isEncyclopediaOpened = true;

        //redirect ui to main menu first
        EncyclopediaMainMenuContent.SetActive(true);
        EncyclopediaFishMenuContent.SetActive(false);
        EncyclopediaFishInformationMenuCanvas.SetActive(false);

        SortingFishesData();
    }
    private void CloseEncyclopediaUIMethod()
    {
        isEncyclopediaOpened = false;

        //close all menus
        EncyclopediaMainMenuContent.SetActive(false);
        EncyclopediaFishMenuContent.SetActive(false);
        EncyclopediaFishInformationMenuCanvas.SetActive(false);

        ClearFishesData();
    }

    public void OpenInvansiveTab()
    {
        OpenEncyclopediaFishMenu();

        SettingUpEndemicTab(invansiveFishes);
    }
    public void OpenEndemicTab()
    {
        OpenEncyclopediaFishMenu();

        SettingUpEndemicTab(endemicFishes);
    }
    private void OpenEncyclopediaFishMenu()
    {
        EncyclopediaMainMenuContent.SetActive(false);
        EncyclopediaFishInformationMenuCanvas.SetActive(false);
        EncyclopediaFishMenuContent.SetActive(true);
    }

    public void OpenEncyclopediaFishInformationMenu(FishItemData fishItem)
    {
        OpenEncyclopediaFishInformationMenuMethod(fishItem);
    }
    public void CloseEncyclopediaFishInformationMenu()
    {
        CloseEncyclopediaFishInformationMenuMethod();
    }

    private void OpenEncyclopediaFishInformationMenuMethod(FishItemData fishItem)
    {
        SettingUpSelectedFishInformation(fishItem);

        EncyclopediaFishMenuContent.SetActive(false);
        EncyclopediaFishInformationMenuCanvas.SetActive(true);
    }
    private void CloseEncyclopediaFishInformationMenuMethod()
    {
        //ClearingUpFishInformation();

        EncyclopediaFishInformationMenuCanvas.SetActive(false);
        EncyclopediaFishMenuContent.SetActive(true);
    }
    
    private void SettingUpSelectedFishInformation(FishItemData fishItem)
    {
        FishNameText.text = fishItem.displayName;
        FishImage.sprite = fishItem.icon;
        FishDescriptionText.text = fishItem.fishDescription;
        FishTypeText.text = fishItem.fishTypes.ToString();
        FishLocationText.text = "Game Designer tidak bilang";
        FishPointText.text = fishItem.fishPoint.ToString();
        FishCostText.text = fishItem.itemSellPrice.ToString();
    }
    // private void ClearingUpFishInformation()
    // {

    // }

    private void ClearFishMenuGrid()
    {
        foreach(Transform FishMenuButtonPrefab in FishMenuGrid.transform)
        {
            GameObject.Destroy(FishMenuButtonPrefab.gameObject);
        }
    }
    private void SettingUpEndemicTab(List<FishItemData> endemicFishes)
    {
        ClearFishMenuGrid();

        foreach(FishItemData fishItem in endemicFishes)
        {
            var button = Instantiate(FishMenuButtonPrefab, FishMenuGrid.transform);

            if(fishItem.GetFishDiscoveredStatus() == false)
            {
                continue;
            }

            button.GetComponentInChildren<Image>().sprite = fishItem.icon;
            button.GetComponent<ButtonScript>().onClick.AddListener(() => OpenEncyclopediaFishInformationMenu(fishItem));
        }
    }
    private void SettingUpInvansiveTab(List<FishItemData> invansiveFishes)
    {
        ClearFishMenuGrid();

        foreach(FishItemData fishItem in invansiveFishes)
        {
            var button = Instantiate(FishMenuButtonPrefab, FishMenuGrid.transform);

            if(fishItem.GetFishDiscoveredStatus() == false)
            {
                continue;
            }

            button.GetComponentInChildren<Image>().sprite = fishItem.icon;
            button.GetComponent<ButtonScript>().onClick.AddListener(() => OpenEncyclopediaFishInformationMenu(fishItem));
        }
    }
    
}
