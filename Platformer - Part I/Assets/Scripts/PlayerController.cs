using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator animator;
    private bool grounded;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    void Start()
    {
        // Getting components that are attached to the player
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting inputs

        float inputX = Input.GetAxis("Horizontal");

        if(inputX == 0){
            animator.SetBool("isRunning", false);
        }
        else{
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3((inputX < 0 ? -2: 2), 2, 2);
        }

        // Changing speed of the player based on input.
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        // Execute jump if the correct input is inputted
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && grounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            grounded = false;
            animator.SetBool("isJumping", true);
        }
    }

}
