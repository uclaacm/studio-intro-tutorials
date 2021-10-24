using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PlayFootstep : MonoBehaviour
{

	[SerializeField] private AudioClip footsteps;
	private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (footsteps == null)
            Debug.LogWarning("PlayFootstep script not provided with AudioClip.");
    }

    void PlayRandomFootstep()
    {
    	source.PlayOneShot(footsteps);
    }
}
