using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvents : GrabEvents
{
    [SerializeField] private ParticleSystem[] particles;
    public void PlayParticle(int particle)
    {
        particles[particle].Play();
    }
}
