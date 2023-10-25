using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")  // Check that our player hits this object
        {
            Debug.Log("Object collected!");
            Destroy(gameObject);
        }
    }
}
