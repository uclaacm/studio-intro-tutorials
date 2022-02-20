using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ShopDisplay : MonoBehaviour
{
    //INVENTORY LIST - 
    [Header("Inventory List")]
    [SerializeField] Image[] images;
    [SerializeField] Inventory player;
    InventoryItem[] invIndex;  
    Dictionary<string, int> inventory;  
    Dictionary<InventoryItem, int> inventoryDict;
    List<InventoryItem> displayList;  
    int lstSize = 0;
    int dispIndex = 0;
    int cursorPos = 0;  
    [SerializeField] int setNumber = 3; //number of items on screen at once // change
    [SerializeField] Scrollbar scrollbar;
    //actions
    [SerializeField] GameObject actionsPanel;
    bool actionsOn = false;
    [SerializeField] KeyCode actionKey = KeyCode.Space; //optional

    //Item DISPLAY
    [Header("Item Displays")]
    [SerializeField] Image itemIcon; //image of items in game
    [SerializeField] TextMeshProUGUI tooltip;
    InventoryItem itemSelected; //item currently being selected

    //SHOP DISPLAY
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI coinstext;
    [SerializeField] TextMeshProUGUI inventoryList;
    int coins = StartScene.coins; //get coins from the startscene which intializes and stores the value

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

        //Integrate: exist the shop
        if (Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadScene("OverworldScene");

        ActionPanelToggle();
    }

    private void OnEnable()
    {
        GetShop();
        RenderDisplay();
    }

    private void Awake()
    {
        invIndex = Resources.FindObjectsOfTypeAll<InventoryItem>();
        foreach (InventoryItem item in invIndex)
        {
            Debug.Log(item);//helpful for debugging purposes
        }
        displayList = new List<InventoryItem>();
    }

    // Update shop display
    void GetShop()
    { 
        inventoryDict = player.GetItemDict();
        List<InventoryItem> killList = new List<InventoryItem>();
        // Remove any items that are purchased from display
        foreach (InventoryItem item in displayList)
        {
            if (!inventoryDict.ContainsKey(item) || item.getPurchased())
            {
                killList.Add(item);
            }
        }
        foreach (InventoryItem item in killList)
        {
            displayList.Remove(item);
            lstSize--;
        }
        foreach (InventoryItem item in inventoryDict.Keys)  // Search through the inventory dictionary
        {
            if (!displayList.Contains(item) && !item.getPurchased()) //if not in display and not purchased, add to display list
            {
                Debug.Log(item.getPurchased());
                displayList.Add(item);
                lstSize++;
            }
        }
    }

    // Move cursor 
    void MoveCursorDown(bool down = true)
    {
        if (down)
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
        else
        {
            if (cursorPos == 0)  // If cursor pointing at topmost slot
            {
                ShiftDisplayDown(false);  // Attempt to shift display up
            }
            else
            {
                cursorPos--;
            }
        }
    }

    // Attempts to shift the displayed section of array, returning false if unable to
    bool ShiftDisplayDown(bool down = true)
    {
        if (down)
        {
            if (dispIndex + 1 >= lstSize - 2)
            {
                return false;
            }
            else
            {
                dispIndex++; //shift down if you can
                return true;
            }
        }
        else
        {
            if (dispIndex - 1 < 0)
            {
                return false;
            }
            else
            {
                dispIndex--; //shift up if you can
                return true;
            }
        }
    }

    // Display the shop slots, rendering each slot with its corresponding image
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

        //DISPLAY INFO FUNCTION
        try
        {
            itemSelected = GetItemOnCursor(); //get item currently selected
            itemIcon.sprite = itemSelected.GetIcon(); //change the sprite icon displayed (.sprite takes in sprites, Image types doon't automatically do that)
            tooltip.text = itemSelected.GetToolTip(); //change the item description displayed
            //display price and coins
            price.text = "Price: " + itemSelected.getPrice().ToString();
            coinstext.text = "Coins: " + coins;
            inventoryList.text = getInventory();
        }
        catch
        {
            itemIcon.sprite = null; 
            tooltip.text = "No Item Selected."; //make variable if wanted
            price.text = "N/A";
            coinstext.text = "N/A";

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
            // find corresponding item with its index, display
            images[order].GetComponentInChildren<TextMeshProUGUI>().text = displayList[dispIndex + order].GetDisplayName();
        }
        else
        {
            images[order].GetComponentInChildren<TextMeshProUGUI>().text = " ";  // Set to blank
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
        float setIndex = lstSize - setNumber; //max count for sets possible; for each one increase from set number, the set count increases by one

        if (setIndex <= 0)
        {
            setIndex = 1; //if the list is smaller than the possible number of items on screen, there could only be one set possible
        }

        float handleSize = 1 / setIndex; //hand should represent one set out of total number of sets
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

        if (actionsOn && Input.GetKeyDown(KeyCode.Alpha1))//if the user selects "purchase"
        { 
            Debug.Log("action 1 initiated");
            BuyItem(GetItemOnCursor());
            actionsOn = !actionsOn;
            GetShop(); //update available items
        }
        else if (actionsOn && Input.GetKeyDown(KeyCode.Alpha2)) //if the user selects "back"
        {
            actionsOn = !actionsOn;
            GetShop(); //update available itesm
        }
        RenderDisplay(); //display items
    }

    // Actions


    void BuyItem(InventoryItem item)
    {
        //make sure there are enough coins left
        if (coins - item.getPrice() >= 0)
        {
            item.purchase();
            coins -= item.getPrice();
        }
    }

    string getInventory()//output list of purchased items
    {
        string inventory = "Inventory List: \n";
        //surch through items and add purchased items
        foreach (InventoryItem item in Resources.FindObjectsOfTypeAll<InventoryItem>()) 
        {
            if (item.getPurchased()) //
                inventory = inventory + item.GetDisplayName() + "\n";
                    
        }
        return inventory;
    }

    
}