
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObjects : MonoBehaviour, IInteractable
{
    [System.Serializable]
    protected struct InteractableObjectSetting
    {
        public bool dontUseScriptToPositioningIndicator;
        public float y_axis_offset;
        public GameObject InteractableIndicatorObj;
        public GameObject InteractIconObj;
        public Color ActivatedInteractIconColor;
        public Color DeactivatedInteractIconColor;
    }
    [SerializeField] protected InteractableObjectSetting interactableObjectSetting;

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
        if(interactableObjectSetting.y_axis_offset == 0)
        {
            interactableObjectSetting.y_axis_offset = 2;
        }
        
        if(!interactableObjectSetting.dontUseScriptToPositioningIndicator)
        {
            interactableObjectSetting.InteractableIndicatorObj.transform.position = 
                new Vector3(transform.position.x, 
                    transform.position.y+interactableObjectSetting.y_axis_offset, 
                    transform.position.z);
        }
        
        
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
