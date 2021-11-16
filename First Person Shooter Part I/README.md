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
In order to start, please download and import the [TODO: skeleton package](package_link) into your own Unity 3D project. Also make sure that the `Input System`, `Cinemachine`, and `Universal RP` packages are installed. To do this go to Window -> Package Manager -> Unity Registry and install the packages. Then in the project tab under the packages folder inside the assets folder, right click and select Create -> Rendering -> Universal Render Pipeline -> Pipeline Asset (Foward Renderer). Then go back to your main screen and click Edit -> Project Settings -> Graphics and drag your newly created `UniversalRenderPipelineAsset` into the `Scriptable Render Pipeline Settings`.
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

### Is the player touching the ground?
One of the most important aspects of an FPS (and in many platformers) is detecting whether a player is in contact with the ground. This is used to determine how to change the player's vertical velocity to reflect their relation to the ground. For example, if a player is not touching the ground, we want to increase their vertical velocity downward to simulate falling, whereas if the player has their feet firmly planted on the ground, we don't want the player to have any vertical velocity. 

In the previous tutorials, we have used OnCollisionEnter as a way to detect when the player has contacted the ground. A more precise way of ground detection is through box casting, which is just like *ray* casting in that we project something out into space and see if it hits something. More specifically, box casting is when we send out a literal box out into the world and see if it collides with anything. For ground checking, we want to project a box out from the soles of a player's feet and see if it comes into contact with the ground.

Unity's Physics class gives us access to the `Physics.BoxCast()` method, which we can use for this purpose. Read the documentation for `BoxCast()` and see if you can figure out how to cast a box downwards based on the specifications provided under "Ground Check Box". Note that `Physics.BoxCast()` will return a `true` if it contacts something and `false` if not.

### Jumping
We will now implement jumping! We want our character to jump when the input system notifies it that the user has pressed the jump button (in this case, the space key). To do this, all we need to do is set `verticalSpeed` to a positive value and our update loop will translate our character upwards (taking into account the effects of gravity, of course). To make the character jump higher, all we need to do is increase `verticalSpeed`.

However, we can do better than this. A good practice in game development is to make the variables you expose in project editors easily understandable for your fellow game designers. Level designers think in terms of level geometry (like how high a wall should be so that the player cannot jump over it) and would much more appreciate a metric to measure jump power that reflects this way of thinking. Instead of exposing `verticalSpeed` in the editor, We will expose a field called `jumpHeight` which we as the programmers can then use to derive what `verticalSpeed` should be.

Recall that the kinematics equation for the initial velocity of a mass given the distance traveled **d** through an opposing acceleration of magnitude **a** is sqrt(-2 * **a** * **d**). 



## Using Cinemachine for FPS POV

First we need to create a camera to be our FPS view camera. On the toolbar do Cinemachine -> Create Virtual Camera to spawn in a Cinemachine camera. Now, open up the robot player's hierarchy and find the head piece, which is at Robot Player -> Bip001 -> Pelvis -> Spine -> Spine 1 -> Neck -> Head -> HeadNub (if you're having a hard time finding it, you can look at the solution project in the repo too!).

![Screenshot](Screenshots/head_nub_hierarchy.png)<br>

 Drag this transform into the "Follow" field of the Cinemachine camera you have just created. Next to the "Body" tab of the Cinemachine editor panel, change "Transpose" to "Hard Lock To Target" and you'll see your camera zip to the positon of the robot's head. We now want the camera to look in the proper direction, so just manually set the rotation of the cinemachine camera to all zeros.

![Screenshot](Screenshots/cinemachine_head_cam.png)<br>

Open up the script `CharacterLookControllerTutorial.cs`. This script will be attached to the head of our character and will rotate it based on mouse inputs. This script contains a method that will be executed during our camera's movement logic called `PostPipelineStageCallback()`.

First, let's set up a reference to the InputAction that tells us about the rotation inputs derived from the user's mouse. We will use this reference to get information about how much the user's mouse position has changed in this current frame and determine how fast the player should rotate based on these mouse movements.

We can obtain these mouse deltas from the InputAction with the ReadValue<T>() method. From here, we want to use these deltas to rotate the player's head accordingly. The tricky part here is that a change in the mouse's x position across the screen will result in a rotation about the player's y-axis and the change in the mouse's y position across the screen results in a rotation about the player's x-axis. For this reason, the y-angle rotation of the player's head should be based on a change in the x-position of the mouse, while the x-angle rotation should be based on a change in the y-position of the mouse. 

The fields `horizontalSpeed` and `verticalSpeed` are in terms of degrees per pixel (moving one pixel results in one degree of rotation), and the mouse deltas are in terms of pixels. Using this information, determine how many degrees to rotate about the player's x and y axes for this frame.

After calculating our desired player rotations, we want to make sure our rotations have not gone too far so as to put the player's head in awkward or unrealistic angles. For this we use the `Mathf.Clamp()` method, which takes in a value and a range (defined by a min/max pair) and "shoves" that value back within the range if it is outside (returning what that "shoved in" value would be).

The last step is to tell the Cinemachine camera to rotate to the rotation we have specified. The camera rotation can be set by setting the value of `state.RawOrientation` to a quaternion that represents our rotation. Note that we will need to convert our rotation with the `Quaternion.Euler()` method, which converts Euler angles to quaternion.

Now that we're finished with the script, return to the Unity editor and add this component to the Cinemachine camera!

## Notes on Reading User Input
In previous tutorials you learned that Unity's new input system can notify scripts when new inputs have been received by invoking special functions you implement in those scripts that have names corresponding to your defined Actions. In those special functions, scripts can process the new input data however it needs (such as moving the character forward or jumping). 

In this tutorial, we will be using a slightly different approach that makes use of C# events. The base concept is the same -- implement a special function in your script that will process incoming inputs from the input system. The main difference is that instead of letting Unity wire up these special functions to be called by the input system, we will be the ones to do it this time.

Well then why would I want to wire it up myself when Unity can do it for me?

As with all things, doing things yourself gives you more flexibility since you are in control of the implementation. One strength of C# events is that they can be heard by GameObjects that are not directly connected to the PlayerInput object.

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
 
