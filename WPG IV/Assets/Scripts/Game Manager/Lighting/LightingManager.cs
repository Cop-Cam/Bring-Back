using UnityEngine;

//[ExecuteAlways]
public class LightingManager : GenericSingletonClass<LightingManager>
{
    [SerializeField]Light sun;
    [SerializeField]float speed = 10f;
    private float currentRotation = 0.0f;

    [SerializeField] TimeManager time;
    
    void Update() {
        //transform.Rotate(Time.deltaTime*time.hours*speed,0,0);

        float rotationAmount = speed * Time.deltaTime / 60.0f;
        currentRotation += rotationAmount;
        transform.rotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);
    }
}