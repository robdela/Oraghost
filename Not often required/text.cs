using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    [SerializeField]public GameObject DialogCanvas;

    public void dialogCanvas(bool isactive)
    {
        if (isactive)
        {
            DialogCanvas.SetActive(true);
        }
        else
        {
            DialogCanvas.SetActive(false);
        }
    }
}
