using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // TODO: Create a public state list of GameObjects to store enemies
    public static List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float health;

    private int enemyMoneyValue = 1; // (NEW) how much an enemy is worth ($) when killed

    private void Awake()
    {
        // TODO: add current Enemy object to the list in Enemies
        Enemy.enemies.Add(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        // TODO: reduce health by dmg
        health -= dmg;
    }

    public void Die()
    {
        // TODO: remove current Enemy object from the list in Enemies
        // TODO: destroy current object
        Enemy.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        // TODO: check if health is less than 0, kill the enemy if so
        if (health <= 0)
        {
            Die();
            FindObjectOfType<CurrencySystem>().Gain(enemyMoneyValue); //(NEW) for gaining money when enemy killed
        }
    }
}
