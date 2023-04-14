
using UnityEngine;
using TMPro;

public class StatusUIController : GenericSingletonClass<StatusUIController>
{
    [SerializeField] private TextMeshProUGUI currentGoldText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        currentGoldText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
    }

    private void OnEnable() 
    {
        PlayerResourceManager.OnMoneyChange += UpdateMoney;
    }

    private void OnDisable() 
    {
        //if(PlayerResourceManager.Instance == null) return;
        PlayerResourceManager.OnMoneyChange -= UpdateMoney;
    }
}
