using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionUI : MonoBehaviour
{
    public GameObject scanningUI; // Het UI-paneel dat wordt weergegeven tijdens scanning
    private ARPlaneManager planeManager;

    void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();

        // Zorg ervoor dat de UI zichtbaar is bij het starten
        scanningUI.SetActive(true);
    }

    void Update()
    {
        // Controleer of er ten minste één AR-plane is gedetecteerd
        if (planeManager.trackables.count > 0)
        {
            // Verberg de scanning UI
            scanningUI.SetActive(false);
        }
        else
        {
            // Toon de scanning UI zolang er geen planes zijn gedetecteerd
            scanningUI.SetActive(true);
        }
    }
}
