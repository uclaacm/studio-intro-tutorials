using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camTarget;
    public float posLerp = 0.04f;

    // LateUpdate is called once per frame after all update functions have been called.
    // This method is useful to order script execution.
    // We use LateUpdate because we want to update the camera's position only after all tracked objects have been moved.
    void LateUpdate()
    {
        // Lerp means linear interpolation, which returns a value betwteen two points in a linear scale.
        // In this case, we use lerp so that the camera changes its position in a fixed amount of time, resulting in smooth movement.
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, posLerp);
    }
}
