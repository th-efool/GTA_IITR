using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    public Transform target;                // Car to follow
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    public float mouseSensitivity = 3f;
    public float pitchMin = -10f, pitchMax = 45f;

    public Transform altCameraPosition;     // Assign in Inspector (e.g. hood cam position)

    private float yaw = 0f;
    private float pitch = 10f;

    private bool useAltView = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        // Switch camera mode with V
        if (Input.GetKeyDown(KeyCode.V))
        {
            useAltView = !useAltView;
        }

        // Only allow freelook in main camera mode
        if (!useAltView && Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        }
        else if (!useAltView)
        {
            yaw = Mathf.LerpAngle(yaw, target.eulerAngles.y, Time.deltaTime * rotationSpeed);
        }
    }

    void LateUpdate()
    {
        if (!target) return;

        if (useAltView && altCameraPosition)
        {
            transform.position = altCameraPosition.position;
            transform.rotation = altCameraPosition.rotation;
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 desiredPosition = target.position + rotation * offset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}
