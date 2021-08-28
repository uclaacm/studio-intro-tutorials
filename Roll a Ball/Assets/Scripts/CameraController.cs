using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camTarget;
    public float posLerp = 0.04f;

    private 
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, posLerp);
    }
}
