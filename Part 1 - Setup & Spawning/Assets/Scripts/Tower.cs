using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float damage;

    public GameObject currentTarget;

    protected void updateNearestEnemy()
    {
        GameObject currentNearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach(GameObject enemy in Enemies.enemies)
        {
            if(enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;
                if(_distance < distance)
                {
                    distance = _distance;
                    currentNearestEnemy = enemy;
                }
            }
        }

        if(distance <= range)
        {
            currentTarget = currentNearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }
}
