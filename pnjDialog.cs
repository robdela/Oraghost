using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pnjDialog : MonoBehaviour
{
    [Multiline]
    [SerializeField] public string sentence;
    private bool isOnScreen = false;

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

            GameObject text = GameObject.Find("textObject");
            text.SendMessage("OnEnable", sentence, SendMessageOptions.DontRequireReceiver);
            isOnScreen = true;
            yield return new WaitForSeconds(1f);
        }
    }
    public void Exit()
    {
        if (isOnScreen && TextAppearGradually.isTextCompleted)
        {
            GameObject text = GameObject.Find("textObject");
            text.GetComponent<TextMeshProUGUI>().text = "";
            GameStateManager.Instance.Playing();
            GameObject gameManager = GameObject.Find("Game Manager");
            gameManager.SendMessage("dialogCanvas", false, SendMessageOptions.DontRequireReceiver);
            isOnScreen = false;
        }
    }

}
