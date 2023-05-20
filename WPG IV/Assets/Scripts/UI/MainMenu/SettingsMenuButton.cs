using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;

    private bool click = false;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetTrigger("rightPressed");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                animator.SetTrigger("leftPressed");
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
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