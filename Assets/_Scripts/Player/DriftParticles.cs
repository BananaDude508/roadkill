using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftParticles : MonoBehaviour
{
    public ParticleSystem[] frontDriftParticles;
    public ParticleSystem[] backDriftParticles;

    private VehicleMovement vehicle;


    private void Awake()
    {
        vehicle = GetComponentInParent<VehicleMovement>();
    }

    private void Update()
    {
        // Particles are not playing
        if (vehicle.drifting)
        {
            if (vehicle.localVel.y > 0)
                foreach (var particle in backDriftParticles)
                    particle.Play();

            else
                foreach (var particle in frontDriftParticles)
                    particle.Play();
        }
        else
        {
            foreach (var particle in backDriftParticles)
                particle.Stop();

            foreach (var particle in frontDriftParticles)
                particle.Stop();
        }
    }
}
