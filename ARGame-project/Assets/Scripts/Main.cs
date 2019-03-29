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
    private bool showPlacementIndicator;

    private GameObject gameBoardLoader;
    public PlacementPopup placementPopup;
    private bool firstTime = true;

    public RepositionButton repositionButton;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        gameBoardLoader = FindObjectOfType<BoardLoader>().gameObject;


        showPlacementIndicator = true;
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
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
        if (placementPoseIsValid && showPlacementIndicator)
        {
            if (firstTime)
            {
                placementPopup.OpenPopup();
                firstTime = false;
            }

            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            gameBoardLoader.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void RepositionBoard()
    {
        repositionButton.HideButton();
        placementPopup.OpenPopup();
        showPlacementIndicator = true;
        gameBoardLoader.GetComponent<BoardLoader>().PauseLevel();
    }

    // Place the board at the place of the placement indicator
    public void PlaceBoard()
    {
        if (placementPoseIsValid)
        {
            // Place for the first time
            if (boardPlaced == false)
            {
                gameBoardLoader.GetComponent<BoardLoader>().LoadLevel(0);
            }

            boardPlaced = true;
            placementPopup.ClosePopup();
            gameBoardLoader.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            repositionButton.ShowButton();
            showPlacementIndicator = false;
            gameBoardLoader.GetComponent<BoardLoader>().ContinueLevel();
        }
    }
}