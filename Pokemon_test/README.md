# Studio Beginner Tutorials - Build an RPG: Combat

**This is still a work in progress, we'll try to get the whole README done soon**
**Date**: January 18, 2022, 7:00 pm - 9:00 pm<br>
**Location**: Zoom<br>
**Instructors**: Connor, Ryan

## Resources
[Slides](https://docs.google.com/presentation/d/11wem-UyzL3qNAjySi4kznZZQPnx7d6gA_zXDun6X59s/edit?usp=sharing)<br>
[Video Soon!](Soon)
 
## Topics Covered
* 
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [Skeleton Package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing)

---

## Setting Up Your Scene
In order to start, please download and import the [skeleton package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing) into your own Unity 2D project.
---

## The Scene
### Overview
The [skeleton package](https://drive.google.com/file/d/1_rJrWlnJ4S6iisUc1YaWXbiayqx7GPou/view?usp=sharing) includes all the assets needed for the scene and has things set up so that we can add scripts to make it playable. Before we get into that though, we want to go over what is in the scene. The scene should look like this:

![Screenshot]() ADD SCREENSHOT<br>

The first thing to note is the BattleSystem object. This is an Empty to which we will attach the script that will run the battle

## Executing Moves
After the `Fight` option has been selected, we want to display the moves of the player's pokemon. In the skeleton package, we have already provided the canvas
elements of placeholder moves and now we have to populate them with the player's pokemon's moves and activate them on the player's command. 

#### Setting Inputs
We can start by continuing to edit our BattleSystem script. After the player presses `Spacebar` or `Q` while hovering over the `Fight` button in the `ActionSelect` Phase, we want to change the current phase to the `MoveSelct` Phase. To do this, in the Update() function we can change the phase by setting `phase = Phases.MoveSelect`. Then we want to toggle the standard dialog **off**, the action select menu **off**, and the move select menu **on**. 

<br>

Now we can move on to the MoveSelect case of our switch statement. First, if the player wants to exit the move select phase, they can do so by pressing the `Escape` key. To implement this, we can change the phase by setting `phase = Phases.ActionSelect`, toggling the standard dialog **on**, the action select menu **on**, and the move select menu **off**. Next, we want to make it so that pressing the `Right Arrow` or `D` key will select the right move option and pressing the `Left Arrow` or `A` key will select the left move option (there are only two moves per pokemon in this project). To do this, using our previously defined `currentMove` integer, we can increment the `currentMove` integer by 1 if the player presses a right input and decrement it by 1 if the player presses a left input. By making this integer lower bounded by **0** and upper bounded by **1**, we can make it so that **0** represents the left move option and **1** represents the right move option. 

<br>

Lastly, when the player presses the `Spacebar` key while hovering over a specific move, we want to execute the move. To begin this process, we can toggle the standard dialog **on** and the move select menu **off** since we'll need to see some dialog about the pokemon and move and we no longer need to see the move select menu. Then we will have to start executing the coroutine which actually executes the attacks. 

#### Attacking
Lets begin by implementing the `ExecuteMoves()` function which will execute attacks from both the player and the enemy. We can start by getting the move which the player selected. In the previous section, we saved the index of the move the player selected in the `currentMove` int, so we can get the pokemon's move at that index by defining `string move = player.basePokemon.moves[currentMove];` which is the name of the move the player selected. Next we can display in the dialog box that the player's pokemon used that move by setting `dialogText.DialogText.text = $"{player.basePokemon.name} used {move}.";`.

<br>

In the way this project is structured, there are no speed stats so the player's pokemon always attacks before the enemy pokemon. So now we have to inflict damage onto the enemy pokemon by calling the `takeDamage` function. 


---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)