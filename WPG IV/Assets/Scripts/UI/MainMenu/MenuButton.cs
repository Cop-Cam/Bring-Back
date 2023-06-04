using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
    [SerializeField] GameObject deactivate;
    [SerializeField] GameObject activate;

    private bool click = false;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1 || click == true) // press enter
            {
                animator.SetBool("pressed", true);
                click = false;
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;

                if (thisIndex == 0)
                {
                    // start game
                    Debug.Log("start game");

                    SceneLoader.Instance.LoadSceneAsync("Demo");
                    
                    StartCoroutine(TransitionManager.Instance.StartTransition());
                }
                else if (thisIndex == 1)
                {
                    // open setting
                    StartCoroutine(OpenSettings());
                    Debug.Log("setting opened");
                }
                else if (thisIndex == 2)
                {
                    Application.Quit();
                    Debug.Log("application quit");
                }
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator OpenSettings()
    {
        yield return new WaitForSeconds(.6f);

        // active and deactive panel/gameobject menu
        deactivate.SetActive(false);
        activate.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuButtonController.index = thisIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!click)
            click = true;
    }
}