using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayAdv : MonoBehaviour
{
    //INVENTORY LIST
    [Header("Inventory List")]
    [SerializeField] GameObject inventoryWindow;
    [SerializeField] GameObject invSlotPrefab; //inventory slot holder prefab
    public List<GameObject> invSlotList = new List<GameObject>(); //queue of inventory slots
    public List<Image> images = new List<Image>();
    [SerializeField] [Range(1, 8)] int setNumber = 3;
    int prevSetNum;
    bool setNumChanged = false;

    //INVENTORY LIST
    [Header("Inventory List")]
    [SerializeField] Inventory player;

    InventoryItem[] invIndex;  // Master list of all InventoryItems
    Dictionary<string, int> inventory;  // Dictionary passed in from the Inventory class
    List<InventoryItem> displayList;  // List of items to be referenced by the display
    int lstSize = 0;

    int dispIndex = 0;
    int cursorPos = 0;  // Can go from 0-2, 0 being topmost, 2 being bottommost

    //Faustine's added fields
    [SerializeField] Scrollbar scrollbar;

    //UNUSED check for disabled inventory slots
    //bool invDisabled = false;

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
    private void Start()
    {
        invIndex = Resources.FindObjectsOfTypeAll<InventoryItem>();
        foreach (InventoryItem item in invIndex)
        {
            Debug.Log(item);
        }
        displayList = new List<InventoryItem>();
        
        CreateInvSlots();
    }

    private void Update()
    {
        if (setNumChanged)
        {
            CreateInvSlots();
            setNumChanged = false;
        }

        //INVENTORY LIST AND CURSOR CODE
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursorDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursorDown(true); //the reversed booleans are a little confusing, perhaps make it so that when we are moving up, the bools say false?
        }
        GetInventory();
        RenderDisplay();

        //Faustine's added methods: visuals
        SetScrollbar();
        ActionPanelToggle();
        DisplayItemInfo();
    }

    public void ValueChanged(float value) //when the slider is changed, update setnumber
    {
        prevSetNum = setNumber;
        setNumber = Mathf.RoundToInt(value);
        setNumChanged = true;
    }

    void CreateInvSlots()
    {
        if(cursorPos > setNumber - 1) //if cursor is out of bounds, reset it
        {
            cursorPos = setNumber - 1;
        }else if (cursorPos < 0) //logically won't happen, but just in case
        {
            cursorPos = 0;
        }

        if(dispIndex == lstSize - prevSetNum && (dispIndex - 1) >= 0 ) //if on last set, which causes bugs, shift up
        {
            dispIndex--;
        }


        int invSlotCount = invSlotList.Count; //count number of items in invSlotList (or a set)
        images.Clear();
        if (invSlotCount < setNumber) //if too few
        {
            for (int i = invSlotCount; i < setNumber; i++) //instantiate until you reach the set number needed
            {
                GameObject invSlot = Instantiate(invSlotPrefab, inventoryWindow.transform);
                invSlot.SetActive(false);
                invSlotList.Add(invSlot);
            }
        }
        else if (invSlotCount > setNumber) //if too many, inactivate extras
        {
            for (int i = setNumber; i < invSlotCount; i++)
            {
                GameObject invSlot = invSlotList[i];
                invSlot.SetActive(false);
            }
        }

        for (int i = 0; i < setNumber; i++) //set remaining inventory slots active
        {
            GameObject invslot = invSlotList[i];
            Image image = invslot.GetComponentInChildren<Image>();
            images.Add(image); //add images to list to access
            invslot.SetActive(true);
        }
        
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
            if (cursorPos == setNumber - 1) //if the cursor position is the bottommost slot
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
            if (dispIndex + 1 > lstSize - setNumber) //if display of the set goes out of bounds
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
        for (int i = 0; i < setNumber; i++)
        {
            DisplayItemName(i); //display items corresponding to max number possible on screen (i.e. 3)
        }

        // Change the alpha to highlight the cursor
        foreach (Image image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f); //first set all to half transparent
        }
        images[cursorPos].color = new Color(images[cursorPos].color.r, images[cursorPos].color.g, images[cursorPos].color.b, 1f); //then set selected to opaque

    }

    private void DisplayItemName(int order) //taking image index as input
    {
        if (dispIndex + order < lstSize)
        {
            images[order].GetComponentInChildren<Text>().text = displayList[dispIndex + order].GetDisplayName(); //find corresponding item with its index, display
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
        float setCount = lstSize - setNumber; //max count for sets possible; for each one increase from set number, the set count increases by one

        if (setCount <= 0)
        {
            setCount = 1; //if the list is smaller than the possible number of items on screen, there could only be one set possible
        }

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
        itemDescription.text = itemSelected.GetToolTip(); //change the item description displayed
    }
}
