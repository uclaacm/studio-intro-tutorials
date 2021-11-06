using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector2 position;

    private Rigidbody2D rb;
    private Animator animator;

    private bool moveLock;
 
    public bool grounded;
    public float speed;
    public float jumpHeight;

    public int health;
    public float knockbackX;
    public float knockbackY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        position = rb.position;
        grounded = false;


        moveLock = false;
    }

    void OnMove(InputValue movementVal)
    {
        if (!moveLock)
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

    }

    void OnFire()
    {
        if(!animator.GetBool("isJumping") || !animator.GetBool("hit"))
            animator.SetTrigger("attack");
    }
    void Update()
    {
        if (horizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3((horizontal < 0 ? -1 : 1), 1, 1);
        }

    }
    private void FixedUpdate()
    {
        Debug.Log(moveLock);
        if (!moveLock)
        {
            rb.velocity = new Vector2(horizontal, vertical <= 0 ? rb.velocity.y : vertical);
        }
        vertical = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            animator.SetBool("isJumping", false);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            int dir = collision.gameObject.GetComponent<Transform>().position.x > rb.position.x ? -1 : 1;
            horizontal = 0;
            vertical = 0;
            moveLock = true;
            rb.velocity = new Vector2(knockbackX*dir, knockbackY);
            animator.SetBool("isJumping", false);
            animator.SetBool("hit", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    public void endHit()
    {
        Debug.Log("end");
        animator.SetBool("hit", false);
        moveLock = false;
    }
}
