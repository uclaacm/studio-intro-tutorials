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
            // TODO: Move the player left. 
            //       If we hit the left boundary, have the enemy switch direction.

        }
        else
        {
            // TODO: Move the player right. 
            //       If we hit the right boundary, have the enemy switch direction.
        }
    }
    
    void Move(Direction dir)
    {
        // Set animator boolean for movement
        animator.SetBool("isRunning", true);

        switch (dir)
        {
            case Direction.left:
                // TODO: Keep initial orientation of sprite is moving left
                
                break;
            case Direction.right:
                // TODO: Otherwise, we flip the sprite
                
                break;
        }

        // TODO: Have enemy move in the specified direction
        
    }
    
    // TODO: Decrement enemy's health and destroy enemy if health <= 0
    public void takeDamage(){
        
    }
}
