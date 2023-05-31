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
        //Debug.Log("trying to sleep");
        InputManager.Instance.IsPlayerAllowedToInteract(false);
        InputManager.Instance.IsPlayerAllowedToMove(false);
        ShowSleepUIConfirmation(true);
    }

    public void Sleep()
    {
        // ShowSleepUIConfirmation(false);

        // //StartCoroutine("SleepTransition");
        // SleepTransition();

        // TimeManager.Instance.ChangeDay();

        // InputManager.Instance.IsPlayerAllowedToInteract(true);
        // InputManager.Instance.IsPlayerAllowedToMove(true);
        PlayerResourceManager.Instance.ResetEnergy();
        StartCoroutine(SleepCoroutine());
    }

    private IEnumerator SleepCoroutine()
    {
        Time.timeScale = 0f;
        ShowSleepUIConfirmation(false);

        //StartCoroutine("SleepTransition");
        SleepTransition();

        TimeManager.Instance.ChangeDay();
        
        yield return new WaitForSecondsRealtime(2.5f);
        
        InputManager.Instance.IsPlayerAllowedToInteract(true);
        InputManager.Instance.IsPlayerAllowedToMove(true);
        
        Time.timeScale = 1f;
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

    private void SleepTransition()
    {
        StartCoroutine(TransitionManager.Instance.StartTransition());
        //yield return new WaitForSeconds(1);
    }
}
