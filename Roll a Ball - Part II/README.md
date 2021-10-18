# Studio Beginner Tutorials - Roll a Ball Part 1
 
**Date**: October 19, 2021, 7:00 pm - 9:00 pm<br>
**Location**: Faraday Room 67-124 (Engineering IV)<br>
**Instructors**: Richard Cheng, Peter Sutarjo, Ryan Vuong
 
## Resources
[Slides](https://docs.google.com/presentation/d/1N__34gQRdCBV8gSB7huCgWJGKWnnMpdQAGUF3QjkW_k/edit?usp=sharing)<br>
[Video](https://youtu.be/oB3sk4a3VkE)<br>
 
## Topics Covered
* UI Elements
* Adding items for player to pick up
* Switching scenes
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
---
## User interface
![Screenshot](Screenshots/image1.png)<br>

### Create a canvas
For this tutorial, we'll be making a simple UI which will display a score and a timer that counts down as shown above. To get started, create a `Canvas` by clicking `GameObject->UI->Canvas`. A canvas should contain all your UI elements in a scene. Looking at the inspector, you may notice that a canvas is in the UI layer, separate from a 2D/3D scene. Every canvas has the following components:
* Canvas: Allows you to change certain settings of a canvas, such as the `Sort Order` which determines which canvas appears on top.
* Canvas Scaler: Used for controlling the overall scale and pixel density of UI elements in the Canvas. Contains settings that allow UI elements on a canvas to change size depending on screen size and resolution. 
* Graphic Raycaster: Determines if an element on a canvas has been hit (e.g. a button).

## Implementing score
### Score controller
To keep track of score and update the text in the UI, we can create a script that will have a reference to the score text and a public static variable which contain the value of the player's current score. As
```csharp
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("Score: {0}", score); 
    }
}
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
 
 
