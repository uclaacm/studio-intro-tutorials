using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private Vector2 position;
    
    public bool grounded;
    public float speed;
    public float jumpHeight;
    public Animator animator;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        position = rb.position;
        grounded = false;
    }

    void OnMove(InputValue movementVal)
    {
        horizontal = 0;
        vertical = 0;
        Vector2 movement = movementVal.Get<Vector2>();
        horizontal = movement.x * speed;
        if (movement.y > 0 && grounded) 
        {
            vertical = jumpHeight;

        }
    }

    void Update()
    {
        if(horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);

        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical <= 0 ? rb.velocity.y : vertical);
        vertical = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }
}
