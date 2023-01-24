using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyNavigation : MonoBehaviour
{
    List<Transform> checkpoints;
    [SerializeField] float moveSpeed = 5f;
    int curCheckpoint = 0;
    Transform curTarg;
    EnemySpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            spawner = GameObject.FindObjectOfType<EnemySpawner>();
            checkpoints = spawner.checkpoints;
            Debug.Log(checkpoints.Count);
            curTarg = checkpoints[curCheckpoint];
        } catch
        {
            Debug.LogError("Could not find an object of type EnemySpawner, or checkpoints of EnemySpawner is empty!");
            curTarg = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(curTarg.position.x, curTarg.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, curTarg.position) < 0.2f)
        {
            SwitchCheckpoints();
        }
    }

    void SwitchCheckpoints()
    {
        curCheckpoint++;
        if (curCheckpoint >= checkpoints.Count)
        {
            // The enemy has reached its destination
            Debug.Log("Enemy escaped!");

            Destroy(gameObject);
        } else
        {
            curTarg = checkpoints[curCheckpoint];
        }
    }

    private void OnDestroy()
    {
        spawner.EnemyDestroyed();
    }
}
