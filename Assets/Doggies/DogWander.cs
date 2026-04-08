using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class DogWander : MonoBehaviour
{
    public float wanderRadius = 25f;
    public float waitTime = 6f;

    private NavMeshAgent agent;
    private Animator animator;

    private bool isWaiting = false;
    private bool isSitting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveToNewPoint();
    }

    void Update()
    {
        // Check if destination is reached and no pending path
        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndMove());
        }

        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        if (animator == null) return;

        bool isMoving = agent.velocity.sqrMagnitude > 0.05f && !isWaiting;

        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isSitting", isSitting && !isMoving);

        // Set animator speed parameter for blend trees or animation control
        animator.SetFloat("Speed", isMoving ? agent.speed : 0f);
    }

    IEnumerator WaitAndMove()
    {
        isWaiting = true;

        // Random chance to sit while waiting
        isSitting = Random.value > 0.5f;

        yield return new WaitForSeconds(waitTime);

        isSitting = false;
        MoveToNewPoint();
        isWaiting = false;
    }

    void MoveToNewPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
