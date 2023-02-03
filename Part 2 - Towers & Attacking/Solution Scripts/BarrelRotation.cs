using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    public Transform pivot;
    public Transform barrel;

    public Tower tower;

    /* rotates barrel towards the enemy */
    private void Update()
    {
        if(tower != null)
        {
            if (tower.currentTarget != null) 
            {
                Vector2 relative = tower.currentTarget.transform.position - pivot.position;
                pivot.right = new Vector3(relative.x, relative.y, 0);
            }
        }
    }
}
