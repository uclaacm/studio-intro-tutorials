using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Public means other scripts can access this value. It will also show up in the editor
    public float speed = 0;
    
    // Holds a reference to a rigid body component of the game object is attached to
    private Rigidbody rb;

    private float movX;
    private float movY;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure you attach rigid body to the ball first
        rb = GetComponent<Rigidbody>();     
    }

    // Detects event and then calls Update
    void OnMove(InputValue movementVal)
    {
        // Horizontal movement
        Vector2 movementVector = movementVal.Get<Vector2>();

        movX = movementVector.x;
        movY = movementVector.y;

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
