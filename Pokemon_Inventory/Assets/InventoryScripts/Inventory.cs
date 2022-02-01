using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<InventoryItem, int> inventory;
    static Inventory instance;

    void Awake()
    {
        // Makes Inventory a singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        // TODO: create dictionary for inventory
        AddAll();   
    }

    // For debugging - This should add one of each InventoryItem in the assets folder
    void AddAll()
    {
        foreach(InventoryItem item in Resources.FindObjectsOfTypeAll<InventoryItem>()) 
        {
            AddItem(item);
        }
    }

    // Adds an item to the dictionary, if an item already exists in the dictionary, increment its value by 1
    public void AddItem(InventoryItem item)
    {
        // TODO: Implement this
    }

    // Removes an item from the dictionary & return true by decrementing the value by 1. If the value is 0, delete the object from the dictionary. If item doesnt exist in the dict, return false 
    public bool RemoveItem(InventoryItem item)
    {
        // TODO: Implement this
        return true;
    }
    
    // Returns a dictionary with the IDName and number of items in said Inventory object - not necessary to use
    public Dictionary<string, int> GetItemList()
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        foreach (InventoryItem item in inventory.Keys)
        {
            dict[item.GetIDName()] = inventory[item];
        }
        return dict; 
    }

    // Returns the exact dictionary this object has 
    public Dictionary<InventoryItem, int> GetItemDict()
    {
        return inventory;
    }
}
