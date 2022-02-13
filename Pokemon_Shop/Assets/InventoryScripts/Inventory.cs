using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<InventoryItem, int> inventory;
    static Inventory instance;
    [SerializeField] InventoryItem testItem;
    [SerializeField] InventoryItem testItem2;
    [SerializeField] InventoryItem testItem3;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        inventory = new Dictionary<InventoryItem, int>();
        AddAll();
    }

    void AddAll()
    {
        foreach(InventoryItem item in Resources.FindObjectsOfTypeAll<InventoryItem>()) //causes unscrollable bug
        {
            // Debug.Log("Adding " + item);
            //
            
                AddItem(item);
            

        }
    }

    public void AddItem(InventoryItem item)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += 1;
        } else
        {
            inventory[item] = 1;
        }
        // Debug.Log("Added " + item);
    }

    public bool RemoveItem(InventoryItem item)
    {
        if (!inventory.ContainsKey(item))
        {
            return false;
        }
        inventory[item] -= 1;
        if (inventory[item] == 0)
        {
            inventory.Remove(item);
        }
        return true;
    }
    
    // Returns a dictionary with the IDName and number of items in said Inventory object
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
