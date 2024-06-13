using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Animatepics : MonoBehaviour
{

    [SerializeField] public Sprite spriteUP, spriteDOWN;
    
    public int picID;
    

    private void Awake()
    {
        

        Animator animator = GetComponent<Animator>();
        

        
        //PlayerPrefs.DeleteKey("Pic_" + picID);
        int state = PlayerPrefs.GetInt("Pic_" + picID, 2);

        if (state == 1)
        {

            GetComponent<SpriteRenderer>().sprite = spriteDOWN;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        else if (state == 0)
        {

            GetComponent<SpriteRenderer>().sprite = spriteUP;
            GetComponent<BoxCollider2D>().enabled = true;
            
        }

        else if (state == 2) { animator.enabled = !animator.enabled; }

        
    }
   

    public void PicsUps()
    {
        GetComponent<Animator>().Play("pics up");
        GetComponent<BoxCollider2D>().enabled = true;
        string key = GetInstanceID().ToString();
        PlayerPrefs.SetInt("Pic_" + picID, 0);
    }

    public void PicsDown()
    {
        GetComponent<Animator>().Play("pics down");
        GetComponent<BoxCollider2D >().enabled = false;
        string key = GetInstanceID().ToString();
        PlayerPrefs.SetInt("Pic_" + picID, 1);
    }

    

}
