
using UnityEngine;
using TMPro;

public class StatusUIController : GenericSingletonClass<StatusUIController>
{
    [SerializeField] private TextMeshProUGUI currentGoldText;
    [SerializeField] private TextMeshProUGUI currentEnergyText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
        UpdateEnergy();
    }

    private void UpdateMoney()
    {
        currentGoldText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
    }

    private void UpdateEnergy()
    {
        currentEnergyText.text = PlayerResourceManager.Instance.PlayerEnergy.ToString();
        
        Debug.Log("Current Energy: "+currentEnergyText.text);
    }

    private void OnEnable() 
    {
        PlayerResourceManager.OnMoneyChange += UpdateMoney;
        PlayerResourceManager.OnEnergyChange += UpdateEnergy;
    }

    private void OnDisable() 
    {
        PlayerResourceManager.OnMoneyChange -= UpdateMoney;
        PlayerResourceManager.OnEnergyChange -= UpdateEnergy;
    }
}
