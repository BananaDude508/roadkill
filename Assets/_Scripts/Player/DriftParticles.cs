using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftParticles : MonoBehaviour
{
	public ParticleSystem[] frontDriftParticles;
	public ParticleSystem[] backDriftParticles;

	private VehicleMovement vehicle;
	private KeyCode driftKey;
	private bool goingForwards;
	private bool lastGoingForwards;
	private bool particlesActive;


	private void Awake()
	{
		vehicle = GetComponentInParent<VehicleMovement>();
		driftKey = vehicle.driftKey;
	}

	private void Update()
	{
		goingForwards = vehicle.localVel.y >= 0;

		if (Input.GetKeyDown(driftKey))
		{
			particlesActive = true;
			UpdateActiveParticles(true);
		}

		if (Input.GetKeyUp(driftKey))
		{
			particlesActive = false;
			UpdateActiveParticles(false);
		}

		if (goingForwards != lastGoingForwards)
			UpdateActiveParticles(particlesActive);
	}

	void UpdateActiveParticles(bool start)
	{
		if (start)
		{
			lastGoingForwards = goingForwards;

			if (goingForwards)
				foreach (ParticleSystem particle in backDriftParticles)
					particle.Play();
			else
				foreach (ParticleSystem particle in frontDriftParticles)
					particle.Play();
		}
		else
		{
			lastGoingForwards = goingForwards;

			foreach (ParticleSystem particle in backDriftParticles)
				particle.Stop();
			foreach (ParticleSystem particle in frontDriftParticles)
				particle.Stop();
		}
	}
}
