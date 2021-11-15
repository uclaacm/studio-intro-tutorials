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
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                    startingRotation = transform.localRotation.eulerAngles;

                Vector2 mouseDelta = rotationAction.ReadValue<Vector2>();

                startingRotation.y += mouseDelta.x * horizontalSpeed * Time.deltaTime;
                startingRotation.x += mouseDelta.y * verticalSpeed * Time.deltaTime;
                startingRotation.x = Mathf.Clamp(startingRotation.x, -verticalClampAngle, verticalClampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.x, startingRotation.y, 0);

            }
        }
    }
}
