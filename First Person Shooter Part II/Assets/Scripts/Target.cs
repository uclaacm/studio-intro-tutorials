using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // initializing hp
    public float hp = 1f;

    // take damage function
    public void TakeDamage(float amount)
    {
        // decrease hp by an amount and if hp reaches 0, the target dies.
        hp -= amount;
        if(hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}
