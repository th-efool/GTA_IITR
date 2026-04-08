using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private float angerTime;
    public float angerThresholdTime = 5f;
    private GuardNPC guardNpcObj;
    public float angerPeriod = 20f;
    private float angryTimeLeft = 0f;
    private bool shouldIncreaseAngerTime = false;
    private bool isPlayerInGrass = false; // Track if player is in grass
    [SerializeField] private GameObject BustedText;
    public AudioSource BustedSound;
    public ThirdPersonController thirdPersonController;
    private bool isBusted = false;

    void Start()
    {
        angerTime = 0;
        //guardNpcObj = FindObjectOfType<GuardNPC>(); // Assign reference
        guardNpcObj = GameObject.FindAnyObjectByType<GuardNPC>();
        BustedText.SetActive(false);
    }

    void Update()
    {
        if (guardNpcObj == null) return; // Prevent null reference errors

        // Increase anger time while in grass
        if (shouldIncreaseAngerTime)
        {
            angerTime += Time.deltaTime;
            Debug.Log("Anger Time: " + angerTime);
        }

        // If anger threshold is exceeded, NPC becomes angry
        if (angerTime > angerThresholdTime && !guardNpcObj.guardAngry)
        {
            guardNpcObj.guardAngry = true;
            angryTimeLeft = angerPeriod; // Start anger countdown
        }

        // Guard remains angry while player is in grass
        if (guardNpcObj.guardAngry)
        {
            if (!isPlayerInGrass) // Start countdown only when player leaves grass
            {
                if (angryTimeLeft > 0)
                {
                    angryTimeLeft -= Time.deltaTime;
                    Debug.Log("Angry time left: " + angryTimeLeft);
                }
                else
                {
                    guardNpcObj.guardAngry = false;
                    angerTime = 0f; // Reset anger only when completely calm
                    shouldIncreaseAngerTime = false;
                }
            }
            else
            {
                // If player is still in grass, refresh the anger time
                angryTimeLeft = angerPeriod;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass"))
        {
            isPlayerInGrass = true;
            shouldIncreaseAngerTime = true;
        }

        if(other.CompareTag("Guard") && guardNpcObj.guardAngry)
        {
            if (isBusted) return;
            BustedText.SetActive(true);
            BustedSound.Play();
            isBusted = true;
            thirdPersonController.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grass"))
        {
            isPlayerInGrass = false;
            shouldIncreaseAngerTime = false;

            if (!guardNpcObj.guardAngry)
            {
                angerTime = 0; // Reset only if the guard isn't already angry
            }
        }
    }
}
