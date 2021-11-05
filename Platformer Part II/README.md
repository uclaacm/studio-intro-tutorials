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

Call this function within `void OnCollisionStay2D(Collision2D collision)` so that we delete tiles when the player collides with them. If you play the game, you should see that when the player runs into or onto a destructible tile, the tile is instantly deleted. While you can now create hidden rooms and fake walls, deleting tiles instantly isn't so great if we want a platform that will collapse after we run over it. To implement a delay between detecting the collisions and actually deleting the tiles, we can use coroutines.

### Coroutines
Couroutines are a special type of function in Unity which allow you to pause execution of your code and then return to it later. In our case, we can start the process of deleting a tile, pause our coroutine, and then actually delete the tile after the delay.

Coroutines always have an `IEnumerator` return type, and within your coroutine you will need to use `yield return` instead of just `return`. Unity provides some very useful `YieldInstructions` that can be used with `yield return`, such as `new WaitForSeconds(float seconds)` which will pause the coroutine for approximately the provided number of seconds. You can also use `yield return null` to only pause your coroutine until the next frame of the game. Finally, to actually run a coroutine, instead of calling it like a regular function (`ExampleCoroutine(inputParameter);`), coroutines are started with `StartCoroutine()` (`StartCoroutine(ExampleCoroutine(inputParameter));`). If you would like to learn more about coroutines and how to use them, you can take a look at the [Programming Essential workshop](https://github.com/uclaacm/studio-advanced-tutorials-f21/tree/main/Programming%20Essentials) from the Advanced Track.

```c#
[SerializeField] private float deletionDelay = 0.5f;	// How long to wait before deleting a tile

private IEnumerator DeleteTileDelayed(Vector3 tilePosition)
{
	// Only need to wait if delay is non-negative
	if (deletionDelay > 0)
	{
		yield return new WaitForSeconds(deletionDelay);
	}

	// Actually delete tile after delay
	tilemap.SetTile(tilemap.WorldToCell(tilePosition), null);
}
```

The function above shows an implementation of waiting a number of seconds before deleting tiles. If you call `DeleteTileDelayed()` from `TriggerDeletions()` and play the game, you should now see that a tile that you walk into or onto will only delete itself after a short delay. However, if you have multiple destructible tiles next to each other and walk over them, you may notice that only the first tile you touch is deleted. This is because we called `TriggerDeletions()` from `OnCollisionEnter2D` — when we move from one tile to the next we don't enter the collider again, since we no longer exit the collider when the tile is instantaneously deleted. To fix this, also call `TriggerDeletions()` from `OnCollisionStay2D`. Now you should be able to run over a destructible platform and have it collapse behind you!

### Bonus Sidequests
One disadvantage of calling `TriggerDeletions()` from `OnCollisionStay2D` is that our player will trigger the deletion of the same tile many times before the tile is actually deleted, slowing down our game. This problem is worsened by longer delays before deletion, and could cause problems down the line if we need to add back a tile in the same position, since it could be deleted by the deletions that have been "buffered". You can solve this by checking whether the tile has already been marked for destruction in your coroutine - the example implementation uses `HashSet<>` to keep track of which tiles are marked for deletion.

You can also extend your newfound knowledge to create moving platforms! By moving a tilemap around, you can move all of the platforms within the tilemap. You can use coroutines or the animation state machine to create platforms that move in a specific pattern.

Finally, our tilemap currently deletes tiles whenever anything collides with it - not just the player. While this can be a good thing (such as allowing enemies to destroy platforms by running over them!), you may also want to have platforms that are only destroyed by certain objects or types of objects. For example, you could modify your DestructibleTilemap script to only delete tiles when the tile touches a bomb!

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
## Essential Links
- [Studio Discord](https://discord.com/invite/bBk2Mcw)
- [Linktree](https://linktr.ee/acmstudio)
- [ACM Membership Portal](https://members.uclaacm.com/)
## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [ACM Website](https://www.uclaacm.com/)
- [ACM Discord](https://discord.com/invite/eWmzKsY)
 
 
 
