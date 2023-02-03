using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower //inherits from Tower.cs
{
    [SerializeField] private float timeBetweenShots; 
    LineRenderer lineRend;
    Enemy enemyScript;

    private float nextTimeToShoot;

    void Awake()
    {
        lineRend = gameObject.GetComponent<LineRenderer>(); //getting LineRenderer
    }

    private void UpdateComponents()
    {
        enemyScript = currentTarget.GetComponent<Enemy>(); //getting the Enemy script from the current target
    }

    /* damages the enemy for one instance when called */
    private void Shoot()
    {
        enemyScript.TakeDamage(damage); //call the take damage function in the Enemy script
    }

    /* uses LineRenderer to draw a laser between the enemy and tower */
    private void DrawLaser()
    {
        if(currentTarget != null)
        {
            //NOTE: if you want the laser to show in front of some sprites, you need to set z-position of those sprites to -1 (map tiles)
            Vector3 sp = transform.position; //starting point - position of tower
            Vector3 ep = currentTarget.transform.position; //end point - position of the target
            lineRend.positionCount = 2; //2 vertices for the line
            lineRend.SetPosition(0, sp); //set first vertex as the starting point
            lineRend.SetPosition(1, ep); //set second vertex as the end point
        }
        else
            lineRend.positionCount = 0; //if no current target, don't draw the laser
    }

    private void Update()
    {
        bool changedEnemy = UpdateNearestEnemy(); //from parent class

        if(changedEnemy)
            UpdateComponents();
        
        DrawLaser();

        if(Time.time >= nextTimeToShoot) //if current time is greater than the next time to shoot
        {
            if(currentTarget != null)
            {
                Shoot();
                nextTimeToShoot = Time.time + timeBetweenShots; 
            }
        }
    }
}
