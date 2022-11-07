# Studio Intro Tutorials - 2D Platformer Part 2
 
**Date**: November 7, 2022, 7:00 pm - 9:00 pm<br>
**Location**: Boelter 2760 <br>
**Instructors**: Ryan
 
## Resources
[Slides]()<br>
[Video (From last year)](https://youtu.be/sxFkzWTz08E) <br>
[Skeleton Package](https://github.com/uclaacm/studio-intro-tutorials/tree/fall-22/Platformer%20-%20Part%20II/platformer-pt1.unitypackage)

## Topics Covered
* Camera Movement
* Sound Effects and Music
* Enemy Movement and Combat
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2021.3.11f1 (Any Unity 2021.3 version should work)](https://unity3d.com/unity/qa/lts-releases)
* [Kings and Pigs Assets](https://pixelfrog-assets.itch.io/kings-and-pigs)
 
---

### Initial Setup
Don't worry if you missed part 1, we have a Unity package that you can install above that will catch you up to where we left off last time. To import this package, download it from our Github Repository -> create a new 2D project in Unity -> go to Assets -> Import Package -> Custom Package and select our package. Please also install the Cinemachine package in your project in Window -> Package Manager -> Unity Registry -> Install Cinemachine.

## Cinemachine
 
### Setup
Currently, we have the `Main Camera` as a child of the `Player`, which will cause the camera to always have the player be at the center of the screen. One issue is this camera setup is that it can be difficult for the player to see what's in front of them. To solve this issue, we can make the camera to be more dynamic and configurable such that it reacts to where the player is on the screen and movement.
 
`Cinemachine` is a state-driven suite of camera modules that allows us to easily configure animation states, implement tracking, dolly, shake, and more. To setup `Cinemachine`, first install the package by navigating to `Window->Package Manager` and install the `Cinemachine` package in the `Unity Registry` section.
 
### Implementing the camera
After installing `Cinemachine`, a new tab should appear to right of `Component` in the upper taskbar. Click `2D Virtual Camera` to add a `CM virtual camera` to our scene. This will be a camera with some settings configured for a 2D scene. Specifically, the `Framing Transposer` under the `Body` settings window will follow a target on the camera's X-Y plane, and prevent rotation.
 
We'll set the `Follow` setting to the `Player` transform. You should see a grid appear in the game view with a yellow dot on the target we're tracking.
![Screenshot](Screenshots/image2.png)<br>
 
### Settings Overview
**Lookahead Time**:  The composer will adjust its target offset to look at a point where the target may be in x seconds into the future. This setting is useful for tracking the target if it's moving fast towards the edge of the frame.<br>
 
**X, Y, Z Damping**: Determines how aggressively the camera maintains the offset on a specified axis. A low damping value means the camera snap faster to its target offset while a larger damping value means the camera will respond slower to the target offset, yielding in smoother movement.<br>
 
**Screen X, Y:** Moves the camera such that the target is positioned to the corresponding X,Y screen coordinates. Note that screen coordinates of (0,0) correspond to the bottom left of the screen while the coordinates of (1,1) correspond to the bottom right of the scren. <br>
 
**Dead Zone Width/Height/Depth**: Increasing this value will cause the blue lines to expand either horizontally or vertically. Any movement of the target within this area will not cause the camera move.<br>
 
**Soft Zone Width/Height**<br>
If the target enters the soft zone, the camera will reorient itself to frame the target in the dead zone. Note that damping values affect how quickly this readjustment occurs.
 
## Audio

### Audio Components
In Unity, the most basic audio components are an `AudioSource` component that plays sounds and an `AudioListener` component, which comes attached to the camera in each scene by default. The `AudioListener` acts like a microphone in 3D space in that it will hear sounds differently based on its position to the `AudioSource` components. For our simple 2D platformer, we don’t care quite as much about this, since we will be setting all of our `AudioSource` components to use 2D instead of 3D in the `Spatial Blend`.

Although you could set up an audio system using just `AudioSource` components, you should use the `AudioMixer`, even if only for making it easy to implement volume settings. The `AudioMixer` allows you to mix different sounds and apply different effects to those sounds depending on which `AudioMixerGroup` the sounds are routed through. For our game, we will be using two groups, one for music and one for sound effects.

To create an `AudioMixer`, go to the project folders (by default at the bottom of Unity’s layout) and click `Create → AudioMixer`. This will create an `AudioMixer` asset that is shared by all of the scenes (the `AudioMixer` is not a game object that lives as part of a scene, but a separate entity that “lives” outside of your scenes.) Double-clicking the `AudioMixer` will open up a window - create a new music group and a sound effects group as children of the master group here.

### Background Music

For our background music, we will create a new empty game object as a child of the Camera game object so that the background music will always play from the location of the camera (which is also where the `AudioListener` is located). Create a new `AudioSource` component on this empty game object, and select one of the [music loops](https://github.com/uclaacm/studio-beginner-tutorials-f21/tree/main/Platformer%20Part%20I/Assets/Audio/Music) as its `AudioClip`. Make sure to set the Music `AudioMixerGroup` for `Output` and make sure the `Play on Awake` and `Loop` boxes are checked. If you play the scene, the background music will now play forever! That was easy.

### Sound Effects

Unfortunately, sound effects are a lot more work to implement. To demonstrate this, let’s add some footstep sounds that play whenever the player is running. A good way to add footstep sounds is to have collection of different footsteps so that you can play one randomly whenever the player takes a step - however I don't have good audio editing software or a good set of footstep sounds so we'll just have to make do with [this sound clip](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Audio/Sound%20Effects/Short%20Footsteps.mp3) I cut to be slightly longer than the animation. We can create a new script with a function that will play this sound whenever the player moves:

```c#   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PlayFootstep : MonoBehaviour
{

	[SerializeField] private AudioClip footsteps;
	private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (footsteps == null)
            Debug.LogWarning("PlayFootstep script not provided with AudioClip.");
    }

    void PlayFootsteps()
    {
        source.PlayOneShot(footsteps);
    }

    void StopPlaying()
    {
        source.Stop();
    }
}
```

Attach this script to the player along with an `AudioSource` that outputs to the Sound Effects `AudioMixerGroup`, than select the Running animation clip in the `Animation` window. Add a new `AnimationEvent` at the start of the clip, and set it to trigger `PlayFootsteps()`. In the Idle and Jumping animation clip, do the same thing except set it to trigger `StopPlaying()` instead. This will allow the animations to play the footstep sounds each cycle the player is running, while also stopping the sound immediately when the player jumps or stops moving.

## Enemies and Combat
 
### Setup
Similar to the player, we'll be setting up the sprite sheet for the enemy which will be a pig. Navigate to the folder called `Kings and Pigs->Sprites-> 03-Pig`. We'll be configuring spritesheets for the running, idle, and death animation. Click on one of the sprites and set the `Sprite Mode` to `Multiple`. Next, click `Sprite Editor` in the inspector and slice the sprites according the cell size (34 x 28). We won't be using the automatic setting since sprite won't always be centered across different animations.
 
### Animator Controller
![ScreenShot](Screenshots/image1.png) <br>
Add an animation controller with the following animation states and parameter. If you need a refresher on how to configure animations in Unity, you can refer to [Part I](https://github.com/uclaacm/studio-beginner-tutorials-f21/tree/main/Platformer%20Part%20I) of this tutorial series!
 
### Enemy Movement
Finally, we can configure a movement script for the enemy, which will cause it to patrol back and forth between two points that we specify. Note that we will also need to manually flip the sprite when it is moving to the right, similar to the player.
```csharp
public class EnemyMovement : MonoBehaviour
{
    enum Direction {left = -1, right = 1};
    private Rigidbody2D rb;
    private Animator animator;
    public float speed;
   
    // Initial orientation of the sprite renderer
    private Vector3 initScale;
 
    // Set boundaries for patrol
    [SerializeField] private Vector3 leftEdge;
    [SerializeField] private Vector3 rightEdge;
 
    // Check if we're moving in a certain direction
    private bool movingLeft;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale;
        movingLeft = true;
    }
 
    // Update is called once per frame
    void Update()
    {  
        if (movingLeft)
        {
            // If we hit the boundary, have the enemy switch direction
            if ( transform.position.x < leftEdge[0])
                movingLeft = false;
            Move(Direction.left);
        }
        else
        {
            if ( transform.position.x > rightEdge[0])
                movingLeft = true;
            Move(Direction.right);
        }
    }
   
    void Move(Direction dir)
    {
        // Set animator boolean for movement
        animator.SetBool("isRunning", true);
 
        switch (dir)
        {
            case Direction.left:
                // Keep initial orientation of sprite is moving left
                transform.localScale = new Vector3(Mathf.Abs(initScale.x), initScale.y, initScale.z);
                break;
            case Direction.right:
                // Otherwise, we flip the sprite
                transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
                break;
        }
 
        // Have enemy move in the specified direction
        rb.velocity = new Vector2(speed * (int) dir, rb.velocity.y);
    }
}
```
### Bonus Sidequests
Instead of having the enemy move back and forth between two points, would it be possible to make the pig move in a random direction for a certain amount of time?
 
Can you implement enemies jumping?
 
What if enemies locked on to the player once they approached a certain distance?
 
---
## Player Combat
 
### Player Taking Damage
Now we want the player to be able to take damage when it comes in contact with the enemy. To do this we are going to modify the `PlayerMovement` script that we made in [Part I](https://github.com/uclaacm/studio-beginner-tutorials-f21/tree/main/Platformer%20Part%20I) of this tutorial series. First we will add some variables that will be used to implement this feature.
 
```csharp
public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector2 position;
 
    private Rigidbody2D rb;
    private Animator animator;
 
    private bool moveLock;
 
    public bool grounded;
    public float speed;
    public float jumpHeight;
 
    public int health;
    public float knockbackX;
    public float knockbackY;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
 
        position = rb.position;
        grounded = false;
 
 
        moveLock = false;
    }
```
We add `movelock` to lock the players movement when it is knocked back from the enemy, `knockbackX` and `knockbackY` to control how much the player gets knocked back, and `health` to make the player take damage.
 
Now we want the player to take damage and be knocked back if it collides with the enemy, so we modify the `OnCollisionEnter2D` function.
```csharp
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Ground")
    {
        grounded = true;
        animator.SetBool("isJumping", false);
    }
    if (collision.gameObject.tag == "Enemy")
    {
        health--;
        int dir = collision.gameObject.GetComponent<Transform>().position.x > rb.position.x ? -1 : 1;
        horizontal = 0;
        vertical = 0;
        moveLock = true;
        rb.velocity = new Vector2(knockbackX*dir, knockbackY);
        animator.SetBool("isJumping", false);
        animator.SetBool("hit", true);
    }
}
```
We need to make sure that we add the `Enemy` tag to the enemy for this to work. Don't worry about the animator booleans, we will take care of those in a bit. We set the `movelock` variable on here to make the player get knocked back from the enemy without interference from player input. For this to work we need to now modify the `OnMove` and `FixedUpdate` functions as follows
 
```csharp
void OnMove(InputValue movementVal)
{
    if (!moveLock)
    {  
        horizontal = 0;
        vertical = 0;
        Vector2 movement = movementVal.Get<Vector2>();
        horizontal = movement.x * speed;
        if (movement.y > 0 && grounded)
        {
            vertical = jumpHeight;
        }
    }
}
private void FixedUpdate()
{    
    if (!moveLock)
    {
        rb.velocity = new Vector2(horizontal, vertical <= 0 ? rb.velocity.y : vertical);
    }
    vertical = 0;
}
```
Now there won't be any interference in the knockback due to user input. But we need a way to set `movelock` back after the player has been knocked back. We will add the `endHit` function below which we will call as an animation event.
```csharp
public void endHit()
{
    Debug.Log("end");
    animator.SetBool("hit", false);
    moveLock = false;
}
```
 
### Animation
We will be setting up another sprite sheet and creating an animation for the player taking damage. Navigate to the folder called `Kings and Pigs->Sprites->01-King Human`, select the `Hit` spritesheet and set the `Sprite Mode` to `Multiple` and the `filter mode` to `point (no filter)`. Next, click `Sprite Editor` in the inspector and we will just use the automatic slicing. Use these sprites to make an animation for the player. Add an `Animation Event` to the last frame of the animation that calls the `endHit` function.
 
In the animation controller we now have a `hit` state. Create a `hit` boolean and add a transition from `Any State` to the `hit` state that occurs when the `hit` variable is true. When clicking on the transition, in the `Inspector`  window turn off `Has Exit Time` and expand the `Settings` dropdown to disable `Can Transition To Self`. This will make it so the animation will play out one full time and then transition to the next state instead of constantly restarting as long as the `hit` boolean is `true`. Now we want the player taking damage animation to override the jump animation, so add the requirement that `hit` is `false` to the transition from `Any State` to `Jump`.
 
### Player Attacking
To make the player attack we are again going to be modifying the `PlayerMovement` script. We will add an `OnFire` function to attack when the `Fire` input is detected
```csharp
void OnFire()
{
    if(!animator.GetBool("isJumping") && !animator.GetBool("hit"))
        animator.SetTrigger("attack");
}
```
Now go back to the `Kings and Pigs->Sprites->01-King Human` folder and select the `Attack` spritesheet. set the `Sprite Mode` to `Multiple` and the `filter mode` to `point (no filter)`, then click `Sprite Editor` in the inspector and slice the sprites according the cell size (78 x 58). Create a new animation for the player and drag these sprites to add them to the animation. To make it so we can detect if the player's attack hits we can add a hitbox. For this we will use a `CircleCollider2D`. Add this to the player and turn on `IsTrigger`. Uncheck the box at the top of the component in the `Inspector` window to set it inactive. We only want this collider to be active when the player is attacking; we can do this in the `animation` window. Select the player and select the `Attack` animation in the animation window. Click the recording button. Now we can make changes to the components of the player and these changes will be added to the animation. When on the first frame of the animation, turn on the `CircleCollider2D` and edit the collider to cover the area that you want to be included in the attack. Adjust the size of the collider at the different frames of the animation to fit where you want the attack range to be and then in the last frame deactivate the collider. Now to make the animation activatable, add a trigger called `attack` in the animation controller for the player and make a transition from `Any State` to the `attack` state. Make sure to set it to set it to have no exit time. Now the player can attack.
 
### Bonus Sidequests
Add a death animation for the player so that something happens when the player reaches zero health.
 
Edit the `PlayerMovement` or `EnemyMovement` scripts to make it so that the enemy takes damage from the player's attack.
 
---
 
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
 

