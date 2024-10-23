using DG.Tweening;
using System.Collections;
using UnityEngine;
using static PlayerStats;

public class VehicleMovement : MonoBehaviour
{
    public TrailRenderer[] driftTrails;
    public Transform body;

    [HideInInspector] public Vector2 localVel;
    [HideInInspector] public float speedRatio;

    public float driftTimeout = 3f;
    public float spinTimeout = 3f;
    private float driftingTime;
    private bool spinningOut;
    private float spinoutTime;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentGrip;
	private bool drifting;
    private float lastRotation;
    private float currentBodyRotation;
    public PlayerSoundManager soundManager;
    private bool drivingSoundActive = false;

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
        if (drifting)
            driftingTime -= Time.deltaTime;
        else
            driftingTime = driftTimeout;

        if (driftingTime <= 0)
        {
            drifting = false;
            Spinout();
            spinoutTime = spinTimeout;
            spinningOut = true;
        }

        if (spinningOut)
        {
            spinoutTime -= Time.deltaTime;
            spinningOut = spinoutTime >= 0;
            return;
        }

;

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

        if (speedRatio > 0.2 && !drivingSoundActive)
        {
            soundManager.PlaySound("drive", true);
            drivingSoundActive = true;
        }

        if (speedRatio < 0.2 && drivingSoundActive)
        {
            soundManager.PlaySound("drive", false);
            drivingSoundActive = false;
        }
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

    // Spinout, change body rotation
    private IEnumerator Spinout()
    {
        return null;
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