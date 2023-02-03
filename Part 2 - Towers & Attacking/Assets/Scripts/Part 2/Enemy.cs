using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<GameObject> enemies = new List<GameObject>(); //creates a list of Enemy objects

    [SerializeField] private float health;

    private void Awake()
    {
        Enemy.enemies.Add(gameObject); //add current Enemy object to the list in Enemies
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg; //reduce health by dmg
    }

    public void Die()
    {
        Enemy.enemies.Remove(gameObject); //remove current Enemy object from the list in Enemies
        Destroy(gameObject); //destroy current object
    }

    private void Update()
    {
        //check if health is less than 0
        if(health <= 0)
            Die();
    }
}
