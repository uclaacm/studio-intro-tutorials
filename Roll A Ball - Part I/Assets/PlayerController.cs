using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider col;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] LayerMask jumpableLayer; // For more robust implementation
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(inputX * speed, rb.velocity.y, inputZ * speed);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
    }

    bool isGrounded()
    {
        float distToGround = col.radius * transform.lossyScale.y;  // This replaces the need to use a magic number
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }
}
