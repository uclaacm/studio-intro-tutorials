using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range; //range of the tower
    [SerializeField] protected float damage; //damage that the tower does per instance

    [HideInInspector] public GameObject currentTarget; //nearest enemy to tower

    /* checks through all the current enemies and finds the closest one to set as
    the current target of the tower 
    returns true if the current enemy has changed, 
    false if it has not or if there is no current enemy */
    protected bool UpdateNearestEnemy()
    {
        GameObject currentNearestEnemy = null; //will store current nearest enemy
                                        //as we loop through all the enemies

        float distance = Mathf.Infinity; //getting a very large distance, this 
                                        //variable will be storing the smallest
                                        //distance between an enemy and the tower 

        // TODO: loop through all the enemies and find the closest enemy
        foreach(GameObject enemy in Enemy.enemies)
        {
            if(enemy != null)
            {
                float currDistance = (transform.position - enemy.transform.position).sqrMagnitude;
                if(currDistance < distance)
                {
                    distance = currDistance;
                    currentNearestEnemy = enemy; 
                }
            }
        }
        // TODO: check if the nearest enemy is in the tower's range. If so, check if the
        // current target has changed (return false if not, true if it has changed) and
        // set current target to the new nearest enemy if it has changed.
        // If nearest enemy is not in range, make sure currentTarget does not store an object
        if(distance <= range)
        {
            if(currentNearestEnemy == currentTarget)
                return false;
            else
            {
                currentTarget = currentNearestEnemy;
                return true;
            }
        }
        else
        {
            currentTarget = null;
        }

        return false;
    }
}
