using System.Collections;
using UnityEngine;

public class FirstMissionTrigger : MonoBehaviour
{
    public float MissionStartTime=15f;
    private MissionTimer missionTimer;
    private FirstMission firstMission;
    public GameObject nerdNPC;
    private void Start()
    {
        missionTimer = GameObject.FindAnyObjectByType<MissionTimer>();
        nerdNPC.SetActive(false);
        firstMission = GameObject.FindAnyObjectByType<FirstMission>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Interactor") && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartMission());
        }
    }

    private IEnumerator StartMission()
    {
        yield return new WaitForSeconds(MissionStartTime);
        firstMission.MissionDisplay();
        nerdNPC.SetActive(true);
        missionTimer.totalTime = 300f;
        missionTimer.StartTimer();
    }
}
