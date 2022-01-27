using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    //INVENTORY LIST
    [Header("Inventory List")]
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Image img3;

    [SerializeField] Inventory player;

    InventoryItem[] invIndex;  // Master list of all InventoryItems
    Dictionary<string, int> inventory;  // Dictionary passed in from the Inventory class
    List<InventoryItem> displayList;  // List of items to be referenced by the display
    int lstSize = 0;

    int dispIndex = 0;
    int cursorPos = 0;  // Can go from 0-2, 0 being topmost, 2 being bottommost

    //Faustine's added fields
    [SerializeField] Scrollbar scrollbar;
    
    //actions
    [SerializeField] GameObject actionsPanel;
    bool actionsOn = false;
    [SerializeField] KeyCode actionKey = KeyCode.Space; //optional

    //INVENTORY DISPLAY
    [Header("Item Displays")]
    [SerializeField] Image itemIcon; //image of items in game
    [SerializeField] Text itemDescription; //item description / tooltip in inventory

    InventoryItem itemSelected; //item currently being selected

    // Update is called once per frame
    void Update()
    {
        //INVENTORY LIST AND CURSOR CODE
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursorDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursorDown(true);
        }
        GetInventory();
        RenderDisplay();

        //Faustine's added methods: visuals
        SetScrollbar();
        ActionPanelToggle();
        DisplayItemInfo();
    }

    private void Start()
    {
        invIndex = Resources.FindObjectsOfTypeAll<InventoryItem>();
        foreach (InventoryItem item in invIndex)
        {
            Debug.Log(item);
        }
        displayList = new List<InventoryItem>();
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
            foreach (InventoryItem item in invIndex)  // Find matching InventoryItem with the IDName
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
        }
        else
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

        //alternative implementation: create an array of the images, if cursorpos = img index, set alpha to 1f, otherwise set it to 0.5f
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

    // Returns the InventoryItem that cursor is pointing at
    private InventoryItem GetItemOnCursor()
    {
        return displayList[dispIndex + cursorPos];
    }

    private void SetScrollbar()
    {
        //SCROLLBAR MATH (OPTIONAL); make uninteractable, change disabled color; just for a visual indicator
        int setNumber = 3; //every set has 3 objects on screen at a time
        float setCount = lstSize - setNumber; //max count for sets possible; for each one increase from set number, the set count increases by one

        float handleSize = 1 / setCount; //hand should represent one set out of total number of sets
        scrollbar.size = handleSize; //set handle size to represent sets of 3

        float handleVal = dispIndex * handleSize; //how far the handle should go
        scrollbar.value = handleVal; //set handle position to the distance down the list

        //why did six lines of code take me two hours -- Faustine
    }

    private void ActionPanelToggle()
    {
        //ACTIONS
        if (Input.GetKeyDown(actionKey)) //key choice can be changed
        {
            actionsOn = !actionsOn;
        }
        actionsPanel.SetActive(actionsOn);

        if (actionsOn && Input.GetKeyDown(KeyCode.Alpha1)) //accessing different actions via numeric keys, or shift cursor to actions panel;
                                                           //actions can display like inventory list OR like combat choices (how many total actions?) https://answers.unity.com/questions/420324/get-numeric-key.html
        {
            Debug.Log("action 1 initiated");
            //DO SOMETHING with itemSelected
        }
    }

    private void DisplayItemInfo()
    {
        //ITEM ICON AND TOOLTIP DISPLAY
        //canvas set to scale with screen size, note anchors (if not scale with screen size, it will scale based on the anchors)

        itemSelected = GetItemOnCursor(); //get item currently selected
        itemIcon.sprite = itemSelected.GetIcon(); //change the sprite icon displayed (.sprite takes in sprites, Image types doon't automatically do that)
        //different ways to manage icon size on the left:
        //itemIcon.SetNativeSize(); //IF you want to set your image to your item sprite native size
        itemDescription.text = itemSelected.GetToolTip(); //change the item description displayed
                                                          //.text takes in strings, which we return with the gettooltip function, if not .text the class type is Text, which is a mismatch
                                                          //optional: check "best fit" in the editor for text box

        //resolve overflow, easiest way is to use text mesh pro - reference past projects and set auto sizing to determine max and min font size and what to do with overflow text
    }
}
