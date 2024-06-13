using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

     
    }


    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    


    public static AudioManager instance;

    public float musicVolume;
    public float sfxVolume;

    

    public void mVolume(float mVolume)
    {
        musicSource.volume = mVolume;
    }

    public void sVolume(float sVolume)
    {
        sfxSource.volume = sVolume;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }



    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
    public void stopMusic()
    {
        musicSource.Stop();
    }

    public void stopSFX()
    {
        sfxSource.Stop();
    }


}


