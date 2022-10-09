using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Component references
    Rigidbody rb;  // Unity's physics component
    SphereCollider col;  // Collision detection 

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the components attached to the current GameObject
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Unity's built-in input system commands
        float inputX = Input.GetAxis("Horizontal");  // Checks the keys associated with horizontal movement (A, D, leftarrow, rightarrow)
        float inputZ = Input.GetAxis("Vertical");  // Checks the keys associated with vertical movement (W, S, uparrow, downarrow)

        // Set the x & z velocity of the Rigidbody to correspond with our inputs while keeping the y velocity what it originally is.
        rb.velocity = new Vector3(inputX * speed, rb.velocity.y, inputZ * speed);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())  // Jump if space is pressed and player is grounded
        {
            Jump();
        }
    }

    bool isGrounded()
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

    void Jump()
    {
        // Set the y velocity to some positive value while keeping the x and z whatever they were originally
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }
}
