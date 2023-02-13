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
        // TODO: Get LineRenderer component
        lineRend = gameObject.GetComponent<LineRenderer>();
        lineRend.material.color = Color.blue;
    }

    private void UpdateComponents()
    {
        // TODO: get Enemy script of the current target
        enemyScript = currentTarget.GetComponent<Enemy>();
    }

    /* damages the enemy for one instance when called */
    private void Shoot()
    {
        // TODO: deal damage to the current target
        enemyScript.TakeDamage(damage);
    }

    /* uses LineRenderer to draw a laser between the enemy and tower */
    private void DrawLaser()
    {
        if(currentTarget != null)
        {
            //NOTE: if you want the laser to show in front of some sprites, you need to set z-position of those sprites to -1 (map tiles)
            // TODO: Get the position of the tower and the current target and store them as Vector3
            Vector3 sp = transform.position;
            Vector3 ep = currentTarget.transform.position;

            lineRend.positionCount = 2; //2 vertices for the line

            // TODO: set position 0 of the LineRenderer to be the tower's position and position 1 to be that of current target
            lineRend.SetPosition(0, sp);
            lineRend.SetPosition(1, ep);
        }
        else
            lineRend.positionCount = 0; //if no current target, don't draw the laser
    }

    private void Update()
    {
        bool changedEnemy = UpdateNearestEnemy(); //from parent class

        // TODO: if the target changed, update Enemy component
        if(changedEnemy)
            UpdateComponents();

        // TODO: draw the laser
        DrawLaser();

        if(Time.time >= nextTimeToShoot) //if current time is greater than the next time to shoot
        {
            if(currentTarget != null)
            {
                // TODO: Shoot the current target
                Shoot();

                nextTimeToShoot = Time.time + timeBetweenShots; 
            }
        }
    }
}
