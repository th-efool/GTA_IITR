using System.Collections;
using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueBoxText;
    public string[] dialogues;
    public float dialogueTime = 3f;
    public GameObject interactionText;
    public GameObject DialogueBox;
    private int dialogueIndex;
    private bool isDialogueActive;
    public NerdNPCMovements movements;
    public FirstMission firstMission;

    public Switching switcher;

    private void Start()
    {
        movements = GameObject.FindAnyObjectByType<NerdNPCMovements>();
        switcher = GameObject.FindAnyObjectByType<Switching>();
        firstMission = GameObject.FindAnyObjectByType<FirstMission>();
        if (interactionText != null)
            interactionText.SetActive(false);

        if (dialogueBoxText != null)
            dialogueBoxText.text = "";
    }

    private IEnumerator PlayDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        DialogueBox.SetActive(true);
        while (dialogueIndex < dialogues.Length)
        {
            if (dialogueBoxText != null)
                dialogueBoxText.text = dialogues[dialogueIndex];

            if (dialogues[dialogueIndex] == "GIVE IT TO ME!!!")
            {
                if (firstMission != null)
                {
                    firstMission.MissionDisplay();
                }

                if (switcher != null)
                    switcher.SwitchTppToFpp();

                if (movements != null)
                    movements.Run();

                MissionTimer timer = GameObject.FindAnyObjectByType<MissionTimer>();
                if (timer != null)
                {
                    timer.totalTime = 300f;
                    timer.StartTimer();
                }
            }


            yield return new WaitForSeconds(dialogueTime);
            dialogueIndex++;
        }

        if (dialogueBoxText != null)
            dialogueBoxText.text = "";

        isDialogueActive = false;
        DialogueBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactor"))
        {
            if (interactionText != null)
                interactionText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactor") && Input.GetKeyDown(KeyCode.E) && !isDialogueActive)
        {
            StartCoroutine(PlayDialogue());

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactor"))
        {
            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }
}
