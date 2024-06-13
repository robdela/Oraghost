using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using System.IO;
using UnityEngine.WSA;

[System.Serializable]
public class InventoryData
{
   static public InventoryData Instance = new InventoryData();
   public List<string> itemNames = new List<string>();
    // Implement the GetEnumerator method required by IEnumerable
    public IEnumerator<string> GetEnumerator()
    {
        return itemNames.GetEnumerator();
    }

    
}

public class Inventory : MonoBehaviour
{
    
    public List<Item> items = new List<Item>();
    static public Inventory Instance = new Inventory();


    

    private void Start()
    {
       
        loadFromJson();
    }
    public void AddItem(Item itemToAdd, int quantity)
    {
        items.Add(itemToAdd);
    }


    public void RemoveItem(Item itemToRemove, int quantity)
    {
        items.Remove(itemToRemove);
    }

    public void GetAllItems()
    {
        foreach (Item item in items)
        {

            Debug.Log(item.name);

        }

    }


    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (Item item in items)
        {
            
            inventoryData.itemNames.Add(item.Name);
        }

        string jsonData = JsonUtility.ToJson(inventoryData);

        File.WriteAllText("inventory.json", jsonData);
        Debug.Log("jsonData : "+jsonData);

    }

    public void loadFromJson()
    {
        if (File.Exists("inventory.json"))
        {

            InventoryData inventoryData = new InventoryData();
            string jsonData = File.ReadAllText("inventory.json");
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);


            foreach (string item in inventoryData)
            {
               Item scriptableItem = Resources.Load<Item>(item);
               Instance.AddItem(scriptableItem, 1 );
            }
           
        }
    }
}



