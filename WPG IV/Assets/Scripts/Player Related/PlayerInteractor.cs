using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this shit in front of player for spawning fishes
public class PlayerInteractor : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMethod()
    {
        Instantiate(ObjectToSpawn, transform.position, transform.rotation);
    }
}
