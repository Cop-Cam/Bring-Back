
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObjects : MonoBehaviour
{
    [System.Serializable]
    protected struct InteractableObjectSetting
    {
        public float IconHeightFromObject;
        public GameObject InteractableIndicatorObj;
        public GameObject InteractIconObj;
        public Color ActivatedInteractIconColor;
        public Color DeactivatedInteractIconColor;
    }
    protected InteractableObjectSetting interactableObjectSetting;

    protected virtual void Start()
    {
        if(interactableObjectSetting.InteractableIndicatorObj == null)
        {
            interactableObjectSetting.InteractableIndicatorObj = transform.parent.Find("InteractIndicator").gameObject;
        }
        if(interactableObjectSetting.InteractIconObj == null)
        {
            interactableObjectSetting.InteractIconObj = interactableObjectSetting.InteractableIndicatorObj.transform.Find("Canvas").Find("InteractIcon").gameObject;
        }
        if(interactableObjectSetting.IconHeightFromObject == 0)
        {
            interactableObjectSetting.IconHeightFromObject = 2;
        }
        interactableObjectSetting.InteractableIndicatorObj.transform.position = new Vector3(transform.position.x, transform.position.y+interactableObjectSetting.IconHeightFromObject, transform.position.z);
        
        SetInteractIcon(GameDatabase.Instance.InteractIconDefault_Sprite, GameDatabase.Instance.InteractIconActivatedDefault_Color, GameDatabase.Instance.InteractIconDeactivatedDefault_Color);
    }

    public virtual void PlayerRaycastIsInRangeIndicator(bool isInRange)
    {
        if(isInRange)
        {
            interactableObjectSetting.InteractIconObj.GetComponent<Image>().color = interactableObjectSetting.ActivatedInteractIconColor;
        }
        else if(!isInRange)
        {
            interactableObjectSetting.InteractIconObj.GetComponent<Image>().color = interactableObjectSetting.DeactivatedInteractIconColor;
        }
    }

    protected virtual void SetInteractIcon(Sprite sprite, Color InRangeColor, Color OutRangeColor)
    {
        interactableObjectSetting.InteractIconObj.GetComponent<Image>().sprite = sprite;
        interactableObjectSetting.ActivatedInteractIconColor = InRangeColor;
        interactableObjectSetting.DeactivatedInteractIconColor = OutRangeColor;
    }

    public abstract void OnInteracted();
}
