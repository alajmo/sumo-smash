using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParticle : MonoBehaviour
{
    public ParticleSystem dirtParticleSystem;

    private void Start()
    {
        // Play();
    }

    public void Play()
    {
        // dirtParticleSystem.Stop();
        dirtParticleSystem.Play();
        // dirtParticleSystem.Emit(101);

        // Debug.Log(dirtParticleSystem.isPaused);
        // Debug.Log(dirtParticleSystem.isStopped);
        // Debug.Log(dirtParticleSystem.IsAlive());
        // Debug.Log(dirtParticleSystem.isEmitting);
        // Debug.Log(dirtParticleSystem.isPlaying);

    }
}
