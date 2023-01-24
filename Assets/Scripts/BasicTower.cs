using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [SerializeField] private float timeBetweenShots; //in seconds
    [SerializeField] private float bulletSpeed;
    public Transform pivot;
    public Transform barrel;
    public Bullet bullet;

    private float nextTimeToShoot;

    private void shoot()
    {
        Bullet newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
        newBullet.setDamage(damage);
        newBullet.setSpeed(bulletSpeed);
    }

    private void Update()
    {
        updateNearestEnemy();
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
