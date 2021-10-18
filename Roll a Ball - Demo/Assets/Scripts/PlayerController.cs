using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 500f;
    
    // Holds a reference to a rigid body component of the game object is attached to
    private Rigidbody rb;

    private float movX;
    private float movY;

    // Starting position of the ball
    private Vector3 startingPosition;

    // You can initialize member variables at declaration
    private int touchingGround = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure you attach rigid body to the ball first
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    // Detects event and then calls Update
    void OnMove(InputValue movementVal)
    {
        // Horizontal movement
        Vector2 movementVector = movementVal.Get<Vector2>();

        movX = movementVector.x;
        movY = movementVector.y;
    }

    // onCollisionEnter is called when you start colliding with an object
    void OnCollisionEnter(Collision collision)
    {
    	if (collision.gameObject.tag == "Ground")
    	{
    		touchingGround++;
    	}
    }

    // onColisionExit is called when you stop colliding with an object
    void OnCollisionExit(Collision collision)
    {
    	if (collision.gameObject.tag == "Ground")
    	{
    		touchingGround--;
    	}
    }

    void OnTriggerEnter(Collider collider)
    {
    	if (collider.gameObject.name == "Respawn Plane")
    	{
    		// Respawn ball at starting position
    		transform.position = startingPosition;

    		// Also stop ball from moving & rotating at respawn
    		rb.velocity = Vector3.zero;
    		rb.angularVelocity = Vector3.zero;
    	}
    }

    // Only jump if you are touching the ground
    void OnJump()
    {
    	if (touchingGround > 0)
        	rb.AddForce(Vector3.up*jumpForce);
    }

    void FixedUpdate()
    {
        // We create a Vector3 using a constructor, passing the x, y values.
        // movY value is the third argument since the xz plane represents our floor.
        Vector3 movement = new Vector3(movX, 0.0f, movY);

        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Quaternion rotationCamera = Quaternion.LookRotation(cameraForward, Vector3.up);

        rb.AddForce(rotationCamera * movement * speed);
    }    
}
