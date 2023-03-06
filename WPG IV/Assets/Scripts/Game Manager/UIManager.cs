using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingletonClass<UIManager>
{
    [Tooltip("Masukkan GameObject parent untuk UI Shop")]
    public GameObject ShopUI;

    // Start is called before the first frame update
    void Start()
    {
        ShopUI = ShopSystem.Instance.ShopUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
