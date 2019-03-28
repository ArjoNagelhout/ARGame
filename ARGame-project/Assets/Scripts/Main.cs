using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class Main : MonoBehaviour
{
    [System.NonSerialized]
    public bool boardPlaced;

    private ARSessionOrigin arOrigin;

    private Pose placementPose;
    private bool placementPoseIsValid;
    public GameObject placementIndicator;

    private GameObject gameBoardLoader;
    public GameObject placementPopup;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        gameBoardLoader = FindObjectOfType<BoardLoader>().gameObject;
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceBoard();
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    // Place the board at the place of the placement indicator
    public void PlaceBoard()
    {
        placementPopup.SetActive(false);
        gameBoardLoader.GetComponent<BoardLoader>().LoadLevel(placementPose);
    }
}