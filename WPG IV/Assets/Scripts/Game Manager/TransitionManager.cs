using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : GenericSingletonClass<TransitionManager>
{
    [SerializeField] private GameObject TransitionCanvas;
    [SerializeField] private Animator TransitionAnimator;
    
    private void Start()
    {
        TransitionCanvas.SetActive(false);
    }

    public IEnumerator StartTransition()
    {
        TransitionCanvas.SetActive(true);
        
        TransitionAnimator.Play("Crossfade_Start");
        yield return new WaitForSecondsRealtime(1f);

        //Time.timeScale = 0f;
        //yield return new WaitForSeconds(0.5f);
        //Time.timeScale = 1f;

        TransitionAnimator.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1f);

        TransitionCanvas.SetActive(false);
    }
}
