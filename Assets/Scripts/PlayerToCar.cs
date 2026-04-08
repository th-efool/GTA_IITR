using UnityEngine;

public class PlayerToCar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactionText;
    public GameObject movingCar;
    //public GameObject stationaryCar;
    public GameObject Player;
    //private GameObject tempMovingCar;
    private CarController carController;
    public CarToPlayer carToPlayerObj;
    public GameObject carCamera;

    private void Start()
    {
        if (carToPlayerObj.isInCar)
        {
            carController.enabled = true;
            carCamera.SetActive(true);
        }
        else 
        { 
            carController.enabled = false;
            carCamera.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                carIsReadyToDrive();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(false);
        }
    }

    private void carIsReadyToDrive()
    {
        carController.enabled = true;
        Player.transform.SetParent(movingCar.transform);
        Player.SetActive(false);
        carToPlayerObj.isInCar = true;
        carToPlayerObj.IsInCar();
    }
    private void carIsReadyToDismount()
    {
        carController.enabled = false;
        Player.transform.SetParent(null);
        Player.SetActive(true);
        carToPlayerObj.isInCar = false;
        carToPlayerObj.IsInCar();
    }
}
