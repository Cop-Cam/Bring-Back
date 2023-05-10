
using UnityEngine;
using UnityEngine.InputSystem;


//Place this shit in front of player for spawning fishes
public class PlayerInteractor : GenericSingletonClass<PlayerInteractor>
{
    private bool isInObject;
    private GameObject InteractedGameObject;
    //private GameObject InteractableIndicator;

    //raycast
    private int rayLength;
    private LayerMask layerMaskInteraction;


    // Start is called before the first frame update
    void Start()
    {
        isInObject = false;
        rayLength = 2;

        // InteractableIndicator.transform.position = new Vector3(0, 20, 0);
        // InteractableIndicator.SetActive(false);
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
        
        if(Physics.Raycast(ray, out hitData, rayLength, LayerMask.GetMask("Interactable")) && (!isInObject || InteractedGameObject == null))
        {
            //Debug.Log("masuk ke interactable");
            isInObject = true;
            InteractedGameObject = hitData.collider.gameObject;
            InteractedGameObject.transform.parent.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(true);
        }
        else if(!Physics.Raycast(ray, out hitData, rayLength, LayerMask.GetMask("Interactable")) && (isInObject || InteractedGameObject != null))
        {
            //Debug.Log("keluar dari interactable");
            isInObject = false;
            Debug.Log("GAmeobject= "+ InteractedGameObject.name);
            InteractedGameObject.transform.parent.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(false);
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
            //IInteractable interactable = InteractedGameObject.transform.parent.gameObject.GetComponentInChildren<IInteractable>();

            InteractableObjects interactableObjects = InteractedGameObject.transform.parent.gameObject.GetComponentInChildren<InteractableObjects>();

            if(interactableObjects != null)
            {
                Debug.Log("current interacted item :"+InteractedGameObject.transform.parent.gameObject.name);
                InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false); //mematikan pergerakkan pemain
                interactableObjects.OnInteracted();
            }
            
            /*
            if(InteractedGameObject.transform.parent.gameObject.CompareTag("Inventory"))
            {
                InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false); //mematikan pergerakkan pemain
                GameObject InteractedObjectParent = InteractedGameObject.transform.parent.gameObject;
                LocalInventory localInventory = InteractedObjectParent.GetComponentInChildren<LocalInventory>();
                localInventory.OnInteracted();
                //ShopSystem.Instance.OpenShopMenu(localInventory); 
            }
            */
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
