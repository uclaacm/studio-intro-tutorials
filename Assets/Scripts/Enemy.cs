using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
    }

    public void die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(health <= 0)
            die();
    }
}
