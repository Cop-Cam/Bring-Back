using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : DontDestroyOnLoadSingletonClass<GameManager>, IMenuHandler
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private GameObject PauseMenuCanvas;

    private bool isGamePaused;

    public override void Awake()
    {
        base.Awake();
        UIManager.Instance.RegisterMenu(this, PauseMenuCanvas);
    }

    private void Start()
    {
        //PauseMenuCanvas.SetActive(false);
    }

    #region IMenuHandlerImplementation
    public void OpeningMenu()
    {
        OpenPauseMenuMethod();
    }
    public void ClosingMenu()
    {
        ClosePauseMenuMethod();
    }
    #endregion

    public void OpenPauseMenu()
    {
        UIManager.Instance.OpenMenu(this);
    }
    public void ClosePauseMenu()
    {
        UIManager.Instance.CloseMenu(this);
    }

    private void OpenPauseMenuMethod()
    {
        // if(isGamePaused)
        // {
        //     ClosePauseMenu();
        //     return;
        // }

        //UIManager.Instance.OpenMenu(this);
        // PauseGame(true);
        // PauseMenuCanvas.SetActive(true);
        
        UpdateMoneyInPauseMenu();
        PlayerResourceManager.OnMoneyChange += UpdateMoneyInPauseMenu;
    }
    private void ClosePauseMenuMethod()
    {
        PlayerResourceManager.OnMoneyChange -= UpdateMoneyInPauseMenu;

        // PauseMenuCanvas.SetActive(false);
        // PauseGame(false);

        //UIManager.Instance.CloseMenu(this);
    }

    private void UpdateMoneyInPauseMenu()
    {
        MoneyText.text = PlayerResourceManager.Instance.PlayerMoney.ToString();
    }

    public void PauseGame(bool shouldGameBePaused)
    {
        isGamePaused = shouldGameBePaused;
        
        if(shouldGameBePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        //InputManager.Instance.IsPlayerAllowedToInteract(shouldGameBePaused);
        InputManager.Instance.IsPlayerAllowedToMove(!shouldGameBePaused);
    }


}
