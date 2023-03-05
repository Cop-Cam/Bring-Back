using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] protected InventoryItemData itemData;
    [SerializeField] private Button thisObjButton;

    protected virtual void Awake() 
    {
        thisObjButton = transform.GetComponent<Button>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        thisObjButton.onClick.AddListener(() => OnClick());
    }

    protected virtual void OnClick()
    {
        Debug.Log("Tombol ditekan");
    }

    public virtual void SetButtonItemData(InventoryItemData other)
    {
        itemData = other;
    }

    // public virtual void SetButtonInteractable(bool isInteractable)
    // {
    //     thisObjButton.enabled = isInteractable;
    // }
}
