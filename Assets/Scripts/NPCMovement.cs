using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float moveRadius = 10f;
    public float idleTime = 3f;
    private Animator npcAnim;
    private float originalSpeed;

    private void Start()
    {
        npcAnim = GetComponent<Animator>();
        originalSpeed = agent.speed;
        StartCoroutine(RandomMovementLoop());
    }

    private IEnumerator RandomMovementLoop()
    {
        while (true)
        {
            // Move to a random point
            Vector3 randomPoint = GetRandomPoint(transform.position, moveRadius);
            agent.SetDestination(randomPoint);

            if (npcAnim != null)
                npcAnim.SetFloat("MoveSpeed", agent.speed);

            // Wait until NPC reaches the point
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Stay idle for some time
            if (npcAnim != null)
                npcAnim.SetFloat("MoveSpeed", 0f);

            yield return new WaitForSeconds(idleTime);
        }
    }

    private Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection.y = 0; // Ensure movement remains on the NavMesh plane
        randomDirection += center;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }

    private void OnTriggerStay(Collider other)  // Logic for when interactor component comes close
    {
        if (other.CompareTag("Interactor"))
        {
            agent.isStopped = true;
            agent.speed = 0f;
            if (npcAnim != null)
                npcAnim.SetFloat("MoveSpeed", 0f);
            transform.LookAt(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactor"))
        {
            agent.isStopped = false;
            agent.speed = originalSpeed;
            if (npcAnim != null)
                npcAnim.SetFloat("MoveSpeed", originalSpeed);
        }
    }
}
