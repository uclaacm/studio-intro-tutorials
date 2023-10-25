using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Component references
    Rigidbody rb;  // Unity's physics component
    SphereCollider col;  // Collision detection 

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float respawnHeight = -10f;

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
        if (transform.position.y < respawnHeight)
            Respawn();
    }

    void OnJump()
    {
        //if player is on the ground, jump
        if (IsGrounded())
            Jump();
    }

    private void Jump()
    {
        // you thought high school physics was going to be useless? Think again!

        // 1/2 * m * v^2 = m * g * h
        // m's cancel out
        // 1/2 * v^2 = g * h
        // solve for v
        // v = sqrt(2 * g * h)
        float initialVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
        Debug.Log(Physics.gravity.magnitude);
        // Set the y velocity to some positive value while keeping the x and z whatever they were originally
        rb.velocity = new Vector3(rb.velocity.x, initialVelocity, rb.velocity.z);
    }

    void OnMove(InputValue moveVal)
    {
        //store input as a 2D vector
        Vector2 direction = moveVal.Get<Vector2>();
        Move(direction.x, direction.y);

    }

    private void Move(float x, float z)
    {
        // Set the x & z velocity of the Rigidbody to correspond with our inputs while keeping the y velocity what it originally is.
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
