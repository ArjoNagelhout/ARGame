using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLoader : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevel_id;
    public Transform toFollow;

    public void LoadLevel(Pose levelPosition)
    {
        GameObject currentLevel = Instantiate(levels[currentLevel_id], levelPosition.position, levelPosition.rotation);
        FollowingCamera[] followingCameras = currentLevel.GetComponentsInChildren<FollowingCamera>();

        foreach (FollowingCamera followingCamera in followingCameras)
        {
            followingCamera.toFollow = toFollow;
        }
    }
}
