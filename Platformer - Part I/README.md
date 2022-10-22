# Studio Beginner Tutorials - 2D Platformer Part 1 
  
**Date**: October 24, 2022, 7:00 pm - 9:00 pm<br>
**Location**: Boelter 2760 <br>
**Instructors**: Ryan, Matthew
 
## Resources
[Slides](https://docs.google.com/presentation/d/1pfJ3OuMaTnBttJlBjijEGwMq7aT6koFEN7K3zYEDpoI/edit?usp=sharing)<br>
[Video (From last year)](https://youtu.be/sxFkzWTz08E)
 
## Topics Covered
* Tilemaps
* 2D Player Movement
* 2D Animation
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2021.3.11f1 (Any Unity 2021.3 version should work)](https://unity3d.com/unity/qa/lts-releases)
* [Kings and Pigs Assets](https://pixelfrog-assets.itch.io/kings-and-pigs)

---

## Creating Platforms
When you download and open the assets in unity, (bottom right corner, project ->assets) you will be able to see the things that you can drag into the empty workspace. We can try dragging in images one block at a time. Try rotating and stretching the images. Look for the background asset and drop it in. then we can talk about the background layers. 
 
### Tilemaps
To expedite the process of making platforms, we can use a tilemap to quickly and easily paint platforms into the scene. First, find the [image](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Kings%20and%20Pigs/Sprites/14-TileSets/Terrain%20(32x32).png) containing the tiles representing parts of platforms. We need to slice this single image into multiple tiles by changing the `Sprite Mode` to `Multiple` in the Inspector. While you’re doing this, also find the `Filter mode` near the bottom and change it to `Point (no filter)` since we want the pixel art to be sharp and crisp. Don’t forget to click `Apply` to actually save these changes!

Next, click the `Sprite Editor` button, and go to the `Slice` tab. Since our tiles are all 32 by 32 pixels, we can slice using “Grid by Cell Size”. Check that our tiles have been sliced correctly, then click apply to save these changes. Next, go to `Window → 2D → Tile Palette`, and create a new Tile Palette. Drag in the tiles you just sliced into this new Tile Palette.

To use this palette, create a `2D Object → Tilemap → Rectangular` in the Hierarchy. This will create a `Tilemap` as a child of a `Grid` game object. Since our tiles are 32 by 32 pixels, we will need to change the size of the cells in the `Grid` game object to `0.3125` (which is 10/32. Pro Tip: Unity can do basic arithmetic whenever you enter a number, so you don’t have to calculate and type in the decimal yourself).

We’re finally ready to start painting platforms! Select a tile in your tile palette and the paintbrush tool at the top of the Tile Palette window, then go to the scene view. You should be able to paint tiles into the scene by clicking on the squares! Now paint to your heart’s content.
 
### Adding Colliders
Right now, all of the platforms you’ve painted into the scene are just background decoration - we need to add colliders to them so that the player will be able to stand on them. Fortunately Unity has a built in component called the `Tilemap Collider 2D` which we can use to do just that! (Note: Since this is a 2D game, we will be using the collider components explicitly marked as 2D - any collider that isn’t named 2D is a 3D component). After adding this component, take a look in the scene view - notice how each tile in each platform has its own collider? That’s obviously unnecessary since the player won’t ever be within a platform, so we also need to add a `Composite Collider 2D` component which will conglomerate the separate colliders into one. After attaching the `Composite Collider 2D`, also check the `Used by composite` box in the `Tilemap Collider 2D` and set the automatically generated `Rigidbody` to be `Static` so that our platforms aren’t affected by gravity.

Note: If you want to paint background tiles into the scene but have them not block your player with colliders, you can create a second `Tilemap` object and paint tiles into that.

---
 
## Player
### Setup + Movement
To create a player, drag any of the player sprites into the scene. (We don't care which one because it will soon be replaced when we animate it). On the `Sprite Renderer`, be sure to increase the value of `Additional Settings → Order in Layer` so that the player will render on top of other sprites in your scene! Attach a `Rigidbody 2D` and check the `Contraints → Freeze Rotation` box so that our player can't rotate (unless you want your player to be able to run upside-down!) We also need a collider for our player - a simple `Box Collider 2D` should suffice. You may want to edit the collider to exclude the hammer so that our player can fall down one-tile wide holes, since the hammer makes the sprite too wide otherwise.

If you play the scene and notice that the player seems to hover slightly above the platform, you may need to adjust Unity's physics settings to detect collisions at a smaller scale, since our sprites are so small. To do so, go to `Edit → Project Settings → Physics → Default Contact Offset` and change the value from the default to some smaller number, such as `0.01`.

To get our player movement, we attach a `PlayerInput` component with the default action map and a [`PlayerMovement` script](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Scripts/PlayerMovement.cs) very similar to the one used for Roll a Ball, except in 2D of course. I would recommend coding a movement script from scratch for practice if you have the time!

### Animation
Now we want to animate our player so that it doesn’t look like the character is just sliding across the screen. To do this, we need to first prepare the spritesheets containing the images for the animations. Find the [idle animation spritesheet](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Kings%20and%20Pigs/Sprites/01-King%20Human/Idle%20(78x58).png) and slice it, much as we did with the spritesheets for the tilemaps. If you allow Unity to automatically slice the sprites, you will notice that the sprites themselves are about 37 wide by 28 pixels tall, but some sprites are slightly smaller. Since we want all of the sprites to be the same size, we can slice by cell size, with a padding based on the 78 pixel by 58 pixel size box containing each sprite. If you do so, an offset of about 9 pixels on the x axis and 16 pixels on the y axis should position the box in the correct place for each sprite in the sheet. Don’t forget to click `Apply` to save the sliced sprites! Repeat these steps for the [running animation](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Kings%20and%20Pigs/Sprites/01-King%20Human/Run%20(78x58).png) and the [jumping animation](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Kings%20and%20Pigs/Sprites/01-King%20Human/Jump%20(78x58).png).

The next step is to add an `Animator` component to the player game object and opening the Animation window via `Window → Animation → Animation`. With the player selected in the hierarchy, create a new animation clip. Then in the animation window, click `Add Property` and find `SpriteRenderer → Sprite`. Delete the existing keys in the right hand side of the animation window, then drag in the idle sprites you sliced earlier into the animation window. You can drag them out to lengthen the animation - having the whole animation take about a second is a good length. Now if you click play, you should see the player doing the idle animation. Now repeat these steps to create the running animation instead.

To transition between the idle and running animations, we need to go to the Animator window. If you created the Idle animation, you should see that there is an arrow from the Entry box to the Idle box. Click the parameters and create a new bool called `isRunning`. Right click the idle box and select `Create Transition`, then click on the running box to draw an arrow from the idle box to the running box. You can then click on the arrow to select it - add a condition to the transition so that we transition from idle to running when `isRunning` is `true`. Similarly, create a transition back from the running state to the idle state when `isRunning` is `false`. For these transitions, make sure to uncheck the `Has Exit Time` box to make them instantaneous.

To create the jumping animation, we repeat these steps by creating a jumping clip, an `isJumping` bool, and creating transitions. For the transitions, since we can start jumping from the idle or the running state, you can save time by creating a single transition from `Any State` to `Jumping` instead of separate transitions from `Running` and `Idle`.

Now in our `PlayerMovement` script, we need to set the value of `isRunning` when the player starts and stops moving by calling `Animator.SetBool()` with values based on whether we are moving or not. For setting the value of `isJumping`, we can also use `Animator.SetBool()` in our `OnCollision` functions when we detect whether the player is grounded.

--- 

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

---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
 
 
