using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingAndLoading : MonoBehaviour
{
    static public SavingAndLoading Instance = new SavingAndLoading();
    [SerializeField] GameObject savingCanvas;

    public void Save()
    {
        StartCoroutine(Saving());  

    }

    private IEnumerator Saving()
    {
        savingCanvas.SetActive(true);
        savingCanvas.GetComponent<Animator>().Play("fade in");
       

        yield return new WaitForSecondsRealtime(1f);
        Inventory.Instance.SaveInventory();
        GameObject player = GameObject.Find("Player");

        PlayerPrefs.SetFloat(" x_value", player.transform.position.x);
        PlayerPrefs.SetFloat(" y_value", player.transform.position.y);
        yield return new WaitForSecondsRealtime(2f);

        savingCanvas.GetComponent<Animator>().Play("fade out");
        yield return new WaitForSecondsRealtime(1f);
        savingCanvas.SetActive(false);
    }

    public void Load()
    {
        GameObject player = GameObject.Find("Player");
        float x_value = PlayerPrefs.GetFloat(" x_value", player.transform.position.x);
        float y_value = PlayerPrefs.GetFloat(" y_value", player.transform.position.y);
        Vector2 playerPosition = new Vector2(x_value, y_value);
        player.transform.position = playerPosition;
    }
}
