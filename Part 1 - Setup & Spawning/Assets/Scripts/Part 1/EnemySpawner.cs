using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] public List<Transform> checkpoints;
    [SerializeField] GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, spawnPosition.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
