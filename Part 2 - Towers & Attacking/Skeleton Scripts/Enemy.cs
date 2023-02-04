using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // TODO: Create a public state list of GameObjects to store enemies

    [SerializeField] private float health;

    private void Awake()
    {
        // TODO: add current Enemy object to the list in Enemies
    }

    public void TakeDamage(float dmg)
    {
        // TODO: reduce health by dmg
    }

    public void Die()
    {
        // TODO: remove current Enemy object from the list in Enemies
        // TODO: destroy current object
    }

    private void Update()
    {
        // TODO: check if health is less than 0, kill the enemy if so
    }
}
