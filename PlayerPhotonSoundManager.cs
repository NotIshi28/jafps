using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhotonSoundManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepClip;
    
    
    public void PlayFootstepSound()
    {
        footstepSource.clip = footstepClip;

        footstepSource.Play();
    }
}
