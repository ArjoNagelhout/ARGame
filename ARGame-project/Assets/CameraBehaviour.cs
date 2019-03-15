using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class CameraBehaviour : MonoBehaviour
{
    public Transform arCamera;
    public float rotateSpeed;
    public float moveSpeed;
    //private Quaternion targetRotation;
    //private Vector3 targetDirection;
    //private float oldDirection;
    //private float deltaDirection;
    //private float actualDirection;

    //private float angle;
    //private float oldAngle;
    public int totalRotation;
    public int maxRotations;

    public float minY;
    public float maxY;

    private float currentY;

    void Start()
    {
        currentY = totalRotation*((maxY - minY) / maxRotations);
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    void Update()
    {
        TurnToObject(arCamera);
        UpdateY();
    }

    private void UpdateY()
    {
        currentY = minY + (maxY - minY) * ((float)totalRotation / maxRotations);
        Vector3 targetPosition = new Vector3(transform.position.x, currentY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void TurnToObject(Transform target)
    {
        // Richting waar naar gekeken wordt tussen 0 en 360
        Vector3 targetDirection = (target.position - transform.position).normalized;
        targetDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)); // Verticale rotatie telt niet mee

        float lookRotation = Quaternion.LookRotation(targetDirection).eulerAngles.y;

        int targetYRotation = 0;

        int rotation = totalRotation % 4; // Welk kwadrant is de rotatie nu

        // Check in welk kwadrant het target zich nu bevindt
        if (lookRotation >= 45 && lookRotation < 135)
        {
            // 0
            if (rotation == 3) totalRotation --;
            if (rotation == 1) totalRotation ++;

            targetYRotation = 90;

        } 
        else if (lookRotation >= 135 && lookRotation < 225)
        {
            // 1
            if (rotation == 0) totalRotation --;
            if (rotation == 2) totalRotation ++;

            targetYRotation = 180;
        }
        else if (lookRotation >= 225 && lookRotation < 315)
        {
            // 2
            if (rotation == 1) totalRotation --;
            if (rotation == 3) totalRotation ++;

            targetYRotation = 270;
        }
        else if ((lookRotation >= 315 && lookRotation < 360) || (lookRotation >= 0 && lookRotation < 45))
        {
            // 3
            if (rotation == 2) totalRotation --;
            if (rotation == 0) totalRotation ++;

            targetYRotation = 0;

        }

        totalRotation = Mathf.Clamp(totalRotation, 0, maxRotations);

        //Debug.Log(string.Format("lookRotation: {0}, rotation: {1}, totalRotation: {2}", lookRotation, rotation, totalRotation));

        Debug.Log(targetYRotation);

        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);


        //angle = 

        //float deltaAngle = angle - oldAngle;



        //oldAngle = angle;

        /*targetDirection = Vector3.Scale((arCamera.position - transform.position).normalized, new Vector3(1, 0, 1));
        targetRotation = Quaternion.LookRotation(targetDirection);

        transform.forward = targetDirection;
        deltaDirection = oldDirection - transform.eulerAngles.y;

        oldDirection = actualDirection;

        actualDirection += deltaDirection;

        Debug.Log(string.Format("deltaDirection:{0}, actualDirection:{1}", deltaDirection, actualDirection));
        */
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}