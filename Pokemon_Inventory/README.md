# Studio Beginner Tutorials - Build an RPG: Inventory & UI

**Date**: February 2, 2022, 7:45 pm - 9:15 pm<br>
**Location**: Zoom<br>
**Instructors**: Faustine, Ming

## Resources
[Slides](https://docs.google.com/presentation/d/1B2nlqguwmeTMIzJ7fGrFS8XrE7aNxKOUwePROp328Ac/edit?usp=sharing)<br>
[Video Soon!](Soon)
 
## Topics Covered
* Managing Items using Scriptable Objects
* UI Concept and Implementation
* Integrating UI with Combat and Overworld
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [Skeleton Package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing)

---

## Final Product
![]()

## Setting Up Your Scene
In order to start, please download and import the [skeleton package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing) into your own Unity 2D project. Note: all finalized scripts can be found [here](https://github.com/uclaacm/studio-beginner-tutorials/tree/main/Pokemon_Combat/Assets/Scripts/Battle) or under `/Assets/Scripts/Battle/` within the readme. 
---

## The Scene
The [skeleton package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing) includes all the assets needed for the scene and has a framework set up to which we can add scripts. After you import the package, the scene should look like this:

![Screenshot](Screenshots/battle_scene.png)<br>

The first thing to note is the BattleSystem object. This is an Empty to which has the script that will run the battle. Nested under this is the UI canvas which everything you can see in the scene are on. We have set the `Render Mode` to `Screen Space - Camera` and we've set the `Render Camera` to the Main Camera. We also set the `UI Scale Mode` to `Scale With Screen Size` and the `Reference Pixels Per Unit` to 32. For the Main Camera, we set the `Projection` to `Orthographic`. This makes it so we can directly add UI elements where we want them on the scene.

Everything else on the scene are UI elements, either images or text, that we have set in the right places and attached the right sprites to. The player and enemy objects will be the Pokemon for the player and their opponent. The dialog object will be where we display the text for the game. DialogText will display text detailing what is happening in the game. ActionSelector is where the option to attack or use an item is displayed. MoveSelector is disabled now, but we will enable it using a script when it is time for the player to select a move. The PlayerHud and EnemyHud are where the names of the Pokemon and their Hp will be displayed.

## Initial Scripts
We have provided some initial scripts that we will be using in this project. These scripts are not too long, so we don't want to spend a long time going over them, but we will provide a brief description of what they do.

### InventoryItem

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order=1)]
public class InventoryItem : ScriptableObject
{
    
}
```

In this file,  we want to define some variables that we will use. Let's define the following:

```c#
[SerializeField] string IDName;  // The name we will reference in our code
[SerializeField] string displayName; // The name that the player sees
[SerializeField] [TextArea] string tooltip; // Description for the object
[SerializeField] Sprite icon;  // The icon displayed with the object

