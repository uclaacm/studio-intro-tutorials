using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject newObject;
    [SerializeField] Camera headCamera;

    // TODO: Initialize HP

    public void TakeDamage(float amount)
    {
        // TODO: Decrease hp and invoke Die method if target should die
        
    }

    void Die()
    {
        Destroy(gameObject);

        // TODO: If time there's stil time, instantiate a new target each time the target is destroyed.

    }

}
