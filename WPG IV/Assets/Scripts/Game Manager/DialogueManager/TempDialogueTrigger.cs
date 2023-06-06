using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime; //using ink

public class TempDialogueTrigger : MonoBehaviour
{
    [SerializeField] private bool isStartingFromStarMethod;
    [SerializeField] private TextAsset inkJSON; //asset cerita
    
    private void Start()
    {
        if(isStartingFromStarMethod)
        {
            TempDialogueManager.Instance.EnterDialogue(inkJSON);

            StartCoroutine(SceneLoader.Instance.LoadSceneAsyncWaitForSecondTimescaled("Demo", 1f));
        }
    }
}

