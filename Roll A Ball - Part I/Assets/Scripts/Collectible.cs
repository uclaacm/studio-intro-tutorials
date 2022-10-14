using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Adds score for collecting the object
        gm.AddScore(5f);

        // OPTIONAL: make the player jump again from picking up the object
        collision.gameObject.GetComponent<PlayerController>().Jump();

        Destroy(gameObject);
    }
}
