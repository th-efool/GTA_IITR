using UnityEngine;

public class AutoAssignWheelReferences : MonoBehaviour
{
    [Header("Auto-assigned Components")]
    public WheelCollider frontLeftCollider, frontRightCollider;
    public WheelCollider rearLeftCollider, rearRightCollider;

    public Transform frontLeftMesh, frontRightMesh;
    public Transform rearLeftMesh, rearRightMesh;

    private void Awake()
    {
        rearLeftMesh       = transform.Find("RLW").transform;
        frontRightCollider = transform.Find("FRWheel").GetComponent<WheelCollider>();
        frontRightMesh     = transform.Find("FRW").transform;

        frontLeftCollider  = transform.Find("FLWheel").GetComponent<WheelCollider>();
        frontLeftMesh      = transform.Find("FLW").transform;

        rearRightCollider  = transform.Find("RRWheel").GetComponent<WheelCollider>();
        rearRightMesh      = transform.Find("RRW").transform;

        rearLeftCollider   = transform.Find("RLWheel").GetComponent<WheelCollider>();

    }
}
