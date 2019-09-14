using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParticle : MonoBehaviour
{
    public ParticleSystem dirtParticleSystem;
    public ParticleSystem airParticleSystem;

    public void PlayDirt() {
        dirtParticleSystem.Play();
    }

    public void PlayAir() {
        airParticleSystem.Play();
    }
}
