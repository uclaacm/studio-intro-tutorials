using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementTutorial : MonoBehaviour
{
    [SerializeField] Camera headCamera;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float jumpHeight = 5;
    Vector2 desiredDirection = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    float verticalSpeed = 0;
    InputAction movementAction;
    InputAction jumpAction;
    Animator animator;

    const float GRAVITY = 9.8f;
    bool isGrounded = false;

    // SMOOTHING VARS
    float smoothTime = 0.1f;
    Vector2 refCurVel;

    void Awake()
    {
        // TODO: Get a reference to the InputActions we care about (movement and jump)
        movementAction = null;
        jumpAction = null;

        // TODO: Get a reference to the animator so we can later use it to play animations based on movement
        animator = null;


        // TODO: Hide and lock our mouse so it doesn't get distracting

    }

    const float PLACEHOLDER_FLOAT = 0;
    void Update()
    {
        // TODO: Calculate the forward direction we WANT the player to move in (based on the direction the head is facing)
        Vector3 desiredForwardVector = new Vector3(PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT);

        // Rotate the player's transform so that they are now facing in the direction that their head was pointing.
        transform.forward = desiredForwardVector;
        
        // TODO: Determine the next velocity values based on a smoothing function (Hint: use Mathf.SmoothDamp())
        currentVelocity.x = PLACEHOLDER_FLOAT;
        currentVelocity.y = PLACEHOLDER_FLOAT;

        // TODO: Determine how to change the vertical speed of the player based on the character's current state
        if(!isGrounded)                     // STATE: Falling
            verticalSpeed = PLACEHOLDER_FLOAT;
        else if(verticalSpeed <= -0.1f)     // STATE: Landing
        {
            verticalSpeed = PLACEHOLDER_FLOAT;
            animator.ResetTrigger("HasJumped");
        }
        else                                // STATE: Standing
        {
            // Nothing?
        }

        // TODO: Move the player based on the calculated velocities

        // Tell the animator what the player's velocity is so that it knows how to animate the character
        animator.SetFloat("VelocityX", currentVelocity.x);
        animator.SetFloat("VelocityZ", currentVelocity.y);
    }

    

    [Header("Ground Check Box")]
    [SerializeField] Transform feetTransform;
    [SerializeField] float groundCastDist = 1f;
    [SerializeField] float sideLength = 0.5f;
    [SerializeField] float boxDepth = 0.1f;
    void FixedUpdate()
    {
        // TODO: Determine whether the player is touching the ground with a call to Physics.BoxCast()
        isGrounded = false;     // Replace "false" with Physics.BoxCast(...)
        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(feetTransform.position + Vector3.down * groundCastDist, new Vector3(sideLength, boxDepth, sideLength));
    }

    
    void OnEnable()
    {
        // TODO: Set up listeners for events broadcast by InputActions
    }

    
    void OnDisable()
    {
        // TODO: Unsubscribe listeners from events broadcast by InputActions
    }

    // This is the method that is triggered when a new direction is pressed, not including when directions are released, which
    // is handled in HandleCanceledMoveDirection()
    private void HandleChangedMoveDirection(InputAction.CallbackContext inputContext)
    {
        // TODO: Determine which direction the user wants to move in based on received inputs
        desiredDirection = new Vector2(PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT);
    }

    // The input system detects returning to the default state as the movement input being "canceled" rather than "changed"
    private void HandleCanceledMoveDirection(InputAction.CallbackContext inputContext) 
    {
        desiredDirection = Vector2.zero;
    }

    private void HandleJump(InputAction.CallbackContext inputContext)
    {
        if(!isGrounded) {return;}

        // TODO: Calculate what the initial vertical velocity of the player should be based on how high we want to jump
        verticalSpeed = PLACEHOLDER_FLOAT;
        animator.SetTrigger("HasJumped");
    }
}
