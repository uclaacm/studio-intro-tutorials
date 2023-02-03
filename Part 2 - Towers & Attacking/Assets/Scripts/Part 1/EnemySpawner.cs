using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;  // The transform that enemies are spawned on
    [SerializeField] public List<Transform> checkpoints;  // A list of checkpoints the enemy will follow
    [SerializeField] GameObject enemy;  // The prefab of the enemy, which we will spawn copies of
    [SerializeField] List<int> waveInfo;  // A list containing the amount of enemies to spawn in each wave
    
    int activeEnemyCount = 0;  // This keeps track of the amount of enemies alive
    int curWave = 0;  // The current wave number (to be used in conjunction with waveInfo)
    bool waveSpawned = false;  // whether or not the current wave is completely spawned (needed to synchronize our Coroutine with the Update loop)

    void Start()
    {
        if (waveInfo.Count < 1)  // Make sure we have at least 1 wave defined
        {
            Debug.LogError("At least one wave must be specified!");
        } else
        {
            StartCoroutine(SpawnWave());  // Start spawning our first wave
        }
    }

    void Update()
    {
        if (activeEnemyCount == 0 && waveSpawned)  // When the current wave has finished spawning and there are no enemies alive
        {
            curWave++;
            StartCoroutine(SpawnWave());
        }
    }

    // This function will be called by the enemy to keep track of the amount of enemies alive
    public void EnemyDestroyed()
    {
        activeEnemyCount--;  // Decrement number of enemies alive
    }

    /*
     * A Coroutine runs concurrently with Unity's update loop, but has some custom control in terms
     * of when a script gets executed. Certain lines of code can be conditionally run, such as under
     * a timer delay (which is what we use here). 
     * 
     * Every Coroutine needs a "yield return" statement followed by a condition. When the program 
     * reaches the yield statement, it will pause execution until the condition you passed in has 
     * been met, and then it continues with whatever lies under the yield return and exits the Coroutine 
     * if there's no more code to run.
     * 
     * The goal of this coroutine is to stagger the spawn of each wave of enemies so one enemy spawns
     * every second.
     */

    IEnumerator SpawnWave()
    {
        if (curWave < waveInfo.Count)  // If our wave number hasn't exceeded the total number of waves we've planned
        {
            waveSpawned = false;  // Indicate that we are spawning a new wave

            for (int i = 0; i < waveInfo[curWave]; i++)
            {
                // Spawn an enemy, at the location of spawnPosition, with default rotation (Quaternion.identity)
                Instantiate(enemy, spawnPosition.position, Quaternion.identity);
                activeEnemyCount++;  // Increment number of enemies alive
                yield return new WaitForSeconds(1f);  // Here, the program pauses the Coroutine for 1 second before resuming.
            }
            waveSpawned = true;  // Indicate we have finished spawning the wave
        } 
    }
}
