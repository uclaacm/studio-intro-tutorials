using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] public List<Transform> checkpoints;
    [SerializeField] GameObject enemy;
    [SerializeField] List<int> waveInfo;
    [SerializeField] int activeEnemyCount = 0;

    int curWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (waveInfo.Count < 1)
        {
            Debug.LogError("At least one wave must be specified!");
        } else
        {
            StartCoroutine(SpawnWave());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeEnemyCount == 0)
        {
            curWave++;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        if (curWave < waveInfo.Count)
        {
            for (int i = 0; i < waveInfo[curWave]; i++)
            {
                Instantiate(enemy, spawnPosition.position, Quaternion.identity);
                activeEnemyCount++;
                yield return new WaitForSeconds(1f);
            }
        } 
    }

    public void EnemyDestroyed()
    {
        activeEnemyCount--;
    }
}
