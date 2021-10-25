using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private Vector2 position;
    private bool grounded;

    public float speed;
    public float jumpHeight;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        position = rb.position;
        grounded = false;
    }

    void Update()
    {
        horizontal = 0;
        vertical = 0;
        if (Input.GetKey(KeyCode.A))
            horizontal -= speed;
        if (Input.GetKey(KeyCode.D))
            horizontal += speed;

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            vertical = jumpHeight;
        }

    }
    private void FixedUpdate()
    {
        if (vertical != 0)
            Debug.Log(vertical);
        rb.velocity = new Vector2(horizontal, vertical == 0 ? rb.velocity.y : vertical);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            Debug.Log("ground");
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            grounded = false;
        }
    }
}
