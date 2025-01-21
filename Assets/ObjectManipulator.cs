using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ObjectManipulator : MonoBehaviour
{
    public GameObject sofaPrefab;
    public GameObject lampPrefab;
    public GameObject tablePrefab;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject sofaInstance;
    private GameObject lampInstance;
    private GameObject tableInstance;

    private GameObject selectedPrefab; // The currently selected prefab
    private GameObject activeInstance; // The instance currently being interacted with

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        selectedPrefab = sofaPrefab; // Default selection
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            PlaceOrMoveObject(touch);
        }

        if (Input.touchCount == 2 && activeInstance != null)
        {
            HandleTwoFingerGestures();
        }
    }

    // Public methods for selecting a prefab
    public void SelectSofa()
    {
        selectedPrefab = sofaPrefab;
        activeInstance = sofaInstance;
        Debug.Log("Sofa selected");
    }

    public void SelectLamp()
    {
        selectedPrefab = lampPrefab;
        activeInstance = lampInstance;
        Debug.Log("Lamp selected");
    }

    public void SelectTable()
    {
        selectedPrefab = tablePrefab;
        activeInstance = tableInstance;
        Debug.Log("Table selected");
    }

    private void PlaceOrMoveObject(Touch touch)
    {
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose;

            if (activeInstance == null)
            {
                // Instantiate the selected prefab and assign it to the respective instance variable
                activeInstance = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
                activeInstance.transform.localScale = Vector3.one * 0.2f; // Adjust initial scale
                Debug.Log($"Placed {selectedPrefab.name} at: {hitPose.position}");

                // Assign to the specific furniture instance
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
                // Move the existing object to the new position
                activeInstance.transform.position = hitPose.position;
                Debug.Log($"Moved {activeInstance.name} to: {hitPose.position}");
            }
        }
    }

    private void HandleTwoFingerGestures()
    {
        ResizeObject();
        RotateObject();
    }

    private void ResizeObject()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
        Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

        float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        if (activeInstance != null)
        {
            Vector3 newScale = activeInstance.transform.localScale + Vector3.one * (difference * 0.003f);
            activeInstance.transform.localScale = new Vector3(
                Mathf.Clamp(newScale.x, 0.05f, 3f),
                Mathf.Clamp(newScale.y, 0.05f, 3f),
                Mathf.Clamp(newScale.z, 0.05f, 3f)
            );
        }
    }

    private void RotateObject()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevAngle = Mathf.Atan2(touch0PrevPos.y - touch1PrevPos.y, touch0PrevPos.x - touch1PrevPos.x);
        float currentAngle = Mathf.Atan2(touch0.position.y - touch1.position.y, touch0.position.x - touch1.position.x);

        float angleDifference = Mathf.Rad2Deg * (currentAngle - prevAngle);

        if (activeInstance != null)
        {
            activeInstance.transform.Rotate(0, -angleDifference, 0, Space.World);
        }
    }
}
