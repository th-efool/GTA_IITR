using TMPro;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public float totalTime = 300f; // Total time in seconds
    public TextMeshProUGUI timerText;  // Timer text object
    public GameObject timerTextObj;    // Optional panel reference

    private float currentTime;
    private bool timerRunning = false;
    private bool hasStarted = false; // Prevent multiple starts

    public GameObject wastedText;
    public AudioSource wastedSound;

    void Start()
    {
        wastedText.SetActive(false);
        currentTime = totalTime;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("timerText is NULL! Assign the TextMeshProUGUI object in the Inspector.");
        }
    }

    void Update()
    {
        if (timerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            UpdateTimerDisplay();
        }

        if (timerRunning && currentTime < 0.01f)
        {
            wastedText.SetActive(true);
            wastedSound.Play();
            StopTimer();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log("Updating Timer: " + timerText.text);
    }

    public void StopTimer()
    {
        timerRunning = false;
        hasStarted = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (timerTextObj != null)
            timerTextObj.SetActive(false);
    }

    public void StartTimer()
    {
        if (hasStarted)
        {
            Debug.LogWarning("StartTimer() called but timer has already started!");
            return;
        }
        timerText.gameObject.SetActive(true);
        timerTextObj.SetActive(true);
        hasStarted = true;
        currentTime = totalTime;
        timerRunning = true;

        //Debug.Log("StartTimer() called!");

        //if (timerText != null)
        //{
        //    timerText.gameObject.SetActive(true);
        //    timerText.text = "Starting Timer...";
        //    Debug.Log("Timer text updated to 'Starting Timer...'");
        //}
        //else
        //{
        //    Debug.LogWarning("timerText is NULL! Assign the TextMeshProUGUI object in the Inspector.");
        //}
        //Debug.Log("Timer started on: " + gameObject.name);

    }
}

