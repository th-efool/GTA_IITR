using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 200f;

    public Transform playerBody; // Reference to the Player object for left-right rotation
    public Transform playerCamera; // Reference to the Camera for up-down rotation

    float xRotation = 0f; // Keeps track of up-down rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the center
    }

    void Update()
    {
        // Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply up-down rotation to the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply left-right rotation to the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
