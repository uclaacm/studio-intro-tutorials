using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player's Transform (their position)
    [SerializeField] private Transform player;

    // Offsets for our camera
    [SerializeField] float yOffset = 1.5f;
    [SerializeField] float xOffset = 0f;
    [SerializeField] float zOffset = -2f;

    void Update()
    {
        ///
        /// So we want the camera to follow the player, but at a distance. To do this we set our
        /// camera's position to the player's position vector plus another vector containing an offset.
        ///
        transform.position = player.position + new Vector3(xOffset, yOffset, zOffset);        
    }
}
