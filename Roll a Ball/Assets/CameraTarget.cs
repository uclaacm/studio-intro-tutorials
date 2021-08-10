using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public GameObject ball;
    
    private Rigidbody ballRB;
    private Vector3 offset;

    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballRB.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion lr = Quaternion.LookRotation(ballRB.velocity);
            Vector3 rotatedOffset = lr * offset;

            transform.position = ball.transform.position + rotatedOffset;
            transform.LookAt(ball.transform.position);

        }
    }
}
