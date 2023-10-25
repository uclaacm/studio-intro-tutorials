using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")  // Check that our player hits this object
        {
            gm.AddScore(1);
            Destroy(gameObject);
        }
    }
}
