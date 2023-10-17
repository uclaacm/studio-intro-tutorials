using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: import UnityEngine.InputSystem and UnityEngine.SceneManagement


public class PlayerController : MonoBehaviour
{
    // TODO: add component references


    // TODO: add variables for speed, jumpHeight, and respawnHeight


    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get references to the components attached to the current GameObject

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: check if player is under respawnHeight and call Respawn()

    }

    void OnJump()
    {
        // TODO: check if player is on the ground, and call Jump()

    }

    private void Jump()
    {
        // TODO: Set the y velocity to some positive value while keeping the x and z whatever they were originally

    }

    void OnMove(InputValue moveVal)
    {
        //TODO: store input as a 2D vector and call Move()

    }

    private void Move(float x, float z)
    {
        // TODO: Set the x & z velocity of the Rigidbody to correspond with our inputs while keeping the y velocity what it originally is.

    }

    bool IsGrounded()
    {
        ///
        /// distToGround gets the distance the raycast needs to travel in order to hit something just beneath itself.
        /// Since the raycast shoots from the center of our collider, we need it to travel the radius of the collider
        /// times any sort of size scaling on the y axis that could've happened to our GameObject. transform.lossyScale gets
        /// the absolute scaling of the object, and SphereCollider.radius gets the radius of our collider.
        ///
        float distToGround = col.radius * transform.lossyScale.y;  // This replaces the need to use a magic number
        ///
        /// Physics.Raycast() shoots a ray from a given position, at a given direction, for a given distance.
        /// It returns a boolean stating if it hits anything other than itself.
        ///
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    private void Respawn()
    {
        // TODO: reload current scene

    }
}
