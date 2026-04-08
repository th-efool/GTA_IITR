using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    [Header("Punch Settings")]
    public int damageAmount = 10;
    private bool hasHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        Debug.Log("🔸 Hitbox collided with: " + other.name);

        // Traverse up the hierarchy to find the NPC root
        Transform current = other.transform;

        while (current != null)
        {
            if (current.CompareTag("NPC"))
            {
                NPCHealth npcHealth = current.GetComponent<NPCHealth>();
                if (npcHealth != null)
                {
                    Debug.Log("✅ Applying damage to NPC root: " + current.name);
                    npcHealth.TakeDamage(damageAmount);
                    hasHit = true;
                }
                break; // No need to go further up
            }

            current = current.parent;
        }
    }

    // Called every punch to allow new hit
    public void ResetHit()
    {
        hasHit = false;
    }
}
