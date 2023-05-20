using UnityEngine;

[ExecuteAlways]
public class LightingManager : GenericSingletonClass<LightingManager>
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Start()
    {
        //Debug.Log("timetotal: "+TimeManager.totalTime);
    }

    /*
    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay = TimeManager.totalTime;
            //(Replace with a reference to the game time)
            // TimeOfDay += Time.deltaTime;
            // TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
            //UpdateLighting(TimeManager.totalTime);
        }
        else
        {
            TimeOfDay = TimeManager.totalTime;

            UpdateLighting(TimeOfDay / 24f);
            //UpdateLighting(TimeManager.totalTime);
        }
    }
    */

    public void UpdateLightingPublic(float TimeOfDay)
    {
        if (Preset == null) return;

        UpdateLighting(TimeOfDay / 24f);
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    /*
    [SerializeField]GameObject LightSourceObj; //object which get
    [SerializeField]Light sun; //lighting preset i guess?
    [SerializeField] TimeManager time;
    [SerializeField] float hour;
    [SerializeField] float min;
    [SerializeField] float rotationSpeed = 0.12f;
    
    private void Start()
    {
        time = TimeManager.Instance;
    }

    void Update() {
        //transform.Rotate(Time.deltaTime*time.hours*speed,0,0);
        hour = time.hours;
        min = time.minutes;
        float timeinsecond = time.hours * 3600 + time.minutes * 60;

        float angle = (timeinsecond / 86400.0f * 360.0f) - 90.0f;

        Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);
        LightSourceObj.transform.rotation = Quaternion.Slerp(LightSourceObj.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
    */
}