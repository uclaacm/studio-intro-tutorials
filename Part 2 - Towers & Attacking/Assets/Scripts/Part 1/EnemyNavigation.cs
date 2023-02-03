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
            spawner = GameObject.FindObjectOfType<EnemySpawner>();  // Find the EnemySpawner
            checkpoints = spawner.checkpoints;  // Reference the list of checkpoints in EnemySpawner
            // Debug.Log(checkpoints.Count);
            curTarg = checkpoints[curCheckpoint];  // Set our first checkpoint to the first checkpoint on the list
        } catch
        {
            Debug.LogError("Could not find an object of type EnemySpawner, or checkpoints of EnemySpawner is empty!");
            curTarg = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* 
         * We use MoveTowards to move a set distance towards our current target. We pass in the starting position, 
         * our desired target position, and the max distance we can travel in one step, and the function will return
         * a position vector representing the position we would be at after moving towards our target.
         */
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(curTarg.position.x, curTarg.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, curTarg.position) < 0.1f)  // If we are close enough to the checkpoint
        {
            SwitchCheckpoints();
        }
    }

    void SwitchCheckpoints()
    {
        curCheckpoint++;  // Move on to the next checkpoint
        if (curCheckpoint >= checkpoints.Count)  // curCheckpoint no longer in range of our checkpoint list
        {
            // The enemy has reached its destination
            Debug.Log("Enemy escaped!");
            // Here, we will implement health loss in the future
            Destroy(gameObject);
        } else  // We still have more checkpoints to reach
        {
            curTarg = checkpoints[curCheckpoint];
        }
    }

    private void OnDestroy()
    {
        // Right before this enemy is destroyed, call the spawner to decrement the number of enemies alive.
        spawner.EnemyDestroyed();
    }
}
