using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float acceleration = 35f;
    public float maxSpeed = 13f;
    public float turnSpeed = 150f;
    public float drag = 2f;
    public float angularDrag = 1f;
    public float tyreGrip = 10f;
    public float driftGrip = -3f;
    public float gainGripMult = .2f;
    public float loseGripMult = 1f;
    public KeyCode driftKey = KeyCode.LeftShift;
    public TrailRenderer[] driftTrails;
    public GameObject body;
    public float driftBodyRotation;
    public float bodyRotaPerSec = 150f;

    [HideInInspector] public Vector2 localVel;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentGrip;
    private float speedRatio;
	private bool drifting;
    private float lastRotation;
    private float currentBodyRotation;


	void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
        rb.angularDrag = angularDrag;
        currentGrip = tyreGrip;
    }


    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Move();
        Steer();

        drifting = Input.GetKey(driftKey);
        foreach (var driftTrail in driftTrails)
            driftTrail.emitting = drifting;

        currentGrip += drifting ? -loseGripMult : gainGripMult;
        currentGrip = Mathf.Clamp(currentGrip, -driftGrip, tyreGrip);

        UpdateBodyRotationValue();

		if (drifting)
            body.transform.localRotation = Quaternion.Euler(0, 0, currentBodyRotation);
        else
            body.transform.localRotation = Quaternion.identity;
	}

    private void Move()
    {
        rb.AddForce(transform.up * moveInput * acceleration);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        localVel = transform.InverseTransformDirection(rb.velocity);
        speedRatio = localVel.y / maxSpeed;
    }

    private void Steer()
    {
        float rotationAmount = -Mathf.Sign(localVel.y) * turnInput * turnSpeed * speedRatio * Time.fixedDeltaTime;
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

    void UpdateBodyRotationValue()
    {
        if (Mathf.Abs(localVel.y) <= 0.6f) return;

        if (!drifting || turnInput == 0)
        {
            if (Mathf.Abs(currentBodyRotation) <= 1f)
                currentBodyRotation = 0f;

			else if (currentBodyRotation != 0)
                currentBodyRotation -= Time.deltaTime * bodyRotaPerSec * (currentBodyRotation > 0 ? 1 : -1);

            return;
        }
        currentBodyRotation -= Time.deltaTime * bodyRotaPerSec * (turnInput > 0 ? 1 : -1);
		currentBodyRotation = Mathf.Clamp(currentBodyRotation, -driftBodyRotation, driftBodyRotation);
	}
}
