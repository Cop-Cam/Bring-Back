using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    //public static GameManager instance { get; private set; }

    //[SerializeField] private PlayerResourceManager playerResourceManager;
    //public GameObject other; //

    // void Awake()
    // {
    //     if(instance != null)
    //     {
    //         Debug.Log("there is another Game Manager");
    //     }
    //     instance = this;

        
    // }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Untuk mematikan atau menghidupkan pergerakkan pemain
    public void IsPlayerAllowedToMove(bool isAllowed)
    {
        if(isAllowed)
        {
            PlayerController.Instance.enabled = true;
            PlayerInteractor.Instance.enabled = true;
        }
        else if(!isAllowed)
        {
            PlayerController.Instance.enabled = false;
            PlayerInteractor.Instance.enabled = false;
        }
        
    }
}
