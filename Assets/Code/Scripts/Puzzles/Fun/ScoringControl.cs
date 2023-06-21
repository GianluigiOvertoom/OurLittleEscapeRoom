using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringControl : MonoBehaviour
{
    [SerializeField] private AudioClip bounceSound;
    private GrabEvents grabEvents;

    private void Start()
    {
        grabEvents = GetComponent<GrabEvents>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        grabEvents.PlaySoundEffect(bounceSound);
    }
}
