using UnityEngine;

public class CarRecoveryManager : MonoBehaviour
{
    public Rigidbody rb;
    public float flipDetectionTime = 3f;         // Time before reset if upside down
    public float minSpeedThreshold = 0.1f;       // Considered stuck if speed is too low
    public float flipAngleThreshold = 0.5f;      // Dot product threshold to detect upside down

    private float flipTimer = 0f;
    private bool isFlipping = false;

    private void Update()
    {
        CheckForFlip();
    }

    void CheckForFlip()
    {
        float speed = rb.linearVelocity.magnitude;

        // Dot product of up direction and world up; if < threshold, it's upside down
        bool isUpsideDown = Vector3.Dot(transform.up, Vector3.up) < flipAngleThreshold;
        bool isStuck = speed < minSpeedThreshold;

        if (isUpsideDown && isStuck)
        {
            flipTimer += Time.deltaTime;

            if (flipTimer >= flipDetectionTime && !isFlipping)
            {
                isFlipping = true;
                StartCoroutine(RecoverCar());
            }
        }
        else
        {
            flipTimer = 0f;
        }
    }

    System.Collections.IEnumerator RecoverCar()
    {
 

        yield return new WaitForSeconds(0.5f); // slight delay for sound or effect

        // Lift and reset orientation
        Vector3 position = transform.position;
        position.y += 2f; // lift slightly to avoid ground clipping
        transform.position = position;

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f); // upright, preserve yaw

        // Zero out physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isFlipping = false;
    }
}
