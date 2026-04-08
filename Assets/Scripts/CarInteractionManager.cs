using UnityEngine;

public class CarInteractionManager : MonoBehaviour
{
    [Header("References")]
    public GameObject interactionText;     // Text shown when near car
    public GameObject dismountText;        // Text shown when inside car
    public GameObject movingCar;           // The car GameObject
    public GameObject player;              // The player GameObject
    public GameObject carCamera;           // The car camera

    private CarController carController;
    private CarSlowdownOnExit carSlowdown;
    private bool isInCar = false;
    public bool canInteractWithCar = false;

    private void Start()
    {
        if (movingCar != null)
        {
            carController = movingCar.GetComponent<CarController>();
            carSlowdown = movingCar.GetComponent<CarSlowdownOnExit>();
        }

        if (interactionText != null) interactionText.SetActive(false);
        if (dismountText != null) dismountText.SetActive(false);
        if (carCamera != null) carCamera.SetActive(false);

        if (isInCar) EnterCar();
        else ExitCar();
    }

    private void Update()
    {
        if (isInCar && Input.GetKeyDown(KeyCode.F) && canInteractWithCar)
        {
            ExitCar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInCar && other.CompareTag("Interactor"))
        {
            if (interactionText != null) interactionText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isInCar && other.CompareTag("Interactor") && Input.GetKeyDown(KeyCode.E) && canInteractWithCar)
        {
            EnterCar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isInCar && other.CompareTag("Interactor"))
        {
            if (interactionText != null) interactionText.SetActive(false);
        }
    }

    private void EnterCar()
    {
        Debug.Log("Entered car.");
        isInCar = true;

        if (carController != null) carController.enabled = true;
        if (carCamera != null) carCamera.SetActive(true);

        if (player != null)
        {
            player.transform.SetParent(movingCar.transform);
            player.SetActive(false);
        }

        if (interactionText != null) interactionText.SetActive(false);
        if (dismountText != null) dismountText.SetActive(true);
    }

    private void ExitCar()
    {
        Debug.Log("Exited car.");
        isInCar = false;

        if (carController != null) carController.enabled = false;
        if (carSlowdown != null) carSlowdown.TriggerStop(); // Gradual stop

        if (carCamera != null) carCamera.SetActive(false);

        if (player != null)
        {
            player.transform.SetParent(null);
            player.SetActive(true);
        }

        if (interactionText != null) interactionText.SetActive(false);
        if (dismountText != null) dismountText.SetActive(false);
    }
}
