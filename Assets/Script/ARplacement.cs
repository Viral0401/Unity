using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public Camera camera;

    [SerializeField] private float xOffset;
    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float rotationZ;
    [SerializeField] private float scaleY = 0.1f; // Default scale value for Y
    [SerializeField] private float scaleZ = 0.1f; // Default scale value for Z
    [SerializeField] private float yTransform = 0.0f; // Y-axis transformation
    [SerializeField] private float zTransform = 0.0f; // Z-axis transformation
    [SerializeField] private float scaleFactor = 0.7f; // Scaling factor
    [SerializeField] private float scaleX = 1.0f;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if(spawnedObject == null)
        {
            ARPlaceObject();
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }


    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    // void ARPlaceObject()
    // {
    //     spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
    // }
    
    
    
    //  void ARPlaceObject()
    // {
    //     xOffset = 2.0f; // Adjust this value to set the desired distance in the X-axis
    //     Vector3 spawnPosition = new Vector3(PlacementPose.position.x + xOffset, PlacementPose.position.y, PlacementPose.position.z);

    //     spawnedObject = Instantiate(arObjectToSpawn, spawnPosition, PlacementPose.rotation);
    // }


    // void ARPlaceObject()
    // {
    //     xOffset = 3.0f; // Adjust this value to set the desired distance in the X-axis
    //     Vector3 spawnPosition = new Vector3(PlacementPose.position.x + xOffset, PlacementPose.position.y, PlacementPose.position.z);

    //     // Set rotation values from serialized fields
    //     Quaternion spawnRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

    //     spawnedObject = Instantiate(arObjectToSpawn, spawnPosition, spawnRotation);
    // }

    // void ARPlaceObject()
    // {
    //     xOffset = 2.0f; // Adjust this value to set the desired distance in the X-axis
    //     Vector3 spawnPosition = new Vector3(PlacementPose.position.x + xOffset, PlacementPose.position.y + yTransform, PlacementPose.position.z + zTransform);

    //     // Set rotation values from serialized fields
    //     Quaternion spawnRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

    //     spawnedObject = Instantiate(arObjectToSpawn, spawnPosition, spawnRotation);

    //     // Scale the spawned object
    //     spawnedObject.transform.localScale = new Vector3(scaleFactor, scaleY, scaleZ);
    // }


    void ARPlaceObject()
    {
        xOffset = 2.0f; // Adjust this value to set the desired distance in the X-axis
        Vector3 spawnPosition = new Vector3(PlacementPose.position.x + xOffset, PlacementPose.position.y + yTransform, PlacementPose.position.z + zTransform);

        // Set rotation values from serialized fields
        Quaternion spawnRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

        spawnedObject = Instantiate(arObjectToSpawn, spawnPosition, spawnRotation);

        // Scale the spawned object
        spawnedObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
}
   

}



