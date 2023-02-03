using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyNavigation : MonoBehaviour
{
    List<Transform> checkpoints;  // This corresponds to a list of checkpoints that the enemy spawner contains
    [SerializeField] float moveSpeed = 5f;  // Movement speed.
    
    int curCheckpoint = 0;  // Current checkpoint index
    Transform curTarg;  // The current position to follow
    EnemySpawner spawner;  // Reference to the enemy spawner
    
    void Start()
    {
        try  // Maybe this is bad practice, but we use this to see if our code below throws an error, and if it does, it won't break stuff
        {
            // TODO: Find the enemy spawner, reference its list of checkpoints, and set our first checkpoint

        } catch
        {
            Debug.LogError("Could not find an object of type EnemySpawner, or checkpoints of EnemySpawner is empty!");
            curTarg = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Move the enemy in the direction of the current target and switch checkpoints if the enemy is close enough to the current one.

    }

    void SwitchCheckpoints()
    {
        // TODO: Move on to the next checkpoint and check if there are more checkpoints

    }

    private void OnDestroy()
    {
        // Right before this enemy is destroyed, call the spawner to decrement the number of enemies alive.
        spawner.EnemyDestroyed();
    }
}
