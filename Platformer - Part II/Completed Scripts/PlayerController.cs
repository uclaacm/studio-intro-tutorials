using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator animator;

    private bool moveLock;
    public int health;
    public float knockbackX;
    public float knockbackY;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        moveLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!moveLock){

            float inputX = Input.GetAxis("Horizontal");

            if(inputX == 0){
                animator.SetBool("isRunning", false);
            }
            else{
                animator.SetBool("isRunning", true);
                transform.localScale = new Vector3((inputX < 0 ? -3: 3), 3, 3);
            }

            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
            if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded()){
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
            if(Input.GetKey(KeyCode.Mouse0)){
                animator.SetTrigger("attack");
            }
        }

    }

    bool isGrounded(){
        float distToGround = box.size.y * transform.lossyScale.y / 2 + .1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround);
        if(hit.collider != null){
            return true;
        }
        else{
            return false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            animator.SetBool("isJumping", false);
        }
        if(collision.gameObject.tag == "Enemy"){
            int dir = collision.gameObject.GetComponent<Transform>().position.x > rb.position.x ? -1 : 1;
            moveLock = true;
            rb.velocity = new Vector2(knockbackX*dir, knockbackY);
            animator.SetBool("hit", true);
            animator.SetBool("isJumping", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            animator.SetBool("isJumping", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyMovement>().takeDamage();
    }
    public void endHit()
    {
        animator.SetBool("hit", false);
        moveLock = false;
    }
}
