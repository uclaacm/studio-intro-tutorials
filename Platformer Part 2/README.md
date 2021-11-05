# Studio Beginner Tutorials - 2D Platformer Part 2 
  
**Date**: November 9th, 2021, 7:00 pm - 9:00 pm<br>
**Location**: Faraday Room 67-124 (Engineering IV)<br>
**Instructors**: Connor, Peter, Richard
 
## Resources
Slides<br>
Video
 
## Topics Covered
* Destructible tiles
* Cinemachine
* Enemies and combat
 
## What you'll need
* [Unity Hub](https://unity.com/download)
* [Unity 2020.3.15f2](https://unity3d.com/unity/qa/lts-releases)
* [Git](https://git-scm.com/downloads)
* [Kings and Pigs Assets](https://pixelfrog-assets.itch.io/kings-and-pigs)

---

## Destructible Tilemaps

### Setup
A common mechanic in platformers are destructible walls and platforms. To add this into our platformer, we will write a script which will delete tiles from our `Tilemap` when something collides with the tile. But first, create a new `2D Object → Tilemap → Rectangular` as a child of the `Grid` game object that already holds our existing tilemaps. We need a separate `Tilemap` since we don't want to delete tiles from platforms that aren't supposed to be destructible. Remember to attach `Tilemap Collider 2D` and `Composite Collider 2D` components, and set the `Rigidbody2D` to static so that other things will be able to collide with our new `Tilemap`! Finally, paint in some tiles into the new tileamp in easily accessible locations so you can test destroying them later.

### Deleting Tiles
Create a new [`DestructibleTilemap` script](https://github.com/uclaacm/studio-beginner-tutorials-f21/blob/main/Platformer%20Part%20I/Assets/Scripts/DestructibleTilemap.cs) and attach it to your new `Tilemap`. Add a member variable referencing the `Tilemap` that's attached to the same game object as our script. You'll also need to add `using UnityEngine.Tilemaps;` at the top of your script. To delete tiles from the tilemap, we can use `tilemap.SetTile(Vector3Int position, null)` since setting a tile to null removes it from the tilemap. The function below iterates over each point where the `Composite Collider 2D` touches another collider, and deletes the tile at the point of contact. Note that we add a small offset (`0.01f`) in the direction of the vector normal to the surface at the contact point so that we delete the correct tile, since the contact points are all on the edge of our tiles.

```c#
// Find position of tiles being collided with and trigger their deletion
private void TriggerDeletions(Collision2D collision)
{
	// Iterate over all points where the collider is touching something
	Vector3 tilePosition = Vector3.zero;
	foreach (ContactPoint2D contact in collision.contacts)
	{
		// Add small offset to delete correct tile
		tilePosition.x = contact.point.x + offset * contact.normal.x;
		tilePosition.y = contact.point.y + offset * contact.normal.y;
    
		// Delete the tile from our tilemap
		tilemap.SetTile(tilemap.WorldToCell(tilePosition), null);
	}
}
```

Call this function in `void OnCollisionStay2D(Collision2D collision)`. If you play your platformer, you should see that when you run into or onto a destructible tile, it gets deleted instantly.

---
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
 
 
