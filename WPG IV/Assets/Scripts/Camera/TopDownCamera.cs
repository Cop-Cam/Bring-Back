using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform cameraTarget;
    public float cameraHeight;
    public float cameraDistance;
    public float cameraAngle;

    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, cameraSpeed * Time.deltaTime);
    }
}
