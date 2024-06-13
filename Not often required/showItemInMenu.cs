using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class showItemInMenu : MonoBehaviour
{

    [SerializeField] string itemName;
    [SerializeField] TextMeshProUGUI Name, description;
    [SerializeField] UnityEngine.UI.Button button;
    private void Start()
    {
        foreach (Item item in Inventory.Instance.items)
        {
            if (item.name == itemName)
            {
                GetComponent<UnityEngine.UI.Image>().enabled = true;
                GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;

                
            }
            else
            {
                button.enabled = false;
            }

        }
    }


    public void displayInfo(string itemName)
    {
        foreach (Item item in Inventory.Instance.items)
        {
            if (item.name == itemName)
            {
                GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                Name.text = item.name;
                description.text = item.description;

            }

        }
    }
}
