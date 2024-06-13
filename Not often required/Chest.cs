using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    [SerializeField] public Sprite spriteUP;


    [SerializeField] private GameObject Player;


    [SerializeField] public List<GameObject> pics;
    public int ChestID;

    public Item item;

    [SerializeField] public TextMeshProUGUI ItemName, ItemDescription;
    
    [SerializeField] public Image Sprite;
    [SerializeField] private GameObject canvas, Empty;
    
    private bool isOnScreen = false;








    private void Awake()
    {

        //PlayerPrefs.DeleteKey("ChestID_" + ChestID);
        int state = PlayerPrefs.GetInt("ChestID_" + ChestID, 0);
        Animator animator = GetComponent<Animator>();
        
        if (state == 1)
        {
            GetComponent<SpriteRenderer>().sprite = spriteUP;
        } else if (state == 0) { animator.enabled = !animator.enabled; }




    }


    public void Interact()
    {
        int state = PlayerPrefs.GetInt("ChestID_" + ChestID, 0);
        if (state == 0)
        {
            Animator animator = GetComponent<Animator>();

            if (animator.runtimeAnimatorController.name == "Chest behind")
            {
                animator.Play("chest back");
            }

            else
            {
                animator.Play("chest front");
            }



            
            PlayerPrefs.SetInt("ChestID_" + ChestID, 0);

            StartCoroutine(Content(0));
        }

        if (state == 1)
        {
            StartCoroutine(Content(1));
        }

        
    }



    private IEnumerator Content(int open)
    {
        

        if (open == 0 && !isOnScreen)
        {
            yield return new WaitForSeconds(0.2f);
            Sprite.sprite = item.sprite;
            ItemDescription.text = item.description;
            ItemName.text = item.Name;



            canvas.SetActive(true);
            isOnScreen = true;
            GameStateManager.Instance.Chest();
            PlayerPrefs.SetInt("ChestID_" + ChestID, 1);

            Inventory.Instance.AddItem(item, 1);

            yield return new WaitForSeconds(1);
            foreach (GameObject gameObject in pics)
            {
                gameObject.GetComponent<Animatepics>().PicsDown();
            }

        }

        if (open == 1 && !isOnScreen)
        {
            yield return new WaitForSeconds(0.2f);
            Empty.SetActive(true);
            isOnScreen = true;
            GameStateManager.Instance.Chest();

            yield return new WaitForSeconds(1);


        }

        


    }



    public void Exit()
    {
        if (isOnScreen)
        {

            canvas.SetActive(false);
            
            Empty.SetActive(false);
            
            
            
            Player.GetComponent<Animator>().Play("Move");
            isOnScreen = false;
            GameStateManager.Instance.Playing();


        }
    }





}
