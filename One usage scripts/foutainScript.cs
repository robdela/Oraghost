using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class foutainScript : MonoBehaviour
{
    [SerializeField] GameObject canvas, skip;
    private bool isOnScreen = false;


    [SerializeField] string[] texts;
    int incrementer = 0;


    public void Interact()
    {
        StartCoroutine(interact());
    }

    private IEnumerator interact()
    {
        if (!isOnScreen)
        {

            yield return new WaitForSeconds(0.2f);
            GameStateManager.Instance.Dialog();
            GameObject gameManager = GameObject.Find("Game Manager");
            gameManager.SendMessage("dialogCanvas", true, SendMessageOptions.DontRequireReceiver);
            isOnScreen = true;
            skip.GetComponent<Button>().enabled = true;

            GameObject text = GameObject.Find("textObject");
            text.SendMessage("OnEnable", texts[incrementer], SendMessageOptions.DontRequireReceiver);
            incrementer++;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(skip);


            yield return new WaitForSeconds(0.1f);

        }
        if (isOnScreen && TextAppearGradually.isTextCompleted)
        {
            GameObject text = GameObject.Find("textObject");
            text.GetComponent<TextMeshProUGUI>().text = "";

            if (texts.Length != incrementer)
            {
                text.SendMessage("OnEnable", texts[incrementer]);
                incrementer++;
            }


            else if (texts.Length <= incrementer)
            {

                skip.GetComponent<Button>().enabled = false;
                EventSystem.current.SetSelectedGameObject(null);
                GameStateManager.Instance.Playing();
                GameObject gameManager = GameObject.Find("Game Manager");
                gameManager.SendMessage("dialogCanvas", false, SendMessageOptions.DontRequireReceiver);
                isOnScreen = false;
                incrementer = 0;

            }


            yield break;
        }
    }

    private void Update()
    {
        if (isOnScreen)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

}


    
