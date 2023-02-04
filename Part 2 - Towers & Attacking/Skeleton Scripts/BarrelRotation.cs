using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform barrel;

    [SerializeField] private Tower tower;

    /* rotates barrel towards the enemy */
    private void Update()
    {
        if(tower != null)
        {
            if (tower.currentTarget != null) 
            {
                // TODO: Rotate the barrel towards the current target
            }
        }
    }
}
