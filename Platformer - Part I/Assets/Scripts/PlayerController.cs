using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private bool grounded;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && grounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            grounded = false;
        }
        Debug.Log(grounded);
    }

    // bool isGrounded()
    // {
    //     float distToGround = box.size.y * transform.lossyScale.y / 2f;
        
    //     return Physics2D.Raycast(transform.position, Vector2.down, distToGround);
    // }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
        }
        Debug.Log("Hit Ground");
    }
}
