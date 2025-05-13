using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Cache the main camera
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Make the UI element face the camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
