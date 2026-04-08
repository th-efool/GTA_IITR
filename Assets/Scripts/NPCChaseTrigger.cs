using UnityEngine;

public class NPCChaseTrigger : MonoBehaviour
{
    public Animator npcAnimator; // Drag NPC's Animator here in Inspector
    public string runTriggerName = "isRunning"; // Must match the parameter in Animator
    public string playerTag = "Player"; // Ensure your player has this tag
    public NPCWaypointChase npcChase;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player entered NPC trigger zone — starting chase");
            npcAnimator.SetBool("isRunning", true); // Just in case
            npcChase.StartChase();
        }
    }
}

