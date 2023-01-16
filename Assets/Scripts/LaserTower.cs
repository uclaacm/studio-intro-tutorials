using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private float timeBetweenShots; //in seconds

    private float nextTimeToShoot;

    private void shoot()
    {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();
        enemyScript.takeDamage(damage);
    }

    private void drawLaser()
    {
        LineRenderer lineRend = gameObject.GetComponent<LineRenderer>();
        if(currentTarget != null)
        {
            //NOTE: if you want the laser to show in front of some sprites, you need to set z-position of those sprites to -1 (map tiles)
            Vector3 sp = transform.position;
            Vector3 ep = currentTarget.transform.position;
            lineRend.positionCount = 2;
            lineRend.SetPosition(0, sp);
            lineRend.SetPosition(1, ep);
        }
        else
            lineRend.positionCount = 0;
    }

    private void Update()
    {
        updateNearestEnemy();
        
        drawLaser();

        if(Time.time >= nextTimeToShoot)
        {
            if(currentTarget != null)
            {
                shoot();
                nextTimeToShoot = Time.time + timeBetweenShots; //check if can replace with deltaTime
            }
        }
    }
}
