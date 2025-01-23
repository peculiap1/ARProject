using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ObjectManipulator : MonoBehaviour
{
    // Prefabs for different models
    public GameObject sofaPrefab;
    public GameObject lampPrefab;
    public GameObject tablePrefab;

    private ARRaycastManager raycastManager; // Manager for AR raycasting
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List to store raycast hits

    private GameObject sofaInstance; // Instance of the sofa
    private GameObject lampInstance; // Instance of the lamp
    private GameObject tableInstance; // Instance of the table

    private GameObject selectedPrefab; // The currently selected prefab
    private GameObject activeInstance; // The instance currently being interacted with

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>(); // Initialize the raycast manager
        selectedPrefab = sofaPrefab; // Set default selected prefab to sofa
    }

    void Update()
    {
        if (Input.touchCount == 0) return; // Exit if no touch detected

        Touch touch = Input.GetTouch(0); // Get the first touch

        if (touch.phase == TouchPhase.Began)
        {
            PlaceOrMoveObject(touch); // Handle placing or moving the object on touch start
        }

        if (Input.touchCount == 2 && activeInstance != null)
        {
            HandleTwoFingerGestures(); // Handle gestures if two fingers are touching and an object is active
        }
    }

    // Public method to select the sofa prefab
    public void SelectSofa()
    {
        selectedPrefab = sofaPrefab; // Set selected prefab to sofa
        activeInstance = sofaInstance; // Set active instance to existing sofa instance
        Debug.Log("Sofa selected"); // Log selection
    }

    // Public method to select the lamp prefab
    public void SelectLamp()
    {
        selectedPrefab = lampPrefab; // Set selected prefab to lamp
        activeInstance = lampInstance; // Set active instance to existing lamp instance
        Debug.Log("Lamp selected"); // Log selection
    }

    // Public method to select the table prefab
    public void SelectTable()
    {
        selectedPrefab = tablePrefab; // Set selected prefab to table
        activeInstance = tableInstance; // Set active instance to existing table instance
        Debug.Log("Table selected"); // Log selection
    }

    // Method to place a new object or move an existing one based on touch input
    private void PlaceOrMoveObject(Touch touch)
    {
        // Perform a raycast from the touch position against detected planes
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose; // Get the position and rotation from the raycast hit

            if (activeInstance == null)
            {
                // Instantiate the selected prefab at the hit position and rotation
                activeInstance = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
                activeInstance.transform.localScale = Vector3.one * 0.2f; // Adjust initial scale
                Debug.Log($"Placed {selectedPrefab.name} at: {hitPose.position}"); // Log placement

                // Assign the instantiated object to the corresponding instance variable
                if (selectedPrefab == sofaPrefab)
                {
                    sofaInstance = activeInstance;
                }
                else if (selectedPrefab == lampPrefab)
                {
                    lampInstance = activeInstance;
                }
                else if (selectedPrefab == tablePrefab)
                {
                    tableInstance = activeInstance;
                }
            }
            else
            {
                // Move the existing active object to the new hit position
                activeInstance.transform.position = hitPose.position;
                Debug.Log($"Moved {activeInstance.name} to: {hitPose.position}"); // Log movement
            }
        }
    }

    // Method to handle two-finger gestures for resizing and rotating
    private void HandleTwoFingerGestures()
    {
        ResizeObject(); // Handle object resizing
        RotateObject(); // Handle object rotation
    }

    // Method to resize the active object based on pinch gesture
    private void ResizeObject()
    {
        Touch touch0 = Input.GetTouch(0); // First touch
        Touch touch1 = Input.GetTouch(1); // Second touch

        // Calculate previous positions of both touches
        Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
        Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

        // Calculate previous and current distance between touches
        float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;

        float difference = currentMagnitude - prevMagnitude; // Difference in distance

        if (activeInstance != null)
        {
            // Calculate new scale based on the difference
            Vector3 newScale = activeInstance.transform.localScale + Vector3.one * (difference * 0.003f);
            // Clamp the scale to prevent it from becoming too small or too large
            activeInstance.transform.localScale = new Vector3(
                Mathf.Clamp(newScale.x, 0.05f, 3f),
                Mathf.Clamp(newScale.y, 0.05f, 3f),
                Mathf.Clamp(newScale.z, 0.05f, 3f)
            );
        }
    }

    // Method to rotate the active object based on twist gesture
    private void RotateObject()
    {
        Touch touch0 = Input.GetTouch(0); // First touch
        Touch touch1 = Input.GetTouch(1); // Second touch

        // Calculate previous positions of both touches
        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        // Calculate angles between previous and current touch positions
        float prevAngle = Mathf.Atan2(touch0PrevPos.y - touch1PrevPos.y, touch0PrevPos.x - touch1PrevPos.x);
        float currentAngle = Mathf.Atan2(touch0.position.y - touch1.position.y, touch0.position.x - touch1.position.x);

        float angleDifference = Mathf.Rad2Deg * (currentAngle - prevAngle); // Difference in angles in degrees

        if (activeInstance != null)
        {
            // Rotate the active object around the Y-axis based on the angle difference
            activeInstance.transform.Rotate(0, -angleDifference, 0, Space.World);
        }
    }
}