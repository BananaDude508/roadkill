using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerStats;

public class VehicleMovement : MonoBehaviour
{
    public TrailRenderer[] driftTrails;
    public GameObject body;

    [HideInInspector] public Vector2 localVel;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentGrip;
    private float speedRatio;
	private bool drifting;
    private float lastRotation;
    private float currentBodyRotation;


	private void Awake()
    {
        vehicleMovement = this;
        rb = GetComponent<Rigidbody2D>();
        rb.drag = vehicleStats.drag;
        rb.angularDrag = vehicleStats.angularDrag;
        currentGrip = vehicleStats.tyreGrip;
    }


    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Move();
        Steer();

        drifting = Input.GetKey(vehicleStats.driftKey);
        foreach (var driftTrail in driftTrails)
            driftTrail.emitting = drifting;

        currentGrip += drifting ? -vehicleStats.loseGripMult : vehicleStats.gainGripMult;
        currentGrip = Mathf.Clamp(currentGrip, -vehicleStats.driftGrip, vehicleStats.tyreGrip);

        UpdateBodyRotationValue();

		if (drifting)
            body.transform.localRotation = Quaternion.Euler(0, 0, currentBodyRotation);
        else
            body.transform.localRotation = Quaternion.identity;
	}

    private void Move()
    {
        rb.AddForce(transform.up * moveInput * vehicleStats.acceleration);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, vehicleStats.maxSpeed);

        localVel = transform.InverseTransformDirection(rb.velocity);
        speedRatio = localVel.y / vehicleStats.maxSpeed;
    }

    private void Steer()
    {
        float rotationAmount = -Mathf.Sign(localVel.y) * turnInput * vehicleStats.turnSpeed * speedRatio * Time.fixedDeltaTime;
        rotationAmount *= !drifting ? 1f : 1.2f;

        lastRotation = rb.rotation;

        rb.rotation += rotationAmount;
        rb.rotation = Mathf.Repeat(rb.rotation, 360);

        if (lastRotation - rotationAmount != rb.rotation)
        {
            lastRotation = rb.rotation - rotationAmount;
        }

        rb.AddForce(transform.right * (-localVel.x * currentGrip));
    }

    private void UpdateBodyRotationValue()
    {
        if (Mathf.Abs(localVel.y) <= 0.6f) return;

        if (!drifting || turnInput == 0)
        {
            if (Mathf.Abs(currentBodyRotation) <= 1f)
                currentBodyRotation = 0f;

			else if (currentBodyRotation != 0)
                currentBodyRotation -= Time.deltaTime * vehicleStats.bodyRotaPerSec * (currentBodyRotation > 0 ? 1 : -1);

            return;
        }
        currentBodyRotation -= Time.deltaTime * vehicleStats.bodyRotaPerSec * (turnInput > 0 ? 1 : -1);
		currentBodyRotation = Mathf.Clamp(currentBodyRotation, -vehicleStats.driftBodyRotation, vehicleStats.driftBodyRotation);
	}
}