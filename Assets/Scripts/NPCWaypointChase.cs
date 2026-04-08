using UnityEngine;

public class NPCWaypointChase : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    public float reachDistance = 0.5f;
    public Animator animator;

    private int currentWaypoint = 0;
    private bool isChasing = false;

    void Update()
    {
        if (isChasing && waypoints.Length > 0)
        {
            Transform target = waypoints[currentWaypoint];
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
            }

            if (Vector3.Distance(transform.position, target.position) <= reachDistance)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
        }

        if (animator != null)
        {
            animator.SetBool("isRunning", isChasing);
        }
    }

    public void StartChase()
    {
        Debug.Log("💥 StartChase() called");
        isChasing = true;
        if (animator != null)
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void StopChase()
    {
        isChasing = false;
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
        }
    }
}
