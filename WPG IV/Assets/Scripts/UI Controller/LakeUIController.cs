using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LakeUIController : GenericSingletonClass<LakeUIController>, IMenuHandler
{
    private LakeInventory lakeInventory;
    [SerializeField] private GameObject LakeInventoryCanvas;
    //[SerializeField] private TextMeshProUGUI CurrentPlayerMoneyText;
    [SerializeField] private Image CurrentFishImages;
    [SerializeField] private TextMeshProUGUI CurrentFishName;
    [SerializeField] private GameObject CollectButton;
    [SerializeField] private GameObject SellButton;


    public override void Awake()
    {
        base.Awake();

        UIManager.Instance.RegisterMenu(this, LakeInventoryCanvas);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // if(LakeInventoryCanvas==null) LakeInventoryCanvas = UIManager.Instance.transform.GetComponentsInChildren<Transform>()
        //                      .FirstOrDefault(c => c.gameObject.name == "LakeUICanvas")?.gameObject;

        //LakeInventoryCanvas.SetActive(false);
    }


    #region IMenuHandlerImplementation
    public void OpeningMenu()
    {
        OpenLakeUIMethod();
    }
    public void ClosingMenu()
    {
        CloseLakeUIMethod();
    }
    #endregion

    public void OpenLakeUI(LakeInventory lakeInventory)
    {
        this.lakeInventory = lakeInventory;
     
        UIManager.Instance.OpenMenu(this);
    }

    public void CloseLakeUI()
    {
        UIManager.Instance.CloseMenu(this);
     
        this.lakeInventory = null;
    }

    private void OpenLakeUIMethod()
    {
        //GameManager.Instance.PauseGame(true);

        // this.lakeInventory = lakeInventory;

        SettingUpUiInformation();
        
        SettingUpButton();
        
        //LakeInventoryCanvas.SetActive(true);

        //UIManager.Instance.OpenMenu(this);
    }

    private void CloseLakeUIMethod()
    {
        //LakeInventoryCanvas.SetActive(false);

        //UIManager.Instance.CloseMenu(this);

        ClearingUpButton();

     
        //StopAllCoroutines();

        InputManager.Instance.IsPlayerAllowedToInteract(true);
        // InputManager.Instance.IsPlayerAllowedToMove(true);      

        //GameManager.Instance.PauseGame(false);
    }

    private void ButtonEventSellItem()
    {
        if(lakeInventory.GetCurrentSavedItemData() != null)
        {
            InventoryItemData soldItem = lakeInventory.RemoveItem();

            if(soldItem is IDiscoverable discoverable && !discoverable.isItemDiscovered())
            {
                discoverable.UpdateDiscoveredStatus(true);
            }

            PlayerResourceManager.Instance.ChangeMoney(soldItem.itemSellPrice);

            //delete the instance
            UnityEngine.Object.Destroy(soldItem);
        }
        
        CloseLakeUI();
    }
    private void ButtonEventCollectItem()
    {
        if(lakeInventory.GetCurrentSavedItemData() != null)
        {
            InventoryItemData collectedItem = lakeInventory.RemoveItem();
            
            if(collectedItem is IDiscoverable discoverable && !discoverable.isItemDiscovered())
            {
                discoverable.UpdateDiscoveredStatus(true);
            }

            QuestSystem.QuestManager.Instance.SendProgressFromQuestManagerToQuest(collectedItem);

            //delete the instance
            UnityEngine.Object.Destroy(collectedItem);
        }

        CloseLakeUI();
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
        SellButton.GetComponent<ButtonScript>().onClick.AddListener(() => ButtonEventSellItem());
    }

    private void SettingUpUiInformation()
    {
        FishItemData fish = lakeInventory.GetCurrentSavedItemData() as FishItemData;
        CurrentFishImages.sprite = fish.icon;
        CurrentFishName.text = fish.displayName;
    }
    
    private void ClearingUpButton()
    {
        // ClearingUpSellGrid();
        // ClearingUpCollectGrid();
        ClearListener();
    }

    private void ClearListener()
    {
        CollectButton.GetComponent<ButtonScript>().onClick.RemoveAllListeners();
        SellButton.GetComponent<ButtonScript>().onClick.RemoveAllListeners();
    }

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
