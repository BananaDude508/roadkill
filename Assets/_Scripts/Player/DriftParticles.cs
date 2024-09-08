using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftParticles : MonoBehaviour
{
	public ParticleSystem[] driftParticles;

	private VehicleMovement vehicle;
	private KeyCode driftKey;


	private void Awake()
	{
		vehicle = GetComponentInParent<VehicleMovement>();
		driftKey = vehicle.driftKey;
	}

	private void Update()
	{
		if (Input.GetKeyUp(driftKey))
			foreach (ParticleSystem particle in driftParticles)
				particle.Stop();

		if (Input.GetKeyDown(driftKey))
			foreach (ParticleSystem particle in driftParticles)
				particle.Play();

		if (Input.GetKey(driftKey))
            foreach (ParticleSystem particle in driftParticles)
			{
				var main = particle.main;
				main.startRotationZ = (-vehicle.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) + (Mathf.PI / 2);
			}
				
    }
}
