using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTextLooking : MonoBehaviour
{
    public GameObject interactionTextCanvas;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 direction = cameraTransform.position - interactionTextCanvas.transform.position;
        direction.y = 0; // Keep only horizontal rotation
        interactionTextCanvas.transform.rotation = Quaternion.LookRotation(-direction);
    }
}
