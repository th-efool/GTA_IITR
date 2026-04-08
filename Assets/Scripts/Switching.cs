using UnityEngine;

public class Switching : MonoBehaviour
{
    public GameObject tpp;           // TPP GameObject in the scene (always present)
    public GameObject fppPrefab;     // FPP prefab from Assets (not in scene)
    public GameObject playerArmature;
    private GameObject tempFPP;      // Runtime instance of the FPP prefab
    private bool isFpp = false;

    void Update()
    {
       
    }

    public void SwitchTppToFpp()
    {
        // Instantiate FPP prefab at the same position/rotation as TPP
        tempFPP = Instantiate(fppPrefab, playerArmature.transform.position, playerArmature.transform.rotation);
        tpp.transform.SetParent(tempFPP.transform);
        // Optional: You can adjust position/rotation here if needed

        // Hide TPP model
        tpp.SetActive(false);
    }

    public void SwitchFppToTpp()
    {
        // Show TPP model again
        tpp.SetActive(true);
        tpp.transform.SetParent(null);
        // Destroy the instantiated FPP instance
        if (tempFPP != null)
        {
            Destroy(tempFPP);
        }
    }
}
