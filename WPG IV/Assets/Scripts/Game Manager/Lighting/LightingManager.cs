using UnityEngine;

//[ExecuteAlways]
public class LightingManager : GenericSingletonClass<LightingManager>
{
    [SerializeField]Light sun;
    [SerializeField] TimeManager time;
    [SerializeField] float hour;
    [SerializeField] float min;
    [SerializeField] float rotationSpeed = 0.12f;
    
    void Update() {
        //transform.Rotate(Time.deltaTime*time.hours*speed,0,0);
        hour = time.hours;
        min = time.minutes;
        float timeinsecond = time.hours * 3600 + time.minutes * 60;

        float angle = (timeinsecond / 86400.0f * 360.0f) - 90.0f;

        Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}