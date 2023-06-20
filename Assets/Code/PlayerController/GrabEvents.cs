using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabEvents : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void PlaySoundEffect(AudioClip soundEffect)
    {
        audioSource.clip = soundEffect;
        audioSource.Play();
    }
}
