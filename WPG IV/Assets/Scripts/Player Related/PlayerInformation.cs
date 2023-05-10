
using UnityEngine;

public class PlayerInformation : GenericSingletonClass<PlayerInformation>
{
    [SerializeField] private PlayerPreset playerPreset;
    [SerializeField] private GameObject currentPlayerModel;
    [SerializeField] private GameObject ModelParentObj;

    
    // Start is called before the first frame update
    private void Start()
    {
        ChangeModel(playerPreset.playerMeshInformation);
    }

    private void ChangeModel(GameObject otherModel)
    {
        if(currentPlayerModel != null) Destroy(currentPlayerModel);

        //currentPlayerModel = Instantiate(otherModel, transform.position, transform.rotation, ModelParentObj.transform);
        currentPlayerModel = Instantiate(otherModel, ModelParentObj.transform);
        
        //currentPlayerModel.transform.parent = transform;
    }


}
