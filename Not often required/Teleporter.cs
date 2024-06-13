using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{ 

    [SerializeField] GameObject canvas, text, skip, yesButton, noButton, sceneChanger, loadingScreen;
    [SerializeField] string nameOfTheSceneToLoad;

    [SerializeField] string[] texts ;
    int incrementer = 0;
    

    private bool isOnScreen = false;

    public void Interact()
    {
        
        StartCoroutine(teleporter());
    }

    private IEnumerator teleporter()

    {
        yesButton.GetComponent<Button>().enabled = false;
        noButton.GetComponent<Button>().enabled = false;
        skip.GetComponent<Button>().enabled = true;
        if (!isOnScreen)
        {
            yield return new WaitForSeconds(0.2f);
            GameStateManager.Instance.Dialog();
            GameObject gameManager = GameObject.Find("Game Manager");
            gameManager.SendMessage("dialogCanvas", true, SendMessageOptions.DontRequireReceiver);
            isOnScreen = true;

            GameObject text = GameObject.Find("textObject");
            text.SendMessage("OnEnable", texts[incrementer], SendMessageOptions.DontRequireReceiver);
            incrementer++;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(skip);


            yield return new WaitForSeconds(0.1f);
        }
        if (isOnScreen)
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

        }
    }
public void ExitButton()
{
    if (isOnScreen)
    {

        canvas.SetActive(false);
        isOnScreen = false;
            incrementer = 0;
            
            GameStateManager.Instance.Playing();
    }
}


    public void Teleport()
    {
        StartCoroutine(teleport());
        canvas.SetActive(false);
    }

    private IEnumerator teleport()
    {
        sceneChanger.GetComponent<Scenechanger>().changeScene(nameOfTheSceneToLoad, loadingScreen, true);

        yield break;
    }
}
