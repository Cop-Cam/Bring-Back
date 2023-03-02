using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start dipanggil");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() 
    {
        Debug.Log("Awake dipanggil");
    }
}
