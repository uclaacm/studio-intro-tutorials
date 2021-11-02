using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;	// Used for Tilemap component

[RequireComponent(typeof(Tilemap))]

public class DestructibleTilemap : MonoBehaviour
{

	private Tilemap tilemap;										// Tilemap attached to this gameObject
	private HashSet<Vector3> beingDeleted = new HashSet<Vector3>();	// Set of tiles being deleted
	private const float offset = 0.01f;								// Small offset to be used in contact calculations

	[Range(0,10)]
	[SerializeField] private float deletionDelay = 0.5f;			// How long to wait before deleting a tile

	void Awake()
	{
		tilemap = GetComponent<Tilemap>();	// Initialize value of tilemap at start of scene
	}

	// Trigger tile deletion when entering the collider
    void OnCollisionEnter2D(Collision2D collision)
    {
    	TriggerDeletions(collision);
    }

    // Trigger tile deletion when staying in collider, since CompositeCollider2D combines colliders of tiles
    void OnCollisionStay2D(Collision2D collision)
    {
    	TriggerDeletions(collision);
    }

    // Find position of tiles being collided with and trigger their deletion
    private void TriggerDeletions(Collision2D collision)
    {
    	Vector3 tilePosition = Vector3.zero;

    	// Iterate over all points where the collider is touching something
    	foreach (ContactPoint2D contact in collision.contacts)
    	{
    		tilePosition.x = contact.point.x + offset * contact.normal.x;	// Add small offset to move to correct tile
    		tilePosition.y = contact.point.y + offset * contact.normal.y;
    		StartCoroutine(DeleteTileDelayed(tilePosition));				// Start deletion of tile
    	}
    }

    private IEnumerator DeleteTileDelayed(Vector3 tilePosition)
    {
    	// Check if tile is already being deleted
    	if (!beingDeleted.Contains(tilePosition))
    	{

    		// Add tile to set of tiles being deleted
    		beingDeleted.Add(tilePosition);

    		// Only need to wait if delay is non-negative
    		if (deletionDelay > 0)
    		{
	    		yield return new WaitForSeconds(deletionDelay);
	    	}

	    	// Actually delete tile after delay
	    	tilemap.SetTile(tilemap.WorldToCell(tilePosition), null);
	    	// Remove tile from set of tiles being deleted
	    	beingDeleted.Remove(tilePosition);
	    }
    }
}
