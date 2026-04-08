// // using System;
// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class CarController : MonoBehaviour
// // {
// //     private float horizontalInput, verticalInput;
// //     private float currentSteerAngle, currentbreakForce;
// //     private bool isBreaking;

// //     // Settings
// //     [SerializeField] private float motorForce, breakForce, maxSteerAngle;

// //     // Wheel Colliders
// //     [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
// //     [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

// //     // Wheels
// //     [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
// //     [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

// //     private void FixedUpdate() {
// //         GetInput();
// //         HandleMotor();
// //         HandleSteering();
// //         UpdateWheels();
// //     }

// //     private void GetInput() {
// //         // Steering Input
// //         horizontalInput = Input.GetAxis("Horizontal");

// //         // Acceleration Input
// //         verticalInput = Input.GetAxis("Vertical");

// //         // Breaking Input
// //         isBreaking = Input.GetKey(KeyCode.Space);
// //     }

// //     private void HandleMotor() {
// //         frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
// //         frontRightWheelCollider.motorTorque = verticalInput * motorForce;
// //         currentbreakForce = isBreaking ? breakForce : 0f;
// //         ApplyBreaking();
// //     }

// //     private void ApplyBreaking() {
// //         frontRightWheelCollider.brakeTorque = currentbreakForce;
// //         frontLeftWheelCollider.brakeTorque = currentbreakForce;
// //         rearLeftWheelCollider.brakeTorque = currentbreakForce;
// //         rearRightWheelCollider.brakeTorque = currentbreakForce;
// //     }

// //     private void HandleSteering() {
// //         currentSteerAngle = maxSteerAngle * horizontalInput;
// //         frontLeftWheelCollider.steerAngle = currentSteerAngle;
// //         frontRightWheelCollider.steerAngle = currentSteerAngle;
// //     }

// //     private void UpdateWheels() {
// //         UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
// //         UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
// //         UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
// //         UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
// //     }

// //     private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
// //         Vector3 pos;
// //         Quaternion rot; 
// //         wheelCollider.GetWorldPose(out pos, out rot);
// //         wheelTransform.rotation = rot;
// //         wheelTransform.position = pos;
// //     }
// // }
using UnityEngine;

public class BasicCarController : MonoBehaviour {
    public float speed = 10f;               // Movement speed
    public float turnSpeed = 120f;          // Turn rate
    public float brakeStrength = 10f;       // Braking strength
    public float driftAmount = 0.5f;        // Drift side-slip

    private float currentSpeed = 0f;

    void Update() {
        float gas = Input.GetAxis("Vertical");     // W/S
        float steer = Input.GetAxis("Horizontal"); // A/D
        bool isBraking = Input.GetKey(KeyCode.Space);

        // === ACCELERATION & BRAKING ===
        if (isBraking) {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * brakeStrength);
        } else {
            float targetSpeed = gas * speed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
        }

        // === MOVEMENT ===
        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        // === TURNING (ONLY WHILE MOVING) ===
        if (Mathf.Abs(currentSpeed) > 0.1f && Mathf.Abs(steer) > 0.01f) {
            float turnAmount = steer * turnSpeed * Time.deltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(0f, turnAmount, 0f);
        }

        // === DRIFT (ONLY WHILE MOVING AND TURNING) ===
        if (!isBraking && Mathf.Abs(currentSpeed) > 1f && Mathf.Abs(steer) > 0.1f) {
            Vector3 drift = transform.right * steer * driftAmount;
            transform.position += drift * Time.deltaTime;
        }
    }
}
