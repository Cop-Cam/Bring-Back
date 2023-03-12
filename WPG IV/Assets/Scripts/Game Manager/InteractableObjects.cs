using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField] protected GameObject InteractableIndicator_GO;
    [SerializeField] protected GameObject InteractIcon_GO;
    [SerializeField] protected Image InteractIcon_Image;

    protected virtual void Start()
    {
        while(InteractableIndicator_GO == null)
        {
            InteractableIndicator_GO = transform.parent.Find("InteractIndicator").gameObject;
        }

        while(InteractIcon_GO == null)
        {
            InteractIcon_GO = InteractableIndicator_GO.transform.Find("Canvas").Find("InteractIcon").gameObject;
        }
        //InteractableIndicator.SetActive(false);
    }

    public virtual void PlayerIsInRangeIndicator(bool isInRange)
    {
        //InteractableIndicator.SetActive(isInRange);
        Debug.Log("kondisi icon: "+isInRange);
    }

    // protected virtual void LateUpdate() 
    // {
    //     InteractableIndicator.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    // }

    protected virtual void SetInteractIcon()
    {
        InteractIcon_GO.GetComponent<Image>().sprite = InteractIcon_Image.sprite;
    }
}
