using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range; //range of the tower
    [SerializeField] protected float damage; //damage that the tower does per instance

    [HideInInspector]public GameObject currentTarget; //nearest enemy to tower

    /* checks through all the current enemies and finds the closest one to set as
    the current target of the tower
    
    returns true if the current enemy has changed, false if it has not or if there is no current enemy */
    protected bool UpdateNearestEnemy()
    {
        GameObject currentNearestEnemy = null; //will store current nearest enemy
                                        //as we loop through all the enemies

        float distance = Mathf.Infinity; //getting a very large distance, this 
                                        //variable will be storing the smallest
                                        //distance between an enemy and the tower 

        //loop through all the enemies in the list stored in the script Enemies.cs
        foreach(GameObject enemy in Enemy.enemies)
        {
            if(enemy != null)
            {
                //get the distance between the tower (transform.position) and the enemy
                float _distance = (transform.position - enemy.transform.position).sqrMagnitude;
                if(_distance < distance) //if the distance is smaller than the current smallest
                                            //distance
                {
                    distance = _distance; //set distance to smallest distance
                    currentNearestEnemy = enemy; //set enemy as the current nearest enemy
                }
            }
        }

        //if the current nearest enemy is in the tower's range
        if(distance <= range) 
        {
            if(currentNearestEnemy == currentTarget)
                return false;
            else
            {
                currentTarget = currentNearestEnemy; //set current target to be the enemy
                return true;
            }
        }
        else
        {
            currentTarget = null; //there is no current target (tower won't attack)
        }
        return false;
    }
}
