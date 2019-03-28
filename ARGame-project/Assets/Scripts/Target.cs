using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject followingCamera;
    private GameBoard gameBoard;

    [System.NonSerialized]
    public bool reached;


    void Start()
    {
        gameBoard = GetComponentInParent<GameBoard>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == followingCamera)
        {
            reached = true;
            gameBoard.CheckProgress();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == followingCamera)
        {
            reached = false;
        }
    }
}
