using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLoader : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevel_id;
    public Transform toFollow;

    public void LoadLevel()
    {
        GameObject currentLevel = Instantiate(levels[currentLevel_id]);
        currentLevel.transform.SetParent(transform, false);
        FollowingCamera[] followingCameras = currentLevel.GetComponentsInChildren<FollowingCamera>();

        foreach (FollowingCamera followingCamera in followingCameras)
        {
            followingCamera.toFollow = toFollow;
        }
    }
}
