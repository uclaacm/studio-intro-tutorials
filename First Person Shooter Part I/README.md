# Studio Beginner Tutorials - First Person Shooter Part 1
  
**Date**: November 14, 2021, 7:00 pm - 9:00 pm<br>
**Location**: Faraday Room 67-124 (Engineering IV)<br>
**Instructors**: Aaron, Ryan
 
## Resources
[Slides](https://docs.google.com/presentation/d/1HeNhXbYw5ydabzrZ79q-_CXQgvbaTx2jRu7gqmSFw3M/edit?usp=sharing)<br>
[Video Soon!](Soon)
 
## Topics Covered
* 3D Movement
* Basic 3D animation
* Some Cinemachine
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [TODO: Skeleton Package](package_link)

---

## Setting Up Your Scene
In order to start, please download and import the [TODO: skeleton package](package_link) into your own Unity 3D project.

---

## Player
### Setup + Movement
Create an empty GameObject. Add a Rigidbody and Capsule Collider. The Capsule Collider ensures that the player doesn't fall through the ground or go through walls. Make sure that the Rigidbody has 'Use Gravity' checked and has mass so that the character is affected normally by gravity.

To get our player movement, we attach a `PlayerInput` component with the default action map and a `CharacterMovement` [script](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/3d-fps-part-i/First%20Person%20Shooter%20Part%20I/Assets/Scripts/CharacterMovement.cs) which is similar to the Roll a Ball and 2D Platformer movement scripts but with C# Events instead of Broadcasted Events.

Within our CharacterMovement script, we start by defining a few SerializeField variables: headCamera and maxSpeed. These variables will help with attaching the camera to the head of the player and making our character's speed editable from the inspector. Then a few variables are defined and initialized for future use. The `Awake()` function is called once before the game starts and is often used to set up references to other scripts and GameObjects, which is exactly what we're doing in our `Awake()` function. Specifically, we are retrieving references to the InputAction from the keyboard, animator, and additionally initializing the cursor to be locked and invisible.

The next part of our code focuses on calling the `Update()` function. Within this function, we set a new `Vector3 deisredForwardVector` to be in the direction that the player is facing (which is based off the camera) without accounting for the y-axis yet because it doesn’t affect movement. Then we rotate the player in this direction with `transform.forward = desiredFowardVector` so that the player is facing the desired direction. After that, we calculate the velocity of the player based on their current velocity and the desired velocity obtained from player input. We use the `Mathf.SmoothDamp` to calculate this velocity because it allows for smooth transitions between movements. Next, we check if the player is falling or landing. If the player is falling, we apply gravity to the vertical speed and if the player is landing, we make the vertical speed 0. Lastly, we move the player with `transform.Translate` and animate the character by calling `animator.SetFloat`.

We want to next check if our player is grounded or not. To do this, we first create a few SerializeField floats to define the dimensions of a box which represents the area at which the player's feet register contact with the ground. We then draw this box (for testing purposes) and use this box to determine if the player is touching the ground. We do this in the FixedUpdate() function to keep our frames in-check with the physics engine.

![Screenshot](Screenshots/image1.png)<br>

Moving on to the smaller functions of our code, we call `OnEnable()` and `OnDisable()` to set up listeners to perform actions only when inputs are received from the keyboard. `HandleChangedMoveDirection` and `HandleCanceledMoveDirection` are used to detect when new directions are inputted into the keyboard and released from the keyboard and changes `desiredDirection` to correspond with them, which is like `OnMove()` from previous tutorials. The last function, `HandleJump()`, is similar to `OnJump()` from previous tutorials and handles the jumping mechanics of the player. The player only jumps when grounded and the vertical speed of the player is also calculated here. 

## Reading User Input
In previous tutorials you learned that Unity's new input system can notify scripts when new inputs have been received by invoking special functions you implement in those scripts that have names corresponding to your defined Actions. In those special functions, scripts can process the new input data however it needs (such as moving the character forward or jumping). 

In this tutorial, we will be using a slightly different approach that makes use of C# events. The base concept is the same -- implement a special function in your script that will process incoming inputs from the input system. The main difference is that instead of letting Unity wire up these special functions to be called by the input system, we will be the ones to do it this time.

Well then why would I want to wire it up myself when Unity can do it for me?

As with all things, doing things yourself gives you more flexibility since you are in control of the implementation. One strength of C# events is that they can be heard by GameObjects that are not directly connected to the PlayerInput object.

## Using Cinemachine for FPS POV


--- 

```c#   
empty script.
```
---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
