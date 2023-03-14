using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField] protected float IconHeightFromObject;
    protected GameObject InteractableIndicator_GO;
    protected GameObject InteractIcon_GO;
    protected Color InteractIcon_ActivatedColor;
    protected Color InteractIcon_DeactivatedColor;

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
        if(IconHeightFromObject == 0)
        {
            IconHeightFromObject = 2;
        }
        InteractableIndicator_GO.transform.position = new Vector3(transform.position.x, transform.position.y+IconHeightFromObject, transform.position.z);
        
        SetInteractIcon(GameDatabase.Instance.InteractIconDefault_Sprite, GameDatabase.Instance.InteractIconActivatedDefault_Color, GameDatabase.Instance.InteractIconDeactivatedDefault_Color);
    }

    // private void SetIconBasedOnTag()
    // {
    //     GameObject thisObjParent = transform.parent.gameObject;
    //     switch(thisObjParent.tag)
    //     {
    //         case "inventory":
    //             SetInteractIcon();
    //             break;
    //     }
    // }

    public virtual void PlayerIsInRangeIndicator(bool isInRange)
    {
        
        if(isInRange)
        {
            InteractIcon_GO.GetComponent<Image>().color = InteractIcon_ActivatedColor;
        }
        else if(!isInRange)
        {
            InteractIcon_GO.GetComponent<Image>().color = InteractIcon_DeactivatedColor;
        }
    }

    // protected virtual void LateUpdate() 
    // {
    //     InteractableIndicator.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    // }

    protected virtual void SetInteractIcon(Sprite sprite, Color InRangeColor, Color OutRangeColor)
    {
        InteractIcon_GO.GetComponent<Image>().sprite = sprite;
        InteractIcon_ActivatedColor = InRangeColor;
        InteractIcon_DeactivatedColor = OutRangeColor;
    }
}
