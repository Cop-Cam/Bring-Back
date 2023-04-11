using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LakeUIController : GenericSingletonClass<LakeUIController>
{
    private LakeInventory lakeInventory;
    
    [Header("GameObject For UI")]
    [SerializeField] private GameObject LakeInventoryUI;
    
    [SerializeField] private TextMeshProUGUI CurrentPlayerMoneyText;

    [SerializeField] private Image CurrentFishImages;


    // [Header("Insert A Prefab for Collect Button and The Grid Which The Button Will Be Instantiated")]
    // [SerializeField] private GameObject CollectButtonPrefab;
    // [SerializeField] private GameObject CollectButtonGridLayout;

    // [Header("Insert A Prefab for Sell Button and The Grid Which The Button Will Be Instantiated")]
    // [SerializeField] private GameObject SellButtonPrefab;
    // [SerializeField] private GameObject SellButtonGridLayout;

    [SerializeField] private GameObject CollectButton;
    [SerializeField] private GameObject SellButton;
    

    public bool isUIOpened { get; private set; }
    // Start is called before the first frame update
    private void Start()
    {
        if(LakeInventoryUI==null) LakeInventoryUI = UIManager.Instance.transform.parent.Find("LakeInventoryUI").gameObject;

        LakeInventoryUI.SetActive(false);
    }

    private IEnumerator UpdateUIStatus()
    {
        CurrentPlayerMoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();

        yield return null;
    }

    public void OpenLakeUI(LakeInventory lakeInventory)
    {
        InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false);

        this.lakeInventory = lakeInventory;

        SettingUpUiInformation();
        
        SettingUpButton();

        StartCoroutine(UpdateUIStatus());
        
        LakeInventoryUI.SetActive(true);
    }

    public void CloseLakeUI()
    {
        LakeInventoryUI.SetActive(false);

        //ClearingUpButton();

        this.lakeInventory = null;
     
        StopAllCoroutines();

        InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(true);
        
    }

    private void ButtonEventSellItem()
    {
        if(lakeInventory.GetCurrentSavedItemData() != null)
        {
            InventoryItemData soldItem = lakeInventory.RemoveItem();
            PlayerResourceManager.Instance.ChangeMoney(soldItem.itemSellPrice);
        }
    }
    private void ButtonEventCollectItem()
    {
        if(lakeInventory.GetCurrentSavedItemData() != null)
        {
            InventoryItemData collectedItem = lakeInventory.RemoveItem();
            QuestSystem.QuestManager.Instance.SendProgressFromQuestManagerToQuest(collectedItem);
        }
    }

    private void SettingUpButton()
    {
        SettingUpSellButton();
        SettingUpCollectButton();
    }

    private void SettingUpCollectButton()
    {
        // CollectButtonPrefab.transform.Find("Icon").GetComponent<Image>().sprite = lakeInventory.GetCurrentSavedItemData().icon;
        // CollectButtonPrefab.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventCollectItem());
        // Instantiate(CollectButtonPrefab, CollectButtonGridLayout.transform);

        FishItemData fish = lakeInventory.GetCurrentSavedItemData() as FishItemData;
        CollectButton.transform.Find("Icon").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = string.Format("{0}: +{1}", fish.fishTypes, fish.fishPoint);
        CollectButton.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventCollectItem());
    }
    private void SettingUpSellButton()
    {
        // SellButtonPrefab.transform.Find("Harga").transform.Find("HargaText").GetComponent<TextMeshProUGUI>().text = lakeInventory.GetCurrentSavedItemData().itemSellPrice.ToString();
        // SellButtonPrefab.transform.Find("Icon").GetComponent<Image>().sprite = lakeInventory.GetCurrentSavedItemData().icon;
        // SellButtonPrefab.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventSellItem());
        // Instantiate(SellButtonPrefab, SellButtonGridLayout.transform);

        FishItemData fish = lakeInventory.GetCurrentSavedItemData() as FishItemData;
        SellButton.transform.Find("Icon").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = string.Format("Gold: +{0}", fish.itemSellPrice);
        CollectButton.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventCollectItem());
    }

    private void SettingUpUiInformation()
    {
        FishItemData fish = lakeInventory.GetCurrentSavedItemData() as FishItemData;
        CurrentFishImages.sprite = fish.icon;
    }
    // private void ClearingUpButton()
    // {
    //     ClearingUpSellGrid();
    //     ClearingUpCollectGrid();
    // }

    // private void ClearingUpSellGrid()
    // {
    //     foreach(Transform SellButtonPrefab in SellButtonGridLayout.transform)
    //     {
    //         GameObject.Destroy(SellButtonPrefab.gameObject);
    //     }
    // }
    // private void ClearingUpCollectGrid()
    // {
    //     foreach(Transform CollectButtonPrefab in CollectButtonGridLayout.transform)
    //     {
    //         GameObject.Destroy(CollectButtonPrefab.gameObject);
    //     }
    // }
}
