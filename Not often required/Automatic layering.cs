using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaticlayering : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform Character;


    private void OnBecameVisible()
    {
        StartCoroutine (layering());
    }

    private void OnBecameInvisible()
    {
        StopCoroutine (layering ());
    }


    private IEnumerator layering()
    {
        if (Character.position.y > transform.position.y)
        {
            spriteRenderer.sortingLayerName = "Foreground";
        } else
        {
            spriteRenderer.sortingLayerName = "Default";
        }
        
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(layering());
    }
}