```

Note that each attribute is made Serializable for ease of access. SerializeField makes that variable appear in the "properties" section of Unity. For tooltip, we use TextArea in order to display a large text box for the SerializeField for tooltip. 

It's good practice to always use getter functions to access member variables in classes, so even though it might seem like a massive waste of time, let's define them :>

```c#
// PUBLIC 
    public string GetIDName()
    {
        return idName;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public Sprite GetIcon()
    {
        return icon;
    }
    
    public string GetToolTip()
    {
        return tooltip;
    }
```

![Screenshot](Screenshots/charizard_base.png)<br>

![Screenshot](Screenshots/blastoise_base.png)<br>

### Inventory
Now that we have our InventoryItem done, let's implement the Inventory that controls these objects. We'll leave Inventory as a MonoBehaviour. The main data structure within Inventory will be a Dictionary mapping a value of type int to a key of type InventoryItem. 

```c#
Dictionary<InventoryItem, int> inventory;
```



### curr_pokemon
This is on both the player and enemy objects. It will create a new instance of the Pokemon on setup and will display the correct sprite based on which Pokemon it is and if it is facing the player or the enemy

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class curr_pokemon : MonoBehaviour
{
    public bool isPlayer;
    public BasePokemon basePokemon;
    public Pokemon pokemon;
    public void SetUp()
    {
        pokemon = new Pokemon(basePokemon);   
        if (isPlayer)
        {
            this.GetComponent<Image>().sprite = basePokemon.back_sprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = basePokemon.front_sprite;
        }
    }
}
```

We have added the BasePokemon we created earlier into the variable for basePokemon for the enemy and player objects.

### HudScript
This script is on each of the Hud objects and will have as variables the name text and the instance of the hpScript that is on the Hp object in that Hud. This script has one function that takes a Pokemon object as input will change the name text to the correct name and set the maxHp of that pokemon in the hpScript (which we will show later)

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hudScript : MonoBehaviour
{
    public Text nameText;
    //public GameObject hpBar;
    public hpScript hp;
    public void setHud(Pokemon pokemon)
    {
        nameText.text = pokemon.basePokemon.name;
        //hpBar.GetComponent<hpScript>().maxHp = pokemon.basePokemon.maxHp;
        hp.maxHp = pokemon.basePokemon.maxHp;
    }
}
```

## Starting the Battle
### Battle Setup
Now we are ready to start working on the logic that makes the battle playable. To do this we will make a `BattleSystem` script that we will attach to the BattleSystem object in the scene. This script will have variables for the things in the scene that we need to be able to change as the battle progresses:

```c#
    public dialogScript dialogText;
    public Text fightText;
    public Text bagText;
    public hudScript playerHud;
    public curr_pokemon player;
    public hudScript enemyHud;
    public curr_pokemon enemy;
```

We need a way to know what is happening in the battle, so we will make an enum for the different phases of the battle and have a variable of this enum storing the current phases. We've broken down the phases into: setting up the battle, selecting an action (fight/bag), selecting an attack, selecting an item, and the attack phase.

```c#
    private enum Phases { SetUp, ActionSelect, MoveSelect, ItemSelect, Attacks };
    private Phases phase = Phases.SetUp;
```

For our game setup we will want to do some waiting, so we will make a `public IEnumerator Setup()` function and in the `Start()` function we will call that function as a coroutine. In the `Setup()` function we will set up the current pokemon and the Huds and we will display an intro message. Then we will wait for a second and switch to the `ActionSelect` phase and prompt the user for input. 

```c#
void Start()
    {
        StartCoroutine(Setup());
    }
    public IEnumerator Setup()
    {
        player.SetUp();
        enemy.SetUp();
        playerHud.setHud(player.pokemon);
        enemyHud.setHud(enemy.pokemon);
        dialogText.DialogText.text = "A wild " + (string)(enemy.basePokemon.name) + " appeared";
        yield return new WaitForSeconds(1f);
        phase = Phases.ActionSelect;
        dialogText.DialogText.text = "What will you do?";
    }
```

### Selecting an Action
To allow the player to select an action we need to make a `dialogScript` which we will add to the dialog object in the scene. This will have variables for everything that appears in the dialog box in the scene as well as lists for the moves and actions that will be selectable.

```c#
    public Text DialogText;
    public GameObject ActionSelector;
    public GameObject MoveSelector;
    public List<Text> actions;
    public List<Text> moves;
```

For now, all we will do in this script is make 2 functions: one to show which action is being selected, and another to toggle the dialog text on/cff so that we can make it go away when an action has been selected.

```c#
    public void dialogToggle(bool on)
    {
        DialogText.enabled = on;
    }
    public void highlightAction(int selection)
    {
        for(int i = 0; i < actions.Count; i++) { 
            if(i == selection)
            {
                actions[i].color = Color.blue;
            }
            else
            {
                actions[i].color = Color.black;
            }
        }
    }
```

Now we need to make it so that we can select an action in the `BattleSystem` script. First we need to add an `int` variable to track which item in the list we are selecting and we will set this to 0 right after switching to the action selectiong phase. In the `Update` function we will make a switch statement using the `phase` variable. If the phase is `ActionSelect` we will change selected if the player moves the selection up or down using the arrow keys or wasd and let them select a choice if they press 'q' or the spacebar.

```c#
switch (phase)
        {
            case Phases.ActionSelect:
                if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    selected = (selected + 1) % 2; //allows player to loop through action select menu in down direction
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    selected = -1 * (selected - 1) % 2; //allows player to loop through action select menu in up direction
                }
                else if ((Input.GetKeyDown(KeyCode.Space) && selected == 0) || Input.GetKeyDown(KeyCode.Q))
                {
                    phase = Phases.MoveSelect;
                    dialogText.dialogToggle(false);
                    dialogText.actionToggle(false);
                    dialogText.moveToggle(true);
                }
                dialogText.highlightAction(selected);
                break;
```

## Executing Moves
After the `Fight` option has been selected, we want to display the moves of the player's pokemon. In the skeleton package, we have already provided the canvas
elements of placeholder moves and now we have to populate them with the player's pokemon's moves and activate them on the player's command. 

### Setting Inputs
We can start by continuing to edit our BattleSystem script. After the player presses `Spacebar` or `Q` while hovering over the `Fight` button in the `ActionSelect` Phase, we want to change the current phase to the `MoveSelct` Phase. To do this, in the Update() function we can change the phase by setting `phase = Phases.MoveSelect`. Then we want to toggle the standard dialog **off**, the action select menu **off**, and the move select menu **on**. 
