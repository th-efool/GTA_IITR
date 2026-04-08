using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnoteWheelActiveness : MonoBehaviour
{
    [SerializeField] private KeyCode wheelKey = KeyCode.Tab;
    [SerializeField] private GameObject wheelParent;
    public StarterAssetsInputs starterAssetsInputObj;
    private void EnableWheel()
    {
        if (wheelParent != null)
        {
            wheelParent.SetActive(true);
            starterAssetsInputObj.cursorLocked = false;
            starterAssetsInputObj.cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void DisableWheel()
    {
        if (wheelParent != null)
        {
            wheelParent.SetActive(false);
            starterAssetsInputObj.cursorLocked = true;
            starterAssetsInputObj.cursorInputForLook = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (Input.GetKey(wheelKey))
        {
            //enable wheel mode
            EnableWheel();
        }
        else if (Input.GetKeyUp(wheelKey))
        {
            //disable wheel mode
            DisableWheel();
        }

    }
}
