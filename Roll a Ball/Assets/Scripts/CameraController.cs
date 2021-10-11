using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject camTarget;
    private float lerpTime = 0.01f;
    private Vector3 offset;
    private float hMagnitude;

    [SerializeField] private GameObject player;
    private Transform playerTransform;
    private Rigidbody playerBody;

    void Start()
    {
        playerTransform = player.transform;
        playerBody = player.GetComponent<Rigidbody>();

        offset = transform.position - playerTransform.position;
        hMagnitude = (new Vector2(offset.x, offset.z)).magnitude;
    }

    // LateUpdate is called once per frame after all update functions have been called.
    // This method is useful to order script execution.
    // We use LateUpdate because we want to update the camera's position only after all tracked objects have been moved.
    void LateUpdate()
    {
        // Calculate horizontal direction of ball movement
        Vector2 horizontal = new Vector2(playerBody.velocity.x, playerBody.velocity.z);

        // If the ball is moving, calculate the new position of the camera
        if (horizontal.magnitude > 0)
        {
            Vector2 offsetHorizontal = horizontal.normalized * hMagnitude;
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(-offsetHorizontal.x, offset.y, -offsetHorizontal.y), lerpTime);
        }
        else    // Otherwise use the current position of the camera
        {
            Vector2 offsetHorizontal = (transform.position - playerTransform.position).normalized * hMagnitude;
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(offsetHorizontal.x, offset.y, offsetHorizontal.y), lerpTime);
        }

        // Always look at the ball
        transform.LookAt(playerTransform.position, Vector3.up);
    }
}
