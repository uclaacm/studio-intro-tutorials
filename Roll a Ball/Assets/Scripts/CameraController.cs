using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camTarget;
    public float posLerp = 0.04f;
    public float rotLerp = 0.01f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.transform.position, posLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, rotLerp);
    }
}
