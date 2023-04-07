using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingletonClass<UIManager>
{
    [Tooltip("Masukkan GameObject parent untuk UI Shop")]
    //mungkin bisa satu2 didefinisikan tapi gak nyaman ama sekali aowkowkw
    //public GameObject ShopUI;
    
 

    //mungkin bisa pake list, tapi gtw caramu makenya
    private List<GameObject> GameUIList;


    //atau mungkin bisa pake dict, tapi gtw caramu makenya
    private Dictionary<string, GameObject> ManagerWithUIObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Awake()
    {
        base.Awake();

        ManagerWithUIObj = new Dictionary<string, GameObject>();
    }

    public void AddUiObjToList(GameObject obj)
    {
        GameUIList.Add(obj);
    }

    public void AddGameObjectToDictionary(GameObject obj)
    {
        ManagerWithUIObj.Add(obj.name, obj);
        Debug.Log("new: "+obj.name);
        Debug.Log("size: "+ManagerWithUIObj.Count);
    }
}
