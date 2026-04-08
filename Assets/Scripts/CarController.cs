using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("F1 Car Settings")]
    [SerializeField] private float motorForce = 12000f;    // Increased for F1 power
    [SerializeField] private float brakeForce = 15000f;    // Increased for carbon brakes
    [SerializeField] private float maxSteerAngle = 20f;    // Adjusted for F1 steering
    [SerializeField] private Vector3 centerOfMassOffset = new Vector3(0, -0.3f, 0); // Lower center of mass
    [SerializeField] private float maxSpeed = 350f;        // Increased for F1 top speed
    [SerializeField] private float steeringSmoothSpeed = 8f; // More responsive steering
    [SerializeField] private float dragFactor = 0.995f;    // Less drag for better aerodynamics
    [SerializeField] private float downforceMultiplier = 2.5f; // Added downforce
    [SerializeField] private float maxMotorTorque = 100000f; // Newton-meters

    private Rigidbody rb;
    private WheelCollider[] wheelColliders = new WheelCollider[4];
    private Transform[] wheelMeshes = new Transform[4];
    private float horizontalInput, verticalInput;
    private float smoothHorizontalInput;
    private bool isBraking;

    private void Start()
    {
        InitializeComponents();
        SetupVehiclePhysics();
    }

    private void InitializeComponents()
    {
        var auto = GetComponent<AutoAssignWheelReferences>();
        wheelColliders[0] = auto.frontLeftCollider;
        wheelColliders[1] = auto.frontRightCollider;
        wheelColliders[2] = auto.rearLeftCollider;
        wheelColliders[3] = auto.rearRightCollider;

        wheelMeshes[0] = auto.frontLeftMesh;
        wheelMeshes[1] = auto.frontRightMesh;
        wheelMeshes[2] = auto.rearLeftMesh;
        wheelMeshes[3] = auto.rearRightMesh;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass += centerOfMassOffset;
    }

    private void SetupVehiclePhysics()
    {
        foreach (var wheel in wheelColliders)
        {
            var friction = wheel.forwardFriction;
            friction.stiffness = 4.5f;  // Increased grip
            wheel.forwardFriction = friction;

            friction = wheel.sidewaysFriction;
            friction.stiffness = 4.8f;  // Better cornering
            wheel.sidewaysFriction = friction;
        }
    }
    

    private void FixedUpdate()
    {
        
        GetInput();
        ApplyDownforce();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);

        smoothHorizontalInput = Mathf.Lerp(smoothHorizontalInput, horizontalInput, Time.fixedDeltaTime * steeringSmoothSpeed);
    }

    private void ApplyDownforce()
    {
        float speed = rb.linearVelocity.magnitude;
        float downforce = speed * speed * downforceMultiplier;
        rb.AddForce(-transform.up * downforce);
    }

    private void HandleMotor()
    {
        float speed = rb.linearVelocity.magnitude;
        Debug.Log(speed);

        float targetTorque = 0f;
        bool isReversing = verticalInput < -0.1f && Vector3.Dot(rb.linearVelocity, transform.forward) < 0.1f;

        if (verticalInput > 0 && speed < maxSpeed)
        {
            float speedFactor = 1f - (speed / maxSpeed);
            targetTorque = verticalInput * motorForce * speedFactor;
        }
        else if (isReversing)
        {
            float reverseSpeedFactor = 1f - (speed / (maxSpeed * 0.3f));
            targetTorque = verticalInput * motorForce * reverseSpeedFactor * 0.4f; // limit reverse torque
        }

        // Clamp torque to max limits
        targetTorque = Mathf.Clamp(targetTorque, -maxMotorTorque, maxMotorTorque);

        // Apply torque to rear wheels (indices 2 and 3)
        for (int i = 2; i <= 3; i++) // Rear wheels only
        {
            if (Mathf.Abs(verticalInput) < 0.01f)
            {
                wheelColliders[i].motorTorque = 0f; // No torque when idle
            }
            else if (wheelColliders[i].GetGroundHit(out WheelHit hit))
            {
                wheelColliders[i].motorTorque = targetTorque;
            }
            else
            {
                wheelColliders[i].motorTorque = 0f;
            }
        }


        // Braking logic
        float brakeTorque = 0f;

        if (isBraking)
        {
            brakeTorque = brakeForce;
        }
        else if ((verticalInput < 0 && !isReversing) || (verticalInput > 0 && isReversing))
        {
            brakeTorque = brakeForce * Mathf.Abs(verticalInput); // progressive braking
        }

        // Aerodynamic drag
        if (Mathf.Abs(verticalInput) < 0.1f && speed > 0f)
        {
            rb.linearVelocity *= dragFactor;
            brakeTorque = Mathf.Max(brakeTorque, brakeForce * 0.1f); // light resistance
        }

        // Apply braking to all wheels
        foreach (var wheel in wheelColliders)
        {
            wheel.brakeTorque = brakeTorque;
        }
    }


    private void HandleSteering()
    {
        float speedFactor = Mathf.Clamp01(1f - (rb.linearVelocity.magnitude / maxSpeed));
        float steerAngle = maxSteerAngle * speedFactor * smoothHorizontalInput;

        // Apply steering to front wheels
        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheelMeshes[i].SetPositionAndRotation(pos, rot);
        }
    }
}
