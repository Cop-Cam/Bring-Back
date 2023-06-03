
using UnityEngine;
using UnityEngine.InputSystem;


//Place this shit in front of player for spawning fishes
public class PlayerInteractor : GenericSingletonClass<PlayerInteractor>
{
    private bool isInObject;
    private GameObject InteractedColliderGameObject;
    private GameObject InteractedColliderParentGameObject;

    //raycast
    private int rayLength;
    private LayerMask layerMaskInteraction;

    // private bool test;

    // Start is called before the first frame update
    private void Start()
    {
        isInObject = false;
        rayLength = 2;
    }

    private void FixedUpdate()
    {
        // if(test)
        DrawRayCast();

        //the bug is most probably from collider that probably disappear when game is paused (timescale 0)
    }
    

    private void DrawRayCast()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hitData;

        Debug.DrawRay(transform.position, fwd*rayLength, Color.red);
        
        if(Physics.Raycast(ray, out hitData, rayLength, LayerMask.GetMask("Interactable")) ) //&& (!isInObject) || InteractedGameObject == null))
        {
            if(InteractedColliderParentGameObject != null && isInObject)
            {
                //Debug.Log("sudah ada di object");
                return;
            }

            //Debug.Log("masuk ke interactable");
            isInObject = true;
            InteractedColliderGameObject = hitData.collider.gameObject;
            InteractedColliderParentGameObject = InteractedColliderGameObject.transform.parent.gameObject; //get parent of gameobject that has collider
            
            //InteractedGameObject.transform.parent.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(true);
            InteractedColliderParentGameObject.transform.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(true);
        }
        else if(!Physics.Raycast(ray, out hitData, rayLength, LayerMask.GetMask("Interactable")) )//&& (isInObject || InteractedGameObject != null))
        {
            if(!isInObject) //jika belum masuk ke objek
            {
                //Debug.Log("belum masuk ke interactableobject");
                return;
            }

            //Debug.Log("keluar dari interactableobject");
            isInObject = false;
            //Debug.Log("GAmeobject= "+ InteractedGameObject.name);
            //InteractedGameObject.transform.parent.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(false);
            InteractedColliderParentGameObject.transform.Find("Script").GetComponent<InteractableObjects>().PlayerRaycastIsInRangeIndicator(false);
            InteractedColliderGameObject = null;
            InteractedColliderParentGameObject = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //Debug.Log("interact context performed");
            Interact();
        }
    }

    private void Interact()
    {
        //Debug.Log("interacted gameobject : "+InteractedColliderParentGameObject.name);
        if(isInObject && InteractedColliderParentGameObject != null)
        {
            IInteractable interactableObjects;

            //interactableObjects = InteractedGameObject.transform.parent.gameObject.GetComponentInChildren<IInteractable>();
            interactableObjects = InteractedColliderParentGameObject.transform.GetComponentInChildren<IInteractable>();

            //InteractableObjects interactableObjects = InteractedGameObject.transform.parent.gameObject.GetComponentInChildren<InteractableObjects>();

            interactableObjects.OnInteracted();
        }
    }
}
