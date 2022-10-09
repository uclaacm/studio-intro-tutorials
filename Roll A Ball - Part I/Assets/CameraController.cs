using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] float yOffset = 1.5f;
    [SerializeField] float xOffset = 0f;
    [SerializeField] float zOffset = -2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(xOffset, yOffset, zOffset);        
    }
}
