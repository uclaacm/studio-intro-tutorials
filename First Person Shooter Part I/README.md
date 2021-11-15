# Studio Beginner Tutorials - FPS Shooter Part 1
  
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
Create an empty GameObject. Add a Rigidbody and Capsule Collider. `Idk why Aaron added the Capsule Collider`. Make sure that the Rigidbody has 'Use Gravity' checked and has mass so that the character is effected normally by gravity.

To get our player movement, we attach a `PlayerInput` component with the default action map and a [`CharacterController` script](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/3d-fps-part-i/First%20Person%20Shooter%20Part%20I/Assets/Scripts/CharacterMovement.cs) which is similar to the Roll a Ball and 2D Platformer movement scripts but with some unique attributes.

Within our CharacterController script, we start by defining a few SerializeField variables: headCamera and maxSpeed. These variables will help with attaching the camera to the head of the player and making our character's speed editable from the inspector. Then a few variables are initialized for future use. The `Awake()` function is called once before the game starts and is often used to set up references to other scripts and GameObjects, which is exactly what we're doing in our `Awake()` funciton. Specifically, we are retrieving references to the InputAction from the keyboard, animator, and additionally initializing the cursor to being locked and invisible.

`Gonna write more tomorrow (11/15)`

### Animation
Lorem ipsum deez
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
 
 
 
