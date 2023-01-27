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

    private void Shoot()
    {
        Bullet newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
        newBullet.SetDamage(damage);
        newBullet.SetSpeed(bulletSpeed);
    }

    private void Update()
    {
        UpdateNearestEnemy();
        if(Time.time >= nextTimeToShoot)
        {
            if(currentTarget != null)
            {
                Shoot();
                nextTimeToShoot = Time.time + timeBetweenShots; //check if can replace with deltaTime
            }
        }
    }
}
