using UnityEngine;

public class TutPickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private FirstMission firstMissionObj;
    
    void Start()
    {
        firstMissionObj = GameObject.FindAnyObjectByType<FirstMission>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactor"))
        {
            firstMissionObj.tutAcquired = true;
            Destroy(gameObject);
            
        }
    }
}
