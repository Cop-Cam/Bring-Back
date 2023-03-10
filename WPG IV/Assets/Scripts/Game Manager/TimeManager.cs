using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : GenericSingletonClass<TimeManager>
{
   /*
    //public static TimeManager instance { get; private set; }

    public float timescale { get; private set; }
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    //instantiate script
    // void Awake() 
    // {
    //     if(instance != null)
    //     {
    //         Debug.Log("there is another TimeManager");
    //     }
    //     instance = this;
    // }

    // Start is called before the first frame update
    void Start()
    {
        TimeOfDay = 5f;
        timescale = 0.1f;
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
            TimeOfDay += CalculateTimeOfDay();
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
        //}
    }
*/

    [SerializeField]public float minutes = 0;
    [SerializeField]public float hours = 8;
    [SerializeField]public float timer = 15;
    [SerializeField]public int date = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("its started");
        InvokeRepeating("Timer", 1.0f, timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (hours == 24)
        {
            DayEnd();
        }
        else //cek terus apakah dapat menimbulkan bug pada pakan terutama fungsi FishMaturingMethod() di script PondInventory
        {
            daychanged = false;
        }
    }

    void Timer()
    {
        minutes+=10;
        if (minutes >= 60)
        {
            hours++;
            minutes = 0;
        }
        //nge ganggu, dikomen dulu
        // Debug.Log(hours);
        // Debug.Log(minutes);   
    }

    public bool daychanged;
    void DayEnd()
    {
        hours = 6;
        minutes = 0;
        daychanged = true;
        date++;
    }


    public float CalculateTimeOfDay()
    {
        return hours;
    }
}
