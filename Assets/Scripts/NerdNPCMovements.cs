using UnityEngine;
using UnityEngine.AI;

public class NerdNPCMovements : MonoBehaviour
{
    public Animator nerdAnimator;
    private NavMeshAgent agent;
    public GameObject[] runToPoints;
    private int index;
    private bool isRunning = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = 0;
        nerdAnimator.SetFloat("Speed", 0f);
    }

    public void Run()
    {
        if (runToPoints.Length == 0) return;

        isRunning = true;
        agent.speed = 7f;
        nerdAnimator.SetFloat("Speed", agent.speed);
        agent.SetDestination(runToPoints[index].transform.position);
    }

    public void StopRunning()
    {
        isRunning = false;
        agent.ResetPath(); // Proper way to stop movement
        agent.speed = 0f;
        nerdAnimator.SetFloat("Speed", 0f);
        nerdAnimator.SetBool("isDead",true);
    }

    void Update()
    {
        if (!isRunning || runToPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            index = (index + 1) % runToPoints.Length;
            agent.SetDestination(runToPoints[index].transform.position);
        }
    }
}
