using UnityEngine;
using static UnityEngine.Rendering.BoolParameter;

public class MissionEndTrigger : MonoBehaviour
{
    public GameObject missionCompleteUI;  
    public AudioSource missionCompleteSound;
    public float displayTime = 11f;
    //public int carLayer = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name + " | Layer: " + LayerMask.LayerToName(other.gameObject.layer));

        if (other.CompareTag("Car"))
        {
            missionCompleteUI.SetActive(true);

            if (missionCompleteSound != null)
                missionCompleteSound.Play();

            Invoke("HideUI", displayTime);
        }
    }

    private void HideUI()
    {
        missionCompleteUI.SetActive(false);
        gameObject.SetActive(false);
    }
}
