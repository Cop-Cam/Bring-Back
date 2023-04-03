using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    [System.Serializable]
    private struct Sound
    {
        [Tooltip("Please dont use space for the name")]
        public string ClipName;
        public AudioClip Clip;
    }

    [SerializeField] private List<Sound> MusicList;
    [SerializeField] private List<Sound> SfxList;

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SfxSource;

    private void Start()
    {
        if(MusicSource == null) MusicSource = transform.Find("MusicSource").GetComponent<AudioSource>();
        if(SfxSource == null) SfxSource = transform.Find("SfxSource").GetComponent<AudioSource>();
    }


    public void PlayMusic(string name)
    {
        Sound sound = MusicList.Find(music => music.ClipName == name);
        
        if(sound.Clip != null)
        {
            MusicSource.clip = sound.Clip;
            MusicSource.Play();
        }
        else
        {
            Debug.LogWarning("no music with that name");
        }
    }

    public void PlaySfx(string name)
    {
        Sound sound = SfxList.Find(sfx => sfx.ClipName == name);
        
        if(sound.Clip != null)
        {
            SfxSource.clip = sound.Clip;
            SfxSource.Play();
        }
        else
        {
            Debug.LogWarning("no sfx with that name");
        }
    }

    public void ToggleMusic() //on off music
    {
        MusicSource.mute = !MusicSource.mute;
    }

    public void ToggleSfx() // on off sfx
    {
        SfxSource.mute = !SfxSource.mute;
    }

    public void MusicVolume(float volume) //mengatur volume music
    {
        MusicSource.volume = volume;
    }

    public void SfxVolume(float volume) //mengatur volumen sfx
    {
        SfxSource.volume = volume;
    }

}
