using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ModelSelectionAndPlacement : MonoBehaviour
{
    // Prefabs for different models
    public GameObject sofaPrefab;
    public GameObject lampPrefab;
    public GameObject tablePrefab;

    private ARRaycastManager raycastManager; // Manager for AR raycasting
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List to store raycast hits
    private GameObject placedObject;  // Reference to the placed object
    private GameObject selectedModel; // Currently selected model prefab

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>(); // Initialize the raycast manager
        selectedModel = sofaPrefab; // Set default selected model to sofa
    }

    void Update()
    {
        if (Input.touchCount == 0) return; // Exit if no touch detected

        Touch touch = Input.GetTouch(0); // Get the first touch
        if (Input.touchCount == 1 && touch.phase == TouchPhase.Began)
        {
            PlaceOrMoveObject(touch); // Handle placing or moving the object
        }
    }

    // Selects the sofa model
    public void SelectSofa()
    {
        selectedModel = sofaPrefab;
        Debug.Log("Sofa selected"); // Log selection
    }

    // Selects the lamp model
    public void SelectLamp()
    {
        selectedModel = lampPrefab;
        Debug.Log("Lamp selected"); // Log selection
    }

    // Selects the table model
    public void SelectTable()
    {
        selectedModel = tablePrefab;
        Debug.Log("Table selected"); // Log selection
    }

    // Places a new object or moves the existing one based on touch input
    private void PlaceOrMoveObject(Touch touch)
    {
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose; // Get the position and rotation from the raycast hit

            if (placedObject == null)
            {
                // Instantiate the selected model at the hit position and rotation
                placedObject = Instantiate(selectedModel, hitPose.position, hitPose.rotation);
                Debug.Log($"Placed {selectedModel.name} at: {hitPose.position}");
            }
            else
            {
                // Move the existing object to the new position
                placedObject.transform.position = hitPose.position;
            }
        }
    }
}