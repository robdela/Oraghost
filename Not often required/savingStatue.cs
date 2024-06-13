using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class savingStatue : MonoBehaviour
{

    [SerializeField] GameObject canvas, save;
    
    private bool isOnScreen = false;

    public void Interact()
    {
        StartCoroutine(Statue());
    }

    private IEnumerator Statue()

    {
        if (!isOnScreen)
        {
            yield return new WaitForSeconds(0.1f);
            isOnScreen = true;
            
            canvas.SetActive(true);
            GameStateManager.Instance.Menu();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(save);
            yield return new WaitForSeconds(1);

        }
        
    }

    public void ExitButton()
    {
        if (isOnScreen)
        {
            
            canvas.SetActive(false);
            isOnScreen = false;
            GameStateManager.Instance.Playing();
        }
    }

}
