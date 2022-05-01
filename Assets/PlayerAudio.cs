using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] HurtClips;
    public AudioClip[] NormalClips;

    public AudioSource audioSource;

    public void PlayHurtSound()
    {
        audioSource.clip = HurtClips[Random.Range(0, HurtClips.Length)];
        audioSource.Play();
    }

    public void PlayNormalSound()
    {
        audioSource.clip = NormalClips[Random.Range(0, NormalClips.Length)];
        audioSource.Play();
    }
}
