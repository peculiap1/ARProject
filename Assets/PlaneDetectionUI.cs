using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionUI : MonoBehaviour
{
    public GameObject scanningUI; // The UI panel displayed during scanning
    private ARPlaneManager planeManager; // Manager for AR plane detection

    void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>(); // Initialize the plane manager

        // Ensure the scanning UI is visible at the start
        scanningUI.SetActive(true);
    }

    void Update()
    {
        // Check if at least one AR-plane has been detected
        if (planeManager.trackables.count > 0)
        {
            // Hide the scanning UI when a plane is detected
            scanningUI.SetActive(false);
        }
        else
        {
            // Show the scanning UI as long as no planes are detected
            scanningUI.SetActive(true);
        }
    }
}