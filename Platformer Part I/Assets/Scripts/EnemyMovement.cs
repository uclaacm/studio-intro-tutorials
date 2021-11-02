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
    
    private Vector3 initScale;

    // Enemy will idle for a certain amount of time and then continue moving
    [SerializeField] private float idleDuration;
    private float idleTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Move(Direction.left);
    }
    
    void Move(Direction dir)
    {
        // Set animator boolean for movement

        // Flip the sprite if moving left
        transform.localScale = new Vector3(Mathf.Abs(initScale.x) * (int) dir, initScale.y, initScale.z);

        // Have enemy move in the specified direction
        rb.velocity = new  Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

    }
}
