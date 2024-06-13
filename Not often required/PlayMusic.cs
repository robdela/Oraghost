using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    [SerializeField] private string musicName;


    void Start()
    {
        AudioManager.instance.PlayMusic(musicName);
    }

    
}
