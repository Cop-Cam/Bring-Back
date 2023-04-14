using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [SerializeField]public int minutes = 0;
    [SerializeField]public int hours = 8;
    [SerializeField]public int timer = 15;
    [SerializeField]public int date = 1;

    //public delegate void TimeManagerEvent();
    //event yang didelegate
    //public static event TimeManagerEvent OnDayChanged;

    public static event Action OnDayChanged;


    //for whatever reason this currentlu worked for preventing instantiating after game stopped
    public override void Awake() 
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("its started");
        InvokeRepeating("Timer", 1.0f, timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (hours == 24)
        {
            DayEnd();
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

    void DayEnd()
    {
        hours = 6;
        minutes = 0;
        date++;

        //Event dijalankan
        OnDayChanged();
    }


    public float CalculateTimeOfDay()
    {
        return hours;
    }

    
}
