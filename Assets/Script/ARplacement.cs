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
    private Pose placementPose;
    private ARRaycastManager arRaycastManager;
    private bool placementPoseIsValid = false;
    private Camera arCamera; // Reference to the AR camera

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arCamera = FindObjectOfType<ARCameraManager>().GetComponent<Camera>(); // Find the AR camera
    }

    void Update()
    {
        if(spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, placementPose.position, placementPose.rotation);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;
// using System.Net;
// using System.Text;
// using System.Threading;

// public class ARPlacement : MonoBehaviour
// {
//     public GameObject arObjectToSpawn;
//     public GameObject placementIndicator;
//     private GameObject spawnedObject;
//     private ARRaycastManager arRaycastManager;
//     private bool placementPoseIsValid = false;
//     private Vector3 handPosition;

//     UdpClient udpClient;
//     Thread receiveThread;

//     void Start()
//     {
//         arRaycastManager = FindObjectOfType<ARRaycastManager>();
//         udpClient = new UdpClient(9999);
//         receiveThread = new Thread(new ThreadStart(ReceiveHandPosition));
//         receiveThread.IsBackground = true;
//         receiveThread.Start();
//     }

//     void Update()
//     {
//         if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//         {
//             ARPlaceObject();
//         }

//         UpdatePlacementIndicator();
//     }

//     void UpdatePlacementIndicator()
//     {
//         if (spawnedObject == null)
//         {
//             placementIndicator.SetActive(true);
//             placementIndicator.transform.position = handPosition;
//         }
//         else
//         {
//             placementIndicator.SetActive(false);
//         }
//     }

//     void ReceiveHandPosition()
//     {
//         while (true)
//         {
//             try
//             {
//                 IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
//                 byte[] data = udpClient.Receive(ref anyIP);
//                 string message = Encoding.UTF8.GetString(data);
//                 string[] parts = message.Split(',');
//                 float x = float.Parse(parts[0]);
//                 float y = float.Parse(parts[1]);
//                 float z = float.Parse(parts[2]); // Optional, depending on your needs

//                 handPosition = new Vector3(x, y, z);
//                 placementPoseIsValid = true;
//             }
//             catch (System.Exception e)
//             {
//                 Debug.Log(e.ToString());
//             }
//         }
//     }

//     void ARPlaceObject()
//     {
//         spawnedObject = Instantiate(arObjectToSpawn, handPosition, Quaternion.identity);
//     }

//     void OnApplicationQuit()
//     {
//         receiveThread.Abort();
//         udpClient.Close();
//     }
// }

