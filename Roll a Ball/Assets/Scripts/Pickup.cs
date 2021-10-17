using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "Player")
       {
           ScoreController.score += 1;
           Destroy(this.gameObject);
       }
   }
}
