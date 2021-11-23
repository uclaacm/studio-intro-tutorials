using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Solution : MonoBehaviour
{

    public GameObject newTarget;
    [SerializeField] Camera headCamera;

    // initializing hp
    public float hp = 1f;

    // take damage function
    public void TakeDamage(float amount)
    {
        // decrease hp by an amount and if hp reaches 0, the target dies.
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        
        Instantiate(newTarget, new Vector3(headCamera.transform.position.x + Random.Range(5, 15),
            headCamera.transform.position.y + Random.Range(0, 3), headCamera.transform.position.z + Random.Range(5, 15)),
                Quaternion.LookRotation(headCamera.transform.forward));
        
    }
}