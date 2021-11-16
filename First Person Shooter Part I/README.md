# Studio Beginner Tutorials - First Person Shooter Part 1
  
**Date**: November 14, 2021, 7:00 pm - 9:00 pm<br>
**Location**: Faraday Room 67-124 (Engineering IV)<br>
**Instructors**: Aaron, Ryan
 
## Resources
[Slides](https://docs.google.com/presentation/d/1HeNhXbYw5ydabzrZ79q-_CXQgvbaTx2jRu7gqmSFw3M/edit?usp=sharing)<br>
[Video](Soon)
 
## Topics Covered
* 3D Movement
* Basic 3D animation
* Some Cinemachine
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)

---

## Setting Up Your Scene
`There's some stuff to set up.`

### Importing Assets
`Whatever assets Aaron imported, idk.`

### Creating Base Level
`The thing Aaron did to create the base level. Add colliders, some other stuff. `

---
 
## Player
### Setup + Movement
Create an empty GameObject. Add a Rigidbody and Capsule Collider. `Idk why Aaron added the Capsule Collider`. Make sure that the Rigidbody has 'Use Gravity' checked and has mass so that the character is affected normally by gravity.

To get our player movement, we attach a `PlayerInput` component with the default action map and a `CharacterController` [script](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/3d-fps-part-i/First%20Person%20Shooter%20Part%20I/Assets/Scripts/CharacterMovement.cs) which is similar to the Roll a Ball and 2D Platformer movement scripts but with some unique attributes.

Within our CharacterController script, we start by defining a few SerializeField variables: headCamera and maxSpeed. These variables will help with attaching the camera to the head of the player and making our character's speed editable from the inspector. Then a few variables are initialized for future use. The `Awake()` function is called once before the game starts and is often used to set up references to other scripts and GameObjects, which is exactly what we're doing in our `Awake()` function. Specifically, we are retrieving references to the InputAction from the keyboard, animator, and additionally initializing the cursor to be locked and invisible.

The next part of our code focuses on calling the `FixedUpdate()` function. We call `FixedUpdate()` instead of `Update()` to keep our frames in-check with the physics engine. Within this function, we set a new `Vector3 deisredForwardVector` to be in the direction that the player is facing (which is based off the camera) without accounting for the y-axis yet because it doesn’t affect movement. Then we rotate the player in this direction with `transform.forward = desiredFowardVector` so that the player is facing the desired direction. After that, we calculate the velocity of the player based on their current velocity and the desired velocity obtained from player input. We use the `Mathf.SmoothDamp` to calculate this velocity because it allows for smooth transitions between movements. Lastly, we move the player with `transform.Translate` and animate the character by calling `animator.SetFloat`.

Moving on to the smaller functions of our code, we call `OnEnable()` and `OnDisable()` to set up listeners to perform actions only when inputs are received from the keyboard. `HandleChangedMoveDirection` and `HandleCanceledMoveDirection` are used to detect when new directions are inputted into the keyboard and released from the keyboard.

## Reading User Input
In previous tutorials you learned that Unity's new input system can notify scripts when new inputs have been received by invoking special functions you implement in those scripts that have names corresponding to your defined Actions. In those special functions, scripts can process the new input data however it needs (such as moving the character forward or jumping). 

In this tutorial, we will be using a slightly different approach that makes use of C# events. The base concept is the same -- implement a special function in your script that will process incoming inputs from the input system. The main difference is that instead of letting Unity wire up these special functions to be called by the input system, we will be the ones to do it this time.

Well then why would I want to wire it up myself when Unity can do it for me?

As with all things, doing things yourself gives you more flexibility since you are in control of the implementation. One strength of C# events is that it is 

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
 
