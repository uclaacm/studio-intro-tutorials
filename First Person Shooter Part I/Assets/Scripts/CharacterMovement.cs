using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour 
{
    [SerializeField] Camera headCamera;
    [SerializeField] Transform bodyTransform;
    [SerializeField] float maxSpeed = 5;
    Vector2 desiredDirection = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    InputAction movementAction;
    Animator animator;

    // SMOOTHING VARS
    float smoothTime = 0.1f;
    Vector2 refCurVel;

    void Awake()
    {
        movementAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions.FindActionMap("Player").FindAction("Movement");
        animator = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        // Rotate the body to point in the direction the camera is pointing
        Vector3 desiredForwardVector = new Vector3(headCamera.transform.forward.x, 0, headCamera.transform.forward.z);
        transform.forward = desiredForwardVector;
        
        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, desiredDirection.x * maxSpeed, ref refCurVel.x, smoothTime);
        currentVelocity.y = Mathf.SmoothDamp(currentVelocity.y, desiredDirection.y * maxSpeed, ref refCurVel.y, smoothTime);

        transform.Translate(new Vector3(currentVelocity.x, 0, currentVelocity.y) * Time.deltaTime);

        animator.SetFloat("VelocityX", currentVelocity.x);
        animator.SetFloat("VelocityZ", currentVelocity.y);
    }



    void OnEnable()
    {
        movementAction.performed += HandleChangedMoveDirection;
        movementAction.canceled += HandleCanceledMoveDirection;
    }

    void OnDisable()
    {
        movementAction.performed -= HandleChangedMoveDirection;
        movementAction.canceled -= HandleCanceledMoveDirection;
    }

    private void HandleChangedMoveDirection(InputAction.CallbackContext inputContext)
    {
        desiredDirection = inputContext.ReadValue<Vector2>();
    }

    // The input system detects returning to the default state as the movement input being "canceled" rather than "changed"
    private void HandleCanceledMoveDirection(InputAction.CallbackContext inputContext) 
    {
        desiredDirection = Vector2.zero;
    }


    
}