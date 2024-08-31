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

    [HideInInspector] public Vector2 localVel;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentGrip;
    private float speedRatio;
	private bool drifting;

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

        currentGrip += !drifting ? gainGripMult : -loseGripMult;
        currentGrip = Mathf.Clamp(currentGrip, -driftGrip, tyreGrip);
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
        rb.rotation += rotationAmount;
        rb.rotation = Mathf.Repeat(rb.rotation, 360);
        


        // Drifting and grip
        rb.AddForce(transform.right * (-localVel.x * currentGrip));
    }
}
