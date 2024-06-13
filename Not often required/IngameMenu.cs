using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject selectedItem;


    public void openMenu()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Playing || (GameStateManager.Instance.CurrentGameState == GameState.Menu && menuCanvas.activeSelf))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            if (menuCanvas.activeSelf)
            {
                GameStateManager.Instance.Menu();
                
                StartCoroutine(Menu());
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                GameStateManager.Instance.Playing();
            }
        }


    }

    
    IEnumerator Menu() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedItem);
        yield break; 
    }


}
