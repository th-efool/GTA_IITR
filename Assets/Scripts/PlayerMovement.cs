using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isCursorVisible = false;
    public CharacterController controller;

    public float walkSpeed = 5f; // Normal walking speed
    public float runSpeed = 10f; // Speed while running
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    private Animator animator;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        // Initialize animator and ensure Idle is the starting animation
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    void Update()
    {
        // Ground check using Physics.CheckSphere
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // Slightly negative to maintain consistent grounding
        }

        // Player movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Check for running (Shift key)
        float currentSpeed = walkSpeed; // Default walking speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // Running when Shift is held
        {
            currentSpeed = runSpeed; // Increase speed when running
            animator.SetBool("isRunning", true); // Set running animation
        }
        else
        {
            animator.SetBool("isRunning", false); // Set idle or walking animation
        }

        // Move the character with the calculated speed
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);

        // Update animator's IsMoving parameter based on player input
        if (move.magnitude > 0.2f)
        {
            animator.SetBool("isMoving", true);
            
        }
        else
        {
            animator.SetBool("isMoving", false);
            
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCursor();
        }

        // Debugging information
        //Debug.Log($"IsGrounded: {isGrounded}, Velocity: {velocity.y}, isMoving: {animator.GetBool("isMoving")}, isRunning: {animator.GetBool("isRunning")}");
    }
    void ToggleCursor()
    {
        isCursorVisible = !isCursorVisible;

        if (isCursorVisible)
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
            Cursor.visible = true;                // Makes the cursor visible
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor in the center of the screen
            Cursor.visible = false;                  // Hides the cursor
        }
    }
}
