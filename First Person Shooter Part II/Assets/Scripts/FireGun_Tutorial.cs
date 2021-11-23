using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FireGun : MonoBehaviour
{
    [SerializeField] Camera headCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    InputAction fireAction;

    void Awake()
    {
        fireAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions.FindActionMap("Player").FindAction("Click");
    }

    void OnEnable()
    {
        fireAction.performed += HandleFire;
    }
    void OnDisable()
    {
        fireAction.performed -= HandleFire;
    }

    private void HandleFire(InputAction.CallbackContext inputContext)
    {
        // TODO: Play muzzle flash

        // defining raycast variable to store later
        RaycastHit hit;

        bool PLACEHOLDER_BOOL = true;
        String PLACEHOLDER_STRING = "Studio Rocks!";

        // TODO: Raycast from head to the direction the player is facing.
        if (PLACEHOLDER_BOOL)
        {
            // Log name of object that was hit
            Debug.Log(PLACEHOLDER_STRING);

            // TODO: Get reference to target that was hit.


            // TODO: Instantiate and destroy impact effect on target with correct direction.
            GameObject PLACEHOLDER = null;
            GameObject impactGameObject = Instantiate(PLACEHOLDER);
            Destroy(PLACEHOLDER);
        }
    }
}