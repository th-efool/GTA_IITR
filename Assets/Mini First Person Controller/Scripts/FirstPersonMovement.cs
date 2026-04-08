using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public bool canRun = true;
    public bool isWalking { get; private set; }
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public GameObject punchHitbox;
    public float punchDuration = 0.3f;
   
    public KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody rb;
    private Animator animator;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Animator not found on child object!");
        }
    }

    void FixedUpdate()
    {
        IsRunning = canRun && Input.GetKey(runningKey);
        float targetSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
        {
            targetSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetSpeed, Input.GetAxis("Vertical") * targetSpeed);
        rb.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.y);

        isWalking = targetVelocity.magnitude > 0;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", IsRunning);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("punch");
            StartCoroutine(EnablePunchHitbox());
        }
    }

    IEnumerator EnablePunchHitbox()
    {
        punchHitbox.SetActive(true);
        punchHitbox.GetComponent<PunchHitbox>().ResetHit();

        yield return new WaitForSeconds(punchDuration);

        punchHitbox.SetActive(false);
    }
}
