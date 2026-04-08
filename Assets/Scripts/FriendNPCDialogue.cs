using System.Collections;
using TMPro;
using UnityEngine;

public class FriendNPCDialogue : MonoBehaviour
{
    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueBoxText;
    public string[] dialogues;
    public float dialogueTime = 3f;
    public GameObject interactionText;

    private int dialogueIndex;
    private bool isDialogueActive;
    private bool dialogueTriggered = false;

    public FirstMission firstMission;
    public MissionTimer timer;

    private void Start()
    {
        firstMission = GameObject.FindAnyObjectByType<FirstMission>();

        if (interactionText != null)
            interactionText.SetActive(false);

        if (dialogueBoxText != null)
            dialogueBoxText.text = "";
    }

    private IEnumerator PlayDialogue()
    {
        Debug.Log("PlayDialogue started");

        isDialogueActive = true;
        dialogueIndex = 0;
        DialogueBox.SetActive(true);

        while (dialogueIndex < dialogues.Length)
        {
            dialogueBoxText.text = dialogues[dialogueIndex];
            yield return new WaitForSeconds(dialogueTime);
            dialogueIndex++;
        }

        dialogueBoxText.text = "";
        DialogueBox.SetActive(false);
        isDialogueActive = false;
        dialogueTriggered = false;

        // Now trigger mission display
        if (firstMission != null)
        {
            firstMission.MissionDisplay();
            timer.StartTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactor") && interactionText != null)
            interactionText.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactor") && Input.GetKeyDown(KeyCode.E) && !dialogueTriggered)
        {
            dialogueTriggered = true;
            StartCoroutine(PlayDialogue());

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactor") && interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }
}
