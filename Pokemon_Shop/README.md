# Studio Beginner Tutorials - Build an RPG: Shop
 
**Date**: Februrary th, 2022, 7:00 pm - 9:00 pm<br>
**Location**: Zoom<br> 
**Instructor**: Amy/Heqing Yin
 
## Resources
[Slides](https://docs.google.com/presentation/d/1-Pkv1ArakrWpbGaBlzdFmZH7MKRcwZtHVb-EqT1kG5A/edit?usp=sharing )<br>
[Video](https://www.youtube.com/watch?v=qGQa0KbByiU)<br>
 
## Topics Covered
* Setting up a shop
* Displaying items
* Buying items
* Integrating the four tutorials together!!
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [Skeleton Package](https://drive.google.com/file/d/1dfWnvE7orxTojZJ7phKna36nYpdVWnZr/view?usp=sharing)

---

## Setting Up Your Scene
In order to start, please download and import the [skeleton package](https://drive.google.com/file/d/1dfWnvE7orxTojZJ7phKna36nYpdVWnZr/view?usp=sharing) into your own Unity 2D project.

---
## The Scenes
The [skeleton package](https://drive.google.com/file/d/1dfWnvE7orxTojZJ7phKna36nYpdVWnZr/view?usp=sharing) includes all the assets needed for the Shop scene (and a completed version of the previous week's Overworld Scene) and has things set up so that we can add scripts to make it playable. Before we get into that though, we want to go over what is in the scene. 

The Shop scene should look like this:

It is highly recommended to watch the tutorial for inventory first, as there are a lot of similarities in UIs and scripts!

## Initial Scripts
We have provided some initial scripts that we will be using in this project. These scripts are not too long, so we don't want to spend a long time going over them, but we will provide a brief description of what they do.

## Setting Up A Shop Scene
    public ShopUIController shop;
    // Start is called before the first frame update
    void Start()
    {
        shop.Toggle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("OverworldScene");
        }
    }

## Updating items purchased
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
    
## Displaying items
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


## Purchasing items
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

## Output a list of the inventory
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
    
## Set up a StartScene
    public class StartScene : MonoBehaviour
    {
        public static int coins = 100;
        // Start is called before the first frame update
        void Start()
        {
            foreach (InventoryItem item in Resources.FindObjectsOfTypeAll<InventoryItem>())
            {
                item.reset();
            }
            coins = 100;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                SceneManager.LoadScene("OverworldScene");
        }
    }


## Last Notes
This project is pretty experimental, so if anything seems off please send a message in our discord's `#questions` channel. It's also an extremely downscaled version of a normal turn-based combat game so that we could quickly explain the fundamentals of turn-based combat instead of making our own fleshed out game (which could take very long). 

---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)

## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)

