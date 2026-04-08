using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarSlowdownOnExit : MonoBehaviour
{
    public float brakeForce = 6000f;
    public float stopThreshold = 0.1f;

    private CarController carController;
    private Rigidbody rb;
    private bool shouldStopCar = false;

    private WheelCollider rearLeft, rearRight, frontLeft, frontRight;

    private void Start()
    {
        carController = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();

        // Grab wheel colliders via CarController
        var auto = GetComponent<AutoAssignWheelReferences>();
        rearLeft = auto.rearLeftCollider;
        rearRight = auto.rearRightCollider;
        frontLeft = auto.frontLeftCollider;
        frontRight = auto.frontRightCollider;
    }

    private void FixedUpdate()
    {
        if (shouldStopCar)
        {
            ApplyGradualBraking();

            // Once nearly stopped, release brakes
            if (rb.linearVelocity.magnitude < stopThreshold)
            {
                shouldStopCar = false;
                ReleaseBrakes();
            }
        }
    }

    public void TriggerStop()
    {
        shouldStopCar = true;
    }

    private void ApplyGradualBraking()
    {
        rearLeft.brakeTorque = brakeForce;
        rearRight.brakeTorque = brakeForce;
        frontLeft.brakeTorque = brakeForce;
        frontRight.brakeTorque = brakeForce;
    }

    private void ReleaseBrakes()
    {
        rearLeft.brakeTorque = 0f;
        rearRight.brakeTorque = 0f;
        frontLeft.brakeTorque = 0f;
        frontRight.brakeTorque = 0f;
    }
}
