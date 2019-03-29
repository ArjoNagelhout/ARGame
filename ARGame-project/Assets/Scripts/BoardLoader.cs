using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardLoader : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevelId;
    public Transform toFollow;

    private GameObject currentLevel;
    private FollowingCamera[] followingCameras;

    public LevelDescription levelDescription;

    public AudioSource succesSound;

    public void LoadLevel(int id)
    {
        // Destroys the previous level
        if (currentLevel != null)
        {
            currentLevel.GetComponent<GameBoard>().DestroyBoard();
            succesSound.Play();
        }

        // Create the new level
        currentLevel = Instantiate(levels[id]);
        currentLevel.transform.SetParent(transform, false);
        currentLevel.GetComponent<Animator>().SetBool("showBoard", true);
        GameBoard gameBoard = currentLevel.GetComponent<GameBoard>();
        levelDescription.ShowDescription(gameBoard.title, gameBoard.subtitle);

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
            currentLevel.GetComponent<GameBoard>().DestroyBoard();
            succesSound.Play();
            Invoke("LoadEnding", 5f);

        }
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(2);
    }
}
