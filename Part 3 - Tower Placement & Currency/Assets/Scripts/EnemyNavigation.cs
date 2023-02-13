using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    List<Transform> checkpoints;
    int curCheckpoint = 0;
    Transform curTarget;

    [SerializeField] float moveSpeed = 5f;
    EnemySpawner spawner;
    

    // Start is called before the first frame update
    void Start()
    {
        try {
            // Reference to enemy spawner
            spawner = GameObject.FindObjectOfType<EnemySpawner>();
            checkpoints = spawner.checkpoints;
            curTarget = checkpoints[curCheckpoint];
        } catch {
            curTarget = transform;  // transform means the current object's transform
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = new Vector3(curTarget.position.x, curTarget.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, curTarget.position) < 0.2f) {
            SwitchCheckpoints();
        }
    }

    void SwitchCheckpoints() {
        curCheckpoint++;
        if (curCheckpoint < checkpoints.Count) {
            curTarget = checkpoints[curCheckpoint];
        } else {
            // Enemy escaped
        Debug.Log("Enemy escaped");
        Destroy(gameObject);  // Do not use "this"
            
        }

    }

    private void OnDestroy() {
        spawner.EnemyDestroyed();
    }
}
