using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    /// 
    /// This script controls the behaviour for collectibles, which we would save as a prefab. Because of this, we can't use
    /// SerializeFields to pass in references since we want to be able to quickly duplicate a lot of these (or even instantiate
    /// them dynamically with scripts!). As such, we will reference other GameObjects through code
    /// 
    GameManager gm;

    void Start()
    {
        // Searches your scene hierarchy for a GameManager component, returning a reference of the first that it finds.
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Adds score for collecting the object
        gm.AddScore(5f);

        /// 
        /// The following we will optionally cover in the tutorial. We want the player to jump after picking up the object.
        /// To do this, first make sure that Jump() under PlayerController is made a public function (so this script can actually call it).
        /// We will grab a reference to PlayerController through the collider (we don't need to check of the colliding object
        /// is the player since only the player can move to collide with this object). To do so, we first reference the GameObject the 
        /// collider is attached to, then reference the PlayerController attached to the GameObject, and then call Jump().
        ///

        // OPTIONAL: make the player jump again from picking up the object
        collision.gameObject.GetComponent<PlayerController>().Jump();

        // Delete the GameObject that this script is attached to
        Destroy(gameObject);
    }
}
