using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [System.NonSerialized]
    public Target[] targets;

    public Transform placementIndicator;

    public string subtitle;

    private BoardLoader gameBoardLoader;

    public Animator animator;

    [System.NonSerialized]
    public bool completed;

    void Start()
    {
        targets = GetComponentsInChildren<Target>();
        gameBoardLoader = FindObjectOfType<BoardLoader>();
    }

    public void CheckProgress()
    {
        int amountReached = 0;

        foreach (Target target in targets)
        {
            if (target.reached == true)
            {
                amountReached += 1;
            }
        }

        if (amountReached == targets.Length)
        {
            if (completed == false)
            {
                completed = true;
                gameBoardLoader.FinishLevel();
            }
        }
    }

    public void DestroyBoard()
    {

    }
}
