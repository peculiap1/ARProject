using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ModelSelectionAndPlacement : MonoBehaviour
{
    public GameObject sofaPrefab;
    public GameObject lampPrefab;
    public GameObject tablePrefab;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject placedObject; // Only one object at a time
    private GameObject selectedModel; // The currently selected model for placement

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        selectedModel = sofaPrefab; // Default to the sofa model
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        // Handle object placement and movement
        Touch touch = Input.GetTouch(0);
        if (Input.touchCount == 1 && touch.phase == TouchPhase.Began)
        {
            PlaceOrMoveObject(touch);
        }
    }

    public void SelectSofa()
    {
        selectedModel = sofaPrefab;
        Debug.Log("Sofa selected");
    }

    public void SelectLamp()
    {
        selectedModel = lampPrefab;
        Debug.Log("Lamp selected");
    }

    public void SelectTable()
    {
        selectedModel = tablePrefab;
        Debug.Log("Table selected");
    }

    private void PlaceOrMoveObject(Touch touch)
    {
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose;

            if (placedObject == null)
            {
                // Place the selected model if no object exists
                placedObject = Instantiate(selectedModel, hitPose.position, hitPose.rotation);
                Debug.Log($"Placed {selectedModel.name} at: {hitPose.position}");
            }
            else
            {
                // Move the existing object
                placedObject.transform.position = hitPose.position;
            }
        }
    }
}
