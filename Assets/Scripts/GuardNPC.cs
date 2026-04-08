using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardNPC : MonoBehaviour
{
    public GameObject[] patrolPoints;
    private NavMeshAgent agent;
    public bool guardAngry;
    private GameObject player;
    private int currentPatrolIndex = 0;
    public Animator guardAnim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }

        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
            return;
        }

        guardAngry = false;
        Patrol();
    }

    private void Patrol()
    {
        
        if (patrolPoints.Length == 0) return;
        agent.speed = 3f;
        guardAnim.SetFloat("Speed", agent.speed);
        agent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
    }

    private void Update()
    {
        if (player == null) return;

        if (guardAngry)
        {
            agent.SetDestination(player.transform.position);
            guardAnim.SetFloat("Speed",6f);
            agent.speed = 6f;
        }
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            Patrol();
        }
    }
}
