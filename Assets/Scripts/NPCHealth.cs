using UnityEngine;
public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100;
    
    private int currentHealth;
    public bool isDead = false;
    public GameObject tutPrefab;
    public Transform tutSpawnPoint;
    public GameObject NerdNPC;
    public NerdNPCMovements movements;
    public NPCDialogue npcDialogues;
    public AudioSource punchSound;

    public FirstMission firstMission;
    public CarInteractionManager carInteractionManager; 
    public Switching switcher;
    void Start()
    {
        currentHealth = maxHealth;
        switcher = GameObject.FindAnyObjectByType<Switching>();
        npcDialogues = GetComponent<NPCDialogue>();
        //carCollider.enabled = false;
        firstMission = GameObject.FindAnyObjectByType<FirstMission>();
    }
    
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        punchSound.Play();
        Debug.Log("NPC took damage, health now: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        firstMission.MissionDisplay();
        Vector3 npcposition = NerdNPC.transform.position;
        Quaternion npcrotation = NerdNPC.transform.rotation;
        carInteractionManager.canInteractWithCar = true;
        isDead = true;
        
        Debug.Log("NPC has died.");

        movements.StopRunning();
        npcDialogues.enabled = false;
        
        if (firstMission != null)
        {
            // Ensure timer stops when NPC dies
            MissionTimer missionTimer = firstMission.GetComponent<MissionTimer>();
            //if (missionTimer != null)
            //{
            //    missionTimer.StopTimer();
            //}
        }

        
        if (tutPrefab != null)
        {
            Vector3 spawnPos = tutSpawnPoint ? tutSpawnPoint.position : transform.position + Vector3.up * 0.5f;
            Instantiate(tutPrefab, spawnPos, Quaternion.identity);
        }

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

        switcher.SwitchFppToTpp();
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in allColliders)
        {
            col.enabled = false;
        }



    }
}