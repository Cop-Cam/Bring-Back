using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
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

/*
    [SerializeField]public float minutes = 0;
    [SerializeField]public float hours = 8;
    [SerializeField]public float timer = 15;
    [SerializeField]public float date = 1;

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
    */


    [SerializeField]private float ingamesecond = 15.0f;
    [SerializeField]private float realtimesecond = 600.0f;

    [SerializeField]private float startingSecond = 0;
    [SerializeField]private float startingMinute = 0;
    [SerializeField]private float startingHour = 8;
    [SerializeField]private float startingDate = 1;

    [field: SerializeField]public float currentSecond {get; private set;}
    [field: SerializeField]public float currentMinute {get; private set;}
    [field: SerializeField]public float currentHour {get; private set;}
    [field: SerializeField]public float currentDate {get; private set;}

    public static float totalTime {get; private set;}

    private float timescale;

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
        currentSecond = startingSecond;
        currentMinute = startingMinute;
        currentHour = startingHour;
        currentDate = startingDate;
        
        timescale = (ingamesecond / realtimesecond) * Time.timeScale;
        
        Debug.Log("timescale is: "+timescale);

        InvokeRepeating("Timer", 1.0f, timescale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Timer()
    {
        
        // Increment the time by one second every frame
        currentSecond++;
        if (currentSecond >= 60)
        {
            currentSecond = 0;
            currentMinute++;
            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
                if (currentHour >= 24)
                {
                    currentHour = 0;
                    DayEnd();
                }
            }
        }
        
        //convert all to hour based
        totalTime = currentHour+(currentMinute/60f)+(currentSecond/3600f);
            
    }

    void DayEnd()
    {
        currentHour = 6;
        currentMinute = 0;
        currentSecond = 0;
        currentDate++;

        //Event dijalankan
        OnDayChanged();
    }
    
}
