using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParticle : MonoBehaviour
{
    public ParticleSystem airParticleSystem;

    public void PlayAir() {
        airParticleSystem.Play();
    }
}
