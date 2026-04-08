using UnityEngine;

public class SyncWithTPP : MonoBehaviour
{
    public Transform tppPlayerTransform;

    void Update()
    {
        if (tppPlayerTransform != null)
        {
            transform.position = tppPlayerTransform.position;
            transform.rotation = tppPlayerTransform.rotation;
        }
    }
}

