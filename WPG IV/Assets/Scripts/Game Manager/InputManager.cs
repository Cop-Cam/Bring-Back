using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class InputManager : GenericSingletonClass<InputManager>
{
    //public GameObject playerObj;
    public static InputActionAsset playerInputActionMapAsset;
    
    // Start is called before the first frame update
    void Start()
    {
        if(playerInputActionMapAsset == null)
        {
            GetInputAction();
        }
        if(playerInputActionMapAsset == null)
        {
            Debug.LogWarning("InputAction masih kosong");
        }
    }

    

    void GetInputAction()
    {
        string[] assetNames = AssetDatabase.FindAssets("IA_PlayerInputAction", new[]{"Assets/Input Actions"});
        foreach(string inputActionAsset in assetNames)
        {
            var IApath = AssetDatabase.GUIDToAssetPath(inputActionAsset);
            var character = AssetDatabase.LoadAssetAtPath<InputActionAsset>(IApath);
            playerInputActionMapAsset = character;
        }
    }

    //Untuk mematikan atau menghidupkan pergerakkan pemain
    public void IsPlayerAllowedToMove(bool isAllowed)
    {
        if(isAllowed)
        {
            playerInputActionMapAsset.FindActionMap("Player").FindAction("Move").Enable();
        }
        else if(!isAllowed)
        {
            playerInputActionMapAsset.FindActionMap("Player").FindAction("Move").Disable();
        }
        //playerObj.transform.Find("Controller").gameObject.SetActive(isAllowed = !isAllowed);
        // if(isAllowed)
        // {
        //     PlayerController.Instance.enabled = true;
        // }
        // else if(!isAllowed)
        // {
        //     PlayerController.Instance.enabled = false;
        // }
    }

    public void IsPlayerAllowedToInteract(bool isAllowed)
    {
        if(isAllowed)
        {
            playerInputActionMapAsset.FindActionMap("Player").FindAction("Interact").Enable();
        }
        else if(!isAllowed)
        {
            playerInputActionMapAsset.FindActionMap("Player").FindAction("Interact").Disable();
        }
        //playerObj.transform.Find("Interactor").gameObject.SetActive(isAllowed = !isAllowed);

        // if(isAllowed)
        // {
        //     PlayerInteractor.Instance.enabled = true;
        // }
        // else if(!isAllowed)
        // {
        //     PlayerInteractor.Instance.enabled = false;
        // }
    }

    public void IsPlayerAllowedToDoPlayerMapsInput(bool isAllowed)
    {
        IsPlayerAllowedToInteract(isAllowed);
        IsPlayerAllowedToMove(isAllowed);
    }
}
