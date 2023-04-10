using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAudio : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
