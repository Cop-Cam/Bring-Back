using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this shit in front of player for spawning fishes
public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
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
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
