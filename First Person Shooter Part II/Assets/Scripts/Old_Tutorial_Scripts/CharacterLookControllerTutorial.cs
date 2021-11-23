using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CharacterLookControllerTutorial : CinemachineExtension
{
    [SerializeField] PlayerInput playerInput;
    InputAction rotationAction;
    Vector3 desiredRotation;       // Represents the angle to turn the head left-right (y) and up-down(x)


    // Camera look variables
    [SerializeField] float horizontalSpeed = 10;
    [SerializeField] float verticalSpeed = 10;
    [SerializeField] float verticalClampAngle = 80;     // Prevents the camera from rotating past this angle


    const float PLACEHOLDER_FLOAT = 0;

    protected override void Awake()
    {
        // TODO: Get reference to the rotation action from our input system
        rotationAction = null;
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
                if(desiredRotation == null)
                    desiredRotation = transform.localRotation.eulerAngles;

                // TODO: Figure out what the last change in mouse position was
                Vector2 mouseDelta = new Vector2(PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT);

                // TODO: Calculate how many degrees to rotate based on the inputs received
                desiredRotation.y = PLACEHOLDER_FLOAT;
                desiredRotation.x = PLACEHOLDER_FLOAT;

                // TODO: Prevent the player's head from looking too far up or down (Hint: use Mathf.Clamp())
                desiredRotation.x = PLACEHOLDER_FLOAT;

                // TODO: Set the cinemachine camera's rotation based on the rotation we want
                state.RawOrientation = Quaternion.Euler(PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT, PLACEHOLDER_FLOAT);

            }
        }
    }
}
