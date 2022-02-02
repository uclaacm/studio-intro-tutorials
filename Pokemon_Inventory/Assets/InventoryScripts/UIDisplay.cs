using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    //INVENTORY LIST
    [Header("Inventory List")]
    [SerializeField] Image[] invSlots;
    
    [SerializeField] Inventory player;
    InventoryItem[] invIndex;  // Master list of all InventoryItems
    Dictionary<InventoryItem, int> inventoryDict; // Dictionary passed in from the Inventory class
    List<InventoryItem> displayList;  // List of items to be referenced by the display
    int lstSize = 0;
    int dispIndex = 0;
    int cursorPos = 0;  // Can go from 0-2, 0 being topmost, 2 being bottommost
    
    //Faustine's added fields
    [SerializeField] int setNumber = 3; //number of items on screen at once
    [SerializeField] Scrollbar scrollbar;
    
    //actions
    [SerializeField] GameObject actionsPanel;
    bool actionsOn = false;
    [SerializeField] KeyCode actionKey = KeyCode.Space; //optional

    //INVENTORY DISPLAY
    [Header("Item Displays")]
    [SerializeField] Image itemIcon; //image of items in game
    [SerializeField] TextMeshProUGUI tooltip; //item description / tooltip in inventory

    InventoryItem itemSelected; //item currently being selected

    // Update is called once per frame
    void Update()
    {
        //INVENTORY LIST AND CURSOR CODE
        if (Input.GetKeyDown(KeyCode.DownArrow) && !actionsOn)
        {
            MoveCursorDown();
            RenderDisplay();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !actionsOn)
        {
            MoveCursorDown(false);
            RenderDisplay();
        }
        
        

        //Faustine's added methods: visuals
        ActionPanelToggle();
    }

    private void OnEnable()
    {
        GetInventory();
        RenderDisplay();
    }

    private void Awake()
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
        inventoryDict = player.GetItemDict();
        // TODO: Remove any items no longer in inventory from display

        // Add items to display
        foreach (InventoryItem item in inventoryDict.Keys)  // Search through the inventory dictionary
        {
            // TODO: Add new items to the display
        }
    }

    // Move cursor 
    void MoveCursorDown(bool down = true)
    {
        if (down)
        {
            // TODO: If cursor is at bottom of screen, shift screen down
            // Otherwise, if cursor is not pointing out of inventory range, shift cursor down
        }
        else
        {
            if (cursorPos == 0)  // If cursor pointing at topmost slot
            {
                // TODO: Attempt to shift screen up
            }
            else
            {
                // TODO: Shift cursor up
            }
        }
    }

    // Attempts to shift the displayed section of array, returning false if unable to
    bool ShiftDisplayDown(bool down = true)
    {
        if (down)
        {
            // TODO: If shifting the screen down will go past the last item slot, return false
            // Otherwise, shift screen down 
                return true;
        }
        else
        {
            if (dispIndex - 1 < 0)  // If shifting up hits the top of the item list, don't 
            {
                return false;
            }
            else
            {
                // TODO: Shift up
                return true;
            }
        }
    }

    // Display the Inventory slots, rendering each slot with its corresponding image - Faustine
    void RenderDisplay()
    {
        for (int i = 0; i < setNumber; i++)
        {
            DisplayItemName(i); // display items corresponding to max number possible on screen (i.e. 3)
        }

        // Change the alpha to highlight the cursor
        foreach (Image image in invSlots)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f); //first set all to half transparent
        }
        invSlots[cursorPos].color = new Color(invSlots[cursorPos].color.r, invSlots[cursorPos].color.g, invSlots[cursorPos].color.b, 1f); //then set selected to opaque

        //OLD DISPLAY INFO FUNCTION
        try
        {
            itemSelected = GetItemOnCursor(); //get item currently selected
            itemIcon.sprite = itemSelected.GetIcon(); //change the sprite icon displayed (.sprite takes in sprites, Image types doon't automatically do that)
            tooltip.text = itemSelected.GetToolTip(); //change the item description displayed
        } catch
        {
            // TODO: Set all to a default blank display
            itemIcon.sprite = null; //idk if this works
            tooltip.text = "No Item Selected."; //make variable if wanted

            if (actionsOn) //if they try to turn on action panel, it won't
            {
                actionsOn = false;
            }
        }

        SetScrollbar(); //set the scroll bar when rendered
    }

    private void DisplayItemName(int order) //taking image index as input
    {
        if (dispIndex + order < lstSize)
        {
            invSlots[order].GetComponentInChildren<TextMeshProUGUI>().text = displayList[dispIndex + order].GetDisplayName(); //find corresponding item with its index, display
        } else
        {
            invSlots[order].GetComponentInChildren<TextMeshProUGUI>().text = " ";  // Set to blank
        }
    }

    // Returns the InventoryItem that cursor is pointing at
    private InventoryItem GetItemOnCursor()
    {
        return displayList[0];  // TODO: Fix this.
    }

    private void SetScrollbar()
    {
        //SCROLLBAR MATH (OPTIONAL); make uninteractable, change disabled color; just for a visual indicator
        int setNumber = 3; //every set has 3 objects on screen at a time
        float setCount = lstSize - setNumber; //max count for sets possible; for each one increase from set number, the set count increases by one
        
        if (setCount <= 0)
        {
            setCount = 1; //if the list is smaller than the possible number of items on screen, there could only be one set possible
        }

        float handleSize = 1 / setCount; //hand should represent one set out of total number of sets
        scrollbar.size = handleSize; //set handle size to represent sets of 3

        float handleVal = dispIndex * handleSize; //how far the handle should go
        scrollbar.value = handleVal; //set handle position to the distance down the list
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
            // Should only be allowed in battle scene
            Debug.Log("action 1 initiated");
            // TODO: Use item
            GetInventory();  // Update inventory 
        } else if (actionsOn && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("action 2 initiated");
            // TODO: Drop Item
            GetInventory();
        }
        RenderDisplay();
    }

    // Actions

    // Only intended for use in combat as of right now, communicate with combat script to heal, return the healPower of item
    int UseItem(InventoryItem item)
    {
        // TODO: Implement this
        return 0;
    }

    // Deletes item
    bool DropItem(InventoryItem item)
    {
        // TODO: Implement this
        return false;
    }


}
