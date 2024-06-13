using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class dayNightCycle : MonoBehaviour
{
    [SerializeField] GameObject[] lights;
    private int tick = 50;
    private int hours;
    private float cycle;
    public Volume volume;
    private bool up;
    private float seconds = 0;

    private void Awake()
    {
        cycle = PlayerPrefs.GetFloat("dayCycle", 0);
        if (PlayerPrefs.GetInt("up") == 1)
        {
            up = true;
        }
        else if (PlayerPrefs.GetInt("up") == 0)
        {
            up = false;
        }



        volume.weight = cycle;
        
    }
    private void FixedUpdate()
    {
        seconds += Time.fixedDeltaTime * tick;
        
        if (seconds >= 3600)
        {
            seconds = 0;
            hours += 1;

        }

        if (hours >= 1)
        {
            hours -= 1;
            if (volume.weight <= 0)
            {
                up = true;
                PlayerPrefs.SetInt("up", 1);
            }
            if (volume.weight >= 1)
            {
                up = false;
                PlayerPrefs.SetInt("up", 0);
            }


            if (up == false)
            {

                volume.weight -= 0.05f;
                PlayerPrefs.SetFloat("dayCycle", volume.weight);
            }

            

            if (up == true)
            {
                volume.weight += 0.05f;
                PlayerPrefs.SetFloat("dayCycle", volume.weight);
            }

            if (volume.weight < 0.6)
            {
                LightOff();
            }

            if (volume.weight > 0.6)
            {
                LightOn();
            }
        }
    }

    private void LightOn()
    {
        foreach (GameObject light in lights) 
        {
            light.SetActive(true);
        }
    }

    private void LightOff()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }


}
