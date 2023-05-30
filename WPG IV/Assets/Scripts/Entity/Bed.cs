using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObjects
{
    [SerializeField] private GameObject SleepConfirmationUI;

    protected override void Start()
    {
        base.Start();
        ShowSleepUIConfirmation(false);
    }

    public override void OnInteracted()
    {
        Debug.Log("trying to sleep");
        InputManager.Instance.IsPlayerAllowedToInteract(false);
        InputManager.Instance.IsPlayerAllowedToMove(false);
        ShowSleepUIConfirmation(true);
    }

    public void Sleep()
    {
        TimeManager.Instance.ChangeDay();
        ShowSleepUIConfirmation(false);
        InputManager.Instance.IsPlayerAllowedToInteract(true);
        InputManager.Instance.IsPlayerAllowedToMove(true);
    }

    private void ShowSleepUIConfirmation(bool isActivated)
    {
        SleepConfirmationUI.SetActive(isActivated);
    }

    public void CancelSleep()
    {
        ShowSleepUIConfirmation(false);
        InputManager.Instance.IsPlayerAllowedToInteract(true);
        InputManager.Instance.IsPlayerAllowedToMove(true);
    }
}
