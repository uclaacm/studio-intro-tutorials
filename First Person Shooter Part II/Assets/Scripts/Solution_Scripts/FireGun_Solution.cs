using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FireGun_Solution : MonoBehaviour
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
        muzzleFlash.Play();
        // defining raycast variable to store later
        RaycastHit hit;

        // Checking if we hit something. Physics.Raycast returns true if something is hit. We pass in headCamera.transform.position to get the starting position
        // of the ray. Then headCamera.transform.forward to get the direction of the ray. Then we store the object we hit into our 'hit' variable.
        if (Physics.Raycast(headCamera.transform.position, headCamera.transform.forward, out hit))
        {
            // logging name of object that was hit.
            Debug.Log(hit.transform.name);

            // getting target hit
            Target_Solution target = hit.transform.GetComponent<Target_Solution>();

            // if a target was actually hit
            if (target != null)
            {
                // decrease hp of target by 1
                target.TakeDamage(1f);
            }
            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 2f);
        }
    }
}
