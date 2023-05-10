using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


// how to use: 
// tambahin aja ke objek yang jadi trigger

public class TLCtrlonTrigger : MonoBehaviour  
{
    [SerializeField] PlayableDirector timeline;
    public Collider triggerCollider;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (!hasPlayed)
        {
            timeline.Play();
            hasPlayed = true;
            Debug.Log("played");
        }
    }
}