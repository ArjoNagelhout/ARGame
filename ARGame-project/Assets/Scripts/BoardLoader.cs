using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLoader : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevelId;
    public Transform toFollow;

    private GameObject currentLevel;
    private FollowingCamera[] followingCameras;

    public LevelDescription levelDescription;

    public void LoadLevel(int id)
    {
        // Destroys the previous level
        currentLevel.GetComponent<GameBoard>().DestroyBoard();

        // Create the new level
        currentLevel = Instantiate(levels[id]);
        currentLevel.transform.SetParent(transform, false);

        string title = "Level " + (id+1).ToString();
        levelDescription.ShowDescription(title, currentLevel.GetComponent<GameBoard>().subtitle);

        followingCameras = currentLevel.GetComponentsInChildren<FollowingCamera>();

        foreach (FollowingCamera followingCamera in followingCameras)
        {
            followingCamera.toFollow = toFollow;
        }
    }

    public void PauseLevel()
    {
        foreach (FollowingCamera followingCamera in followingCameras)
        {
            followingCamera.enabled = false;
        }
    }

    public void ContinueLevel()
    {
        foreach (FollowingCamera followingCamera in followingCameras)
        {
            followingCamera.enabled = true;
        }
    }

    public void FinishLevel()
    {

        if ((currentLevelId+1) < levels.Length)
        {
            // Load new level
            currentLevelId += 1;
            LoadLevel(currentLevelId);
        }
        else
        {
            // Show end screen
        }
    }
}
