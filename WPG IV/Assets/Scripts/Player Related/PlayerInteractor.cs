using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Place this shit in front of player for spawning fishes
public class PlayerInteractor : GenericSingletonClass<PlayerInteractor>
{
    //public static PlayerInteractor instance; 
    private bool isInObject;
    private GameObject InteractedGameObject;

    // void Awake() 
    // {
    //     if(instance != null)
    //     {
    //         Debug.Log("there is another PlayerInteractor");
    //     }
    //     instance = this;
    //     // Vector3 playerPosition = transform.parent.position;
    //     // transform.position += new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 0.5f);
    //     // Debug.Log(transform.position);
    //     // Debug.Log(transform.parent.position);
        
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //isFiring = context.ReadValue<float>();
        if(context.performed)
        {
            Interact();
        }
    }

    //void SpawnMethod()
    //{
     //   Instantiate(ObjectToSpawn, transform.position, transform.rotation);
    //}
    public void Interact()
    {
        if(isInObject)
        {
            Debug.Log("Object is Interacted");
            if(InteractedGameObject.CompareTag("Ponds"))
            {
                GameManager.Instance.IsPlayerAllowedToMove(false); //mematikan pergerakkan pemain
                GameObject InteractedObjectParent = InteractedGameObject.transform.parent.gameObject;
                LocalInventory localInventory = InteractedObjectParent.GetComponentInChildren<LocalInventory>();
                ShopSystem.Instance.OpenShopMenu(localInventory); 
            }
        }
    }
    

    private void OnTriggerEnter(Collider other) 
    {
        //if(other.CompareTag("InteractableObjects"))
        //{
            isInObject = true;
            InteractedGameObject = other.gameObject;
        //}
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("InteractableObjects"))
        {
            isInObject = false;
            InteractedGameObject = null;
        }   
    }
}
