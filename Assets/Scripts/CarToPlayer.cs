using UnityEngine;

public class CarToPlayer : MonoBehaviour
{
    //public GameObject stationaryCar;
    //public GameObject movingCar;
    public GameObject player;
    public bool isInCar = false;
    public GameObject dismountText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dismountText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCar)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                isInCar = false;
                player.transform.SetParent(null);

                IsInCar();
                //player.transform.SetParent()
            }
        }
    }

    public void IsInCar()
    {
        dismountText.SetActive(isInCar);
    }
}
