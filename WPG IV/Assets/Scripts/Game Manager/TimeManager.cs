using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    public float timescale { get; private set; }
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    //instantiate script
    void Awake() 
    {
        if(instance != null)
        {
            Debug.Log("there is another TimeManager");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeOfDay = 5f;
        timescale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    void StartTimer()
    {
        //if (Application.isPlaying)
        //{
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime * timescale;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
        //}
    }
}
