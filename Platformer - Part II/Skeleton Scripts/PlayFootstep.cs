using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PlayFootstep : MonoBehaviour
{

	[SerializeField] private AudioClip footsteps;
	private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (footsteps == null)
            Debug.LogWarning("PlayFootstep script not provided with AudioClip.");
    }

    // TODO: Play footstep sound
    void PlayFootsteps()
    {
        
    }

    // TODO: Stop playing footstep sound
    void StopPlaying()
    {
        
    }
}