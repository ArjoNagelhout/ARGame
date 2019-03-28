using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject followingCamera;
    private GameBoard gameBoard;

    [System.NonSerialized]
    public bool reached;

    // Start is called before the first frame update
    void Start()
    {
        gameBoard = GetComponentInParent<GameBoard>();
    }

    // Update is called once per frame
    void Update()
    {

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
