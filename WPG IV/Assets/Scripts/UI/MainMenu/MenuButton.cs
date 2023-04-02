using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1) // press enter
            {
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;

                if (thisIndex == 0)
                {
                    // start game
                    Debug.Log("start game");
                }
                else if (thisIndex == 1)
                {
                    // open setting
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
}
