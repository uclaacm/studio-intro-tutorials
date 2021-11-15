using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] InputAction rotationAction;
    [SerializeField] Transform headTransform;
    [SerializeField] Transform bodyTransform;

    void Awake()
    {
        rotationAction = GetComponent<PlayerInput>().actions.FindActionMap("Player").FindAction("Rotation");
    }

    void OnEnable()
    {
        rotationAction.performed += HandleNewRotation;
    }

    private void HandleNewRotation(InputAction.CallbackContext inputContext)
    {
        Vector2 mouseInput = inputContext.ReadValue<Vector2>();

        //headTransform.Rotate(new Vector3(mouseInput.y * Time.deltaTime, 0, 0), );
    }
}