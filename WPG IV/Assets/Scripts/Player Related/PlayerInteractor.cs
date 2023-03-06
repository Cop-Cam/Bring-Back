using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Place this shit in front of player for spawning fishes
public class PlayerInteractor : GenericSingletonClass<PlayerInteractor>
{
    private bool isInObject;
    private GameObject InteractedGameObject;

    //raycast
    private int rayLength;
    private LayerMask layerMaskInteraction;


    // Start is called before the first frame update
    void Start()
    {
        isInObject = false;
        rayLength = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        DrawRayCast();
    }

    void DrawRayCast()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hitData;

        Debug.DrawRay(transform.position, fwd*rayLength, Color.red);
        
        if(Physics.Raycast(ray, out hitData, rayLength, ~(1 << LayerMask.GetMask("Player"))) && (!isInObject || InteractedGameObject == null))
        {
            Debug.Log("masuk ke interactable");
            isInObject = true;
            InteractedGameObject = hitData.collider.gameObject;
        }
        else if(!Physics.Raycast(ray, out hitData, rayLength, ~(1 << LayerMask.GetMask("Player"))) && (isInObject || InteractedGameObject != null))
        {
            Debug.Log("keluar dari interactable");
            isInObject = false;
            InteractedGameObject = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Interact();
        }
    }

    void Interact()
    {
        if(isInObject && InteractedGameObject != null)
        {
            if(InteractedGameObject.transform.parent.gameObject.CompareTag("Ponds"))
            {
                InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false); //mematikan pergerakkan pemain
                GameObject InteractedObjectParent = InteractedGameObject.transform.parent.gameObject;
                LocalInventory localInventory = InteractedObjectParent.GetComponentInChildren<LocalInventory>();
                localInventory.OnInteracted();
                //ShopSystem.Instance.OpenShopMenu(localInventory); 
            }
        }
    }
    

    // private void OnTriggerEnter(Collider other) 
    // {
    //     //if(other.CompareTag("InteractableObjects"))
    //     //{
    //         isInObject = true;
    //         InteractedGameObject = other.gameObject;
    //     //}
    // }

    // private void OnTriggerExit(Collider other) 
    // {
        
    //         isInObject = false;
    //         InteractedGameObject = null;
        
    // }
}
