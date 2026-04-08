using UnityEngine;

public class FriendNPC : MonoBehaviour
{
     public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactor") && Input.GetKeyDown(KeyCode.E))
        {
            
                animator.SetTrigger("Hi");
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.ResetTrigger("Hi");
    }
}
