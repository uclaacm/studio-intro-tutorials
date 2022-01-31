using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Image img3;

    [SerializeField] Inventory player;

    Color[] colors = {Color.black, Color.blue, Color.red, Color.green, Color.yellow};  // For testing - to be replaced with actual inventory 

    InventoryItem[] invIndex;  // Master list of all InventoryItems
    Dictionary<string, int> inventory;  // Dictionary passed in from the Inventory class
    List<InventoryItem> displayList;  // List of items to be referenced by the display
    int lstSize = 0;

    int dispIndex = 0; 
    int cursorPos = 0;  // Can go from 0-2, 0 being topmost, 2 being bottommost

    private void Start()
    {
        invIndex = Resources.FindObjectsOfTypeAll<InventoryItem>();
        foreach(InventoryItem item in invIndex)
        {
            Debug.Log(item);
        }
        displayList = new List<InventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursorDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursorDown(true);
        }
        GetInventory();
        // print();
        RenderDisplay();
    }

    // Update local inventory dictionary 
    void GetInventory()
    {
        inventory = player.GetItemList();
        // Remove any items no longer in inventory from display
        foreach (InventoryItem item in displayList)
        {
            if (!inventory.ContainsKey(item.GetIDName()))
            {
                displayList.Remove(item);
                lstSize--;
            }
        }
        foreach (string IDName in inventory.Keys)  // Search through the inventory dictionary
        {
            bool found = false;
            foreach(InventoryItem item in invIndex)  // Find matching InventoryItem with the IDName
            {
                if (IDName == item.GetIDName())  // If match is found & the item is not already in displayList
                {
                    if (!displayList.Contains(item))
                    {
                        displayList.Add(item);
                        lstSize++;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Debug.LogError("No match found for item: " + IDName);
            }
        }
    }

    // Move cursor 
    void MoveCursorDown(bool reverse = false)
    {
        if (reverse)
        {
            if (cursorPos == 0)  // If cursor pointing at topmost slot
            {
                ShiftDisplayDown(true);  // Attempt to shift display up
            }
            else
            {
                cursorPos--;
            }
        } else
        {
            if (cursorPos == 2)
            {
                ShiftDisplayDown();
            } 
            else if (dispIndex + cursorPos + 1 < lstSize)  // Cursor is not pointing out of inventory range
            {
                cursorPos++;
            }
        }
    }

    // Attempts to shift the displayed section of array, returning false if unable to
    bool ShiftDisplayDown(bool reverse = false)
    {
        if (reverse)
        {
            if (dispIndex - 1 < 0)
            {
                return false;
            }
            else
            {
                dispIndex--;
                return true;
            }

        }
        else
        {
            if (dispIndex + 1 >= lstSize - 2)
            {
                return false;
            }
            else
            {
                dispIndex++;
                return true;
            }
        }
    }

    // Display the Inventory slots, rendering each slot with its corresponding image
    void RenderDisplay()
    {
        // img1.color = colors[dispIndex];
        // img2.color = colors[dispIndex + 1];
        // img3.color = colors[dispIndex + 2];
        if (dispIndex < lstSize)
        {
            img1.GetComponentInChildren<Text>().text = displayList[dispIndex].GetDisplayName();
        }
        if (dispIndex + 1 < lstSize)
        {
            img2.GetComponentInChildren<Text>().text = displayList[dispIndex + 1].GetDisplayName();
        }
        if (dispIndex + 2 < lstSize)
        {
            img3.GetComponentInChildren<Text>().text = displayList[dispIndex + 2].GetDisplayName();
        }

        // Change the alpha to highlight the cursor
        switch (cursorPos)
        {
            case 0:
                img1.color = new Color(img1.color.r, img1.color.g, img1.color.b, 1f);
                img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, 0.5f);
                img3.color = new Color(img3.color.r, img3.color.g, img3.color.b, 0.5f);
                break;
            case 1:
                img1.color = new Color(img1.color.r, img1.color.g, img1.color.b, 0.5f);
                img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, 1f);
                img3.color = new Color(img3.color.r, img3.color.g, img3.color.b, 0.5f);
                break;
            case 2:
                img2.color = new Color(img2.color.r, img2.color.g, img2.color.b, 0.5f);
                img1.color = new Color(img1.color.r, img1.color.g, img1.color.b, 0.5f);
                img3.color = new Color(img3.color.r, img3.color.g, img3.color.b, 1f);
                break;
        }

    }

    // Returns the InveentoryItem that cursor is pointing at
    public InventoryItem GetItemOnCursor()
    {
        return displayList[dispIndex + cursorPos];
    }

    // For debugging
    void print()
    {
        foreach (InventoryItem item in displayList)
        {
            Debug.Log(item);
        }
    }

}
