using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyMovement : MonoBehaviour
{
    enum Direction {left = -1, right = 1};
    private Rigidbody2D rb;
    private Animator animator;
    public float speed;
    public int health = 3;
    // Initial orientation of the sprite renderer
    private Vector3 initScale;

    // Set boundaries for patrol
    [SerializeField] private Vector3 leftEdge;
    [SerializeField] private Vector3 rightEdge;

    // Check if we're moving in a certain direction
    private bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale;
        movingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if (movingLeft)
        {
            // If we hit the boundary, have the enemy switch direction
            if ( transform.position.x < leftEdge[0])
                movingLeft = false;
            Move(Direction.left);
        }
        else
        {
            if ( transform.position.x > rightEdge[0])
                movingLeft = true;
            Move(Direction.right);
        }
    }
    
    void Move(Direction dir)
    {
        // Set animator boolean for movement
        animator.SetBool("isRunning", true);

        switch (dir)
        {
            case Direction.left:
                // Keep initial orientation of sprite is moving left
                transform.localScale = new Vector3(Mathf.Abs(initScale.x), initScale.y, initScale.z);
                break;
            case Direction.right:
                // Otherwise, we flip the sprite
                transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1, initScale.y, initScale.z);
                break;
        }

        // Have enemy move in the specified direction
        rb.velocity = new Vector2(speed * (int) dir, rb.velocity.y);
    }
    public void takeDamage(){
        health--;
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
