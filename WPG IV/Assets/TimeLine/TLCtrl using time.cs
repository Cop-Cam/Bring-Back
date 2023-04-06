using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TLCtrl : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineToPlay;
    [SerializeField] TimeManager time;
    [SerializeField] int hour;
    [SerializeField] int min;
    [SerializeField] int date;

    private bool hasPlayed = false;

    void Start() {
        Debug.Log("started ctrl");
    }

    void Update(){
        if (time.date == date && time.hours == hour){
            PlayTimeline(); 
        } 

        
    }
    
    public void PlayTimeline()
    {
        if (!hasPlayed)
        {
            timelineToPlay.Play();
            hasPlayed = true;
        }
        
        // Debug.Log("played");
        // StopTimeLine();
        
    }

    public void StopTimeLine()
    {
        timelineToPlay.Stop();
    }
}
