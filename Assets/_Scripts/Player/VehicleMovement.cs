using DG.Tweening;
using System.Collections;
using UnityEngine;
using Cinemachine;
using static PlayerStats;

public class VehicleMovement : MonoBehaviour
{
    public TrailRenderer[] driftTrails;
    public Transform body;

    [HideInInspector] public Vector3 localVel;
    [HideInInspector] public float speedRatio;

    public float driftTimeout = 3f;
    public float spinTimeout = 3f;
    public float spinoutDegPerSec = 1080;
    [HideInInspector] public bool spinningOut;
    private float driftingTime;
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

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    public float ssAmpGain;
    public float ssFreqGain;

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

        perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
            spinoutTime = spinTimeout;
            spinningOut = true;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero);

            foreach (var hit in hits)
                if (hit.collider.CompareTag("Enemy"))
                    OnHitEnemy(hit.collider.gameObject);

            StartCoroutine(CameraShake(ssAmpGain, ssFreqGain, spinTimeout));
        }

        if (spinningOut)
        {
            spinoutTime -= Time.deltaTime;
            spinningOut = spinoutTime >= 0;
            body.transform.localEulerAngles += Time.deltaTime * spinoutDegPerSec * Vector3.forward;
            transform.localPosition += 0.1f * Mathf.Sin(2 * Mathf.PI * spinoutTime) * Vector3.right;
            transform.localPosition += localVel * Time.deltaTime * (spinoutTime / spinTimeout);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            OnHitEnemy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            OnHitEnemy(collision.gameObject);
    }

    private void OnHitEnemy(GameObject enemyObject)
    {
        if (speedRatio < 0.8) return;

        if (spinningOut)
        {
            Rigidbody2D enemyRB = enemyObject.GetComponent<Rigidbody2D>();
            if (enemyRB == null) return;
            Vector2 forceDir = (transform.position - enemyObject.transform.position).normalized;
            float explodeForce = Random.Range(800f, 1200f);
            enemyRB.AddForce(-forceDir * explodeForce);
            return;
        }

        BasicEnemyController enemyC = enemyObject.GetComponent<BasicEnemyController>();
        if (enemyC == null) return;
        playerController.HurtEnemy(enemyC, drifting, speedRatio);
    }

    private IEnumerator CameraShake(float amplitudeGain, float frequencyGain, float shakeTime)
    {
        SetCameraNoise(amplitudeGain, frequencyGain);
        yield return new WaitForSeconds(shakeTime);
        SetCameraNoise(0, 0);
        virtualCamera.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.OutSine);
        virtualCamera.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.OutSine);
    }

    private void SetCameraNoise(float amplitudeGain, float frequencyGain)
    {
        print("Setting camera noise");
        perlinNoise.m_AmplitudeGain = amplitudeGain;
        perlinNoise.m_FrequencyGain = frequencyGain;
    }
}