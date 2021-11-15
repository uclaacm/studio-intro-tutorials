using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CharacterLookController : CinemachineExtension
{
    // Cinemachine extension from https://www.youtube.com/watch?v=5n_hmqHdijM
    [SerializeField] PlayerInput playerInput;
    InputAction rotationAction;
    Vector3 startingRotation;       // Represents the angle to turn the head left-right (y) and up-down(x)


    // Camera look variables
    [SerializeField] float horizontalSpeed = 10;
    [SerializeField] float verticalSpeed = 10;
    [SerializeField] float verticalClampAngle = 80;     // Prevents the camera from rotating past this angle

    protected override void Awake()
    {
        rotationAction = playerInput.actions.FindActionMap("Player").FindAction("Rotation");
        base.Awake();
    }

    // This callback is invoked when this Cinemachine camera changes to a new state (it cycles through 4 stages)
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {   
        // If this Cinemachine camera is set to a Follow target
        if(vcam.Follow)
        {
            // If this state that we're on is the AIM state
            if(stage == CinemachineCore.Stage.Aim)
            {
                // If the rotation that we intend to give to the player's camera has not yet been initialized, initialize it
                // to the current rotation of the camera
                if(startingRotation == null)
                    startingRotation = transform.localRotation.eulerAngles;

                // Figure out what the last change in mouse position was
                Vector2 mouseDelta = rotationAction.ReadValue<Vector2>();

                // Adjust the rotation that we intend to give the player's camera so that it accounts for inputs received
                // from the user through their mouse movements. Remember that when we move our mouse on the horizontal
                // axis, that corresponds to a rotation about the y-axis. And when we move our mouse on the vertical axis,
                // that corresponds to a rotation about the x-axis. This is why the x and y values below are swapped.
                startingRotation.y += mouseDelta.x * horizontalSpeed * Time.deltaTime;
                startingRotation.x += mouseDelta.y * verticalSpeed * Time.deltaTime;

                // The Mathf.Clamp() function is used to keep the starting rotation from going too far (the player shouldn't
                // be able to rotate their head forever!)
                startingRotation.x = Mathf.Clamp(startingRotation.x, -verticalClampAngle, verticalClampAngle);

                // Finally, now that we've applied the effects of our latest inputs, we set the cinemachine camera's rotation
                state.RawOrientation = Quaternion.Euler(-startingRotation.x, startingRotation.y, 0);

            }
        }
    }
}
