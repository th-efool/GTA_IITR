using System.Collections;
using TMPro;
using UnityEngine;

public class FirstMission : MonoBehaviour
{
    public bool tutAcquired = false;
    public GameObject missionBox;
    public TextMeshProUGUI missionText;

    public string[] missionDescription;
    private int CurrentIndex = 0;
    [HideInInspector] public bool isDisplaying = false;

    private bool hasTriggeredFromTut = false; // to prevent repeat trigger

    void Start()
    {
        CurrentIndex = 0;
        hasTriggeredFromTut = false;

        if (missionBox != null)
            missionBox.SetActive(false);
    }

    void Update()
    {
        
        if (tutAcquired && !hasTriggeredFromTut)
        {
            hasTriggeredFromTut = true;
            MissionDisplay();
            Debug.Log("Mission is supposed to be displayed!");
        }
    }

    public void MissionDisplay()
    {
        Debug.Log($"MissionDisplay called at index {CurrentIndex}, isDisplaying = {isDisplaying}");

        if (CurrentIndex >= missionDescription.Length || isDisplaying)
            return;

        if (missionBox != null)
        {
            missionBox.SetActive(true);
            missionText.text = missionDescription[CurrentIndex];
            StartCoroutine(MissionDisplayOff());
        }
        else
        {
            Debug.LogWarning("missionBox is null!");
        }

        CurrentIndex++;
    }

    private IEnumerator MissionDisplayOff()
    {
        isDisplaying = true;
        yield return new WaitForSeconds(5f);
        if (missionBox != null)
            missionBox.SetActive(false);
        isDisplaying = false;
    }
}
