using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour 
{
    [SerializeField] Camera headCamera;
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
        // Get a reference to the specific InputAction of the object. This is the component that will broadcast an event
        // whenever it receives a new WASD input from the user (including letting go of keys).
        movementAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions.FindActionMap("Player").FindAction("Movement");

        // Get a reference to the animator. This is the component that will help us play the correct movement animations
        // based on the velocity of the player.
        animator = GetComponent<Animator>();


        // Make sure our mouse is invisible and locked within the game screen (so that the player doesn't have to worry about
        // it going all over the place when they're trying to play)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        // Determine the direction that the player's head is facing (not taking into account the y-axis). This is the
        // direction we want our player to move in.
        Vector3 desiredForwardVector = new Vector3(headCamera.transform.forward.x, 0, headCamera.transform.forward.z);

        // Rotate the player's transform so that they are now facing in the direction that their head was pointing.
        transform.forward = desiredForwardVector;
        
        // Calculate the velocity of the player by smoothly interpolating between their current velocity and the velocity
        // we want indicated by our arrow keys. Note that if we just immediately assigned the velocity to the player's
        // WASD input, the player's character would jolt abruptly upon changing directions, which doesn't feel good.
        // Essentially what Mathf.SmoothDamp() does is it takes in the velocity you're at (currentVelocity),
        // the velocity you WANT to be at (desiredDirection * maxSpeed), a variable it can use to store some data (refCurVel),
        // and a factor that determines how slowly you want to ease between your current and desired velocities (smoothTime)
        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, desiredDirection.x * maxSpeed, ref refCurVel.x, smoothTime);
        currentVelocity.y = Mathf.SmoothDamp(currentVelocity.y, desiredDirection.y * maxSpeed, ref refCurVel.y, smoothTime);

        // Finally, move the player
        transform.Translate(new Vector3(currentVelocity.x, 0, currentVelocity.y) * Time.deltaTime);

        // Tell the animator what the player's velocity is so that it knows how to animate the character
        animator.SetFloat("VelocityX", currentVelocity.x);
        animator.SetFloat("VelocityZ", currentVelocity.y);
    }


    
    void OnEnable()
    {
        // Set up listeners for events that are broadcast when new inputs are received from the player.
        // Remember that we want this event-listener model because it means we only need to do work
        // when there's new information for us (which is much better than polling for input).
        movementAction.performed += HandleChangedMoveDirection;
        movementAction.canceled += HandleCanceledMoveDirection;
    }

    void OnDisable()
    {
        // It's best practice to unsubscribe from events before you "leave" so that the event doesn't
        // try to invoke any methods that no longer function.
        movementAction.performed -= HandleChangedMoveDirection;
        movementAction.canceled -= HandleCanceledMoveDirection;
    }

    // This is the method that is triggered when a new direction is pressed, not including when directions are released, which
    // is handled in HandleCanceledMoveDirection()
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