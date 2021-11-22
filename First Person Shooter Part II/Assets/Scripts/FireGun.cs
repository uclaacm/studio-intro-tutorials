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
        fireAction = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions.FindActionMap("Player").FindAction("Click"); // PART 2
    }

    void OnEnable()
    {
        fireAction.performed += HandleFire;     // PART 2
    }
    void OnDisable()
    {
        fireAction.performed -= HandleFire; // PART 2
    }

    private void HandleFire(InputAction.CallbackContext inputContext)
    {
        // playing muzzle flash particle effect
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
            Target target = hit.transform.GetComponent<Target>();

            // if a target was actually hit
            if (target != null)
            {
                // decrease hp of target by 1
                target.TakeDamage(1f);
            }

            // creating impact particle effect. instantiating the particle effect at the object's location and setting the particle effect to point out
            // from the direction we shot in. The Instantiate function takes in a Quaternion so we use Quaternion.LookRotation() to take in a direction
            // and turn it into a Quaternion. The direction we want is the normal vector because it will point out from the direction we shot in, so we pass in 'hit.normal'.
            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 2f);
        }
    }
}
