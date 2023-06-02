using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
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

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // some func
                animator.SetTrigger("rightPressed");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // some func
                animator.SetTrigger("leftPressed");
            }

            // for button apply and back
            if (Input.GetAxis("Submit") == 1 || click == true) // press enter
            {
                animator.SetBool("pressed", true);
                click = false;
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;

                if (thisIndex == 3)
                {
                    // apply setting
                    Debug.Log("Settings applied");
                }
                else if (thisIndex == 4)
                {
                    // open Menu close Setting
                    StartCoroutine(OpenMenu());
                    Debug.Log("Menu opened");
                }
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator OpenMenu()
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