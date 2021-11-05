# Studio Beginner Tutorials - 2D Platformer Part 2 
  
**Date**: November 9th, 2021, 7:00 pm - 9:00 pm<br>
**Location**: Faraday Room 67-124 (Engineering IV)<br>
**Instructors**: Connor, Peter, Richard
 
## Resources
Slides<br>
Video
 
## Topics Covered
* Tilemaps and platforms
* Basic 2D animation with spritesheets
* Basic audio management
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [Kings and Pigs Assets](https://pixelfrog-assets.itch.io/kings-and-pigs)

---

## Destructible Tilemaps
### Setup
A common mechanic in platformers are destructible walls and platforms. To add this into our platformer, we will write a script which will delete tiles from our `Tilemap` when something collides with the tile. But first, create a new `2D Object → Tilemap → Rectangular` as a child of the `Grid` game object that already holds our existing tilemaps. We need a separate `Tilemap` since we don't want to delete tiles from platforms that aren't supposed to be destructible. Remember to attach `Tilemap Collider 2D` and `Composite Collider 2D` components, and set the `Rigidbody2D` to static so that other things will be able to collide with our new `Tilemap`!

---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
 
 
