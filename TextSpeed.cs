using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextAppearGradually : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public string textToDisplay;
    public float textSpeed;
    public static bool isTextCompleted;

    public void OnEnable(string textToDisplay)
    {
        StopAllCoroutines();
        textElement.text = "";
        isTextCompleted = false;

        StartCoroutine(ShowTextGradually(textToDisplay));
    }

    IEnumerator ShowTextGradually(string textToDisplay)
    {
        int charIndex = 0;

        while (charIndex < textToDisplay.Length)
        {
            textElement.text += textToDisplay[charIndex];
            charIndex++;
            yield return new WaitForSecondsRealtime(1/textSpeed);
        }
        isTextCompleted = true;
    }
}