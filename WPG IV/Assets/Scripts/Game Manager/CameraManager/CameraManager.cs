using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : GenericSingletonClass<CameraManager>
{
    [SerializeField] private GameObject tPlayer;
    [SerializeField] CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        
        if(tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
        }
        //vcam.LookAt = tPlayer.transform;
        vcam.Follow = tPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
