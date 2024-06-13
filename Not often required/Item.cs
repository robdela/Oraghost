using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/default")]
[System.Serializable]
public class Item : ScriptableObject

{
    public string Name;
    public Sprite sprite;
    public string description;

}

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;
}



