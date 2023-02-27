using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_Ikan : MonoBehaviour
{
    public class Ikan_Lokal
    {
        public string name {get; set;}
        public string description {get; set;}
    }

    public class Ikan_Invasif
    {
        public string name {get; set;}
        public string description {get; set;}
    }




    // Start is called before the first frame update
    void Start()
    {
        Ikan_Invasif Gabus = new Ikan_Invasif()
        {
            name = "Ikan Gabus",
            description = "Ikan berbentuk gabus"
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


