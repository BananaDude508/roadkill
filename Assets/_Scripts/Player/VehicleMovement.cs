using UnityEngine;
using static PlayerStats;

public class VehicleMovement : MonoBehaviour
{
    public TrailRenderer[] driftTrails;
    public Transform body;

    [HideInInspector] public Vector2 localVel;
    [HideInInspector] public float speedRatio;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentGrip;
	private bool drifting;
    private float lastRotation;
    private float currentBodyRotation;

    public Rigidbody2D rigidbody2d
    {
        get { return rb; }
    }


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
        if (drifting)
            currentBodyRotation = CustomFunctions.SmoothLerp(currentBodyRotation, vehicleStats.driftBodyRotation * speedRatio * -turnInput, 0.1f);
        else
            currentBodyRotation = CustomFunctions.SmoothLerp(currentBodyRotation, 0, 0.1f);

        body.transform.localRotation = Quaternion.Euler(0, 0, currentBodyRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BasicEnemyController enemy = collision.gameObject.GetComponent<BasicEnemyController>();
            if (enemy == null) return;
            if (speedRatio <= 0.25) return;
            playerController.HurtEnemy(enemy, drifting, speedRatio);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BasicEnemyController enemy = collision.gameObject.GetComponent<BasicEnemyController>();
            if (enemy == null) return;
            if (speedRatio <= 0.5) return;
            if (!drifting) return;
            playerController.HurtEnemy(enemy, drifting, speedRatio);
        }
    }
}