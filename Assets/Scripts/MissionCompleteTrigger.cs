using UnityEngine;

public class MissionCompleteTrigger : MonoBehaviour
{
    public GameObject missionCompleteUI;
    private FirstMission firstMission;

    void Start()
    {
        firstMission = GameObject.FindAnyObjectByType<FirstMission>();

        if (missionCompleteUI != null)
            missionCompleteUI.SetActive(false); 
        else
            Debug.LogWarning("MissionComplete UI is not assigned!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactor"))
        {
            if (firstMission != null && firstMission.tutAcquired)
            {
                if (missionCompleteUI != null)
                {
                    missionCompleteUI.SetActive(false); // Reset
                    missionCompleteUI.SetActive(true);
                    Debug.Log("Mission complete UI shown!");
                }
            }
            else
            {
                Debug.Log("Tutorial not yet acquired.");
            }
        }
    }
}
