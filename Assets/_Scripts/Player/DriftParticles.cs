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


	private void Awake()
	{
		vehicle = GetComponentInParent<VehicleMovement>();
		driftKey = vehicle.driftKey;
	}

	private void Update()
	{
		goingForwards = vehicle.localVel.y >= 0;

		if (Input.GetKeyDown(driftKey))
			UpdateActiveParticles();

		if (Input.GetKeyUp(driftKey))
			UpdateActiveParticles(false);

		if (goingForwards != lastGoingForwards && Input.GetKey(driftKey))
				UpdateActiveParticles();
	}

	void UpdateActiveParticles(bool start = true)
	{
		if (!start)
		{
			foreach (ParticleSystem particle in frontDriftParticles)
				particle.Stop();
			foreach (ParticleSystem particle in backDriftParticles)
				particle.Stop();
			return;
		}

		if (goingForwards)
		{
			foreach (ParticleSystem particle in backDriftParticles)
				particle.Play();
			foreach (ParticleSystem particle in frontDriftParticles)
				particle.Stop();
		}
		else
		{
			foreach (ParticleSystem particle in frontDriftParticles)
				particle.Play();
			foreach (ParticleSystem particle in backDriftParticles)
				particle.Stop();
		}
		lastGoingForwards = goingForwards;
	}
}
