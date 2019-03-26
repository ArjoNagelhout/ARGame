using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class PuzzlecameraBehaviour : MonoBehaviour
{
    public Transform arCamera;
    public float rotateSpeed;
    public float moveSpeed;

    public int totalRotation;
    public int maxRotations;
    public float minY;
    public float maxY;

    private int actualRotations;
    private int targetYRotation;
    private float currentY;

    void Start()
    {
        actualRotations = totalRotation % 4;
        currentY = totalRotation*((maxY - minY) / maxRotations);
        //targetYRotation = (actualRotations * 90) + 180;
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

    private void ChangeRotation(int from_rot, int value, int rot)
    {
        int newRotation = totalRotation + value;
        if (newRotation >= 0 && newRotation <= maxRotations)
        {
            if (totalRotation == maxRotations)
            {
                int needs = (maxRotations % 4) -1;
                if (from_rot == needs)
                {
                    targetYRotation = rot;
                    totalRotation += value;
                }
            } else if (totalRotation == 0)
            {
                int needs = (maxRotations % 4) + 1;
                if (from_rot == needs)
                {
                    targetYRotation = rot;
                    totalRotation += value;
                }
            }
            else
            {
                totalRotation += value;
                targetYRotation = rot;
            }

        }

        actualRotations += value;

    }

    private void TurnToObject(Transform target)
    {
        // Richting waar naar gekeken wordt tussen 0 en 360
        Vector3 targetDirection = (target.position - transform.position).normalized;
        targetDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)); // Verticale rotatie telt niet mee

        float lookRotation = Quaternion.LookRotation(targetDirection).eulerAngles.y;

        // Zorg ervoor dat de rotatie binnen waardes blijft. 
        if (actualRotations < 4) actualRotations += 4;
        else if (actualRotations > 8) actualRotations -= 4;

        // In welk kwadrant zit de speler nu
        int rotation = actualRotations % 4;

        // Check in welk kwadrant het target zich nu bevindt
        if (lookRotation >= 45 && lookRotation < 135)
        {
            // 0
            if (rotation == 3) ChangeRotation(3, -1, 90);
            if (rotation == 1) ChangeRotation(1, 1, 90);

        } 
        else if (lookRotation >= 135 && lookRotation < 225)
        {
            // 1
            if (rotation == 0) ChangeRotation(0, -1, 180);
            if (rotation == 2) ChangeRotation(2, 1, 180);

        }
        else if (lookRotation >= 225 && lookRotation < 315)
        {
            // 2
            if (rotation == 1) ChangeRotation(1, -1, 270);
            if (rotation == 3) ChangeRotation(3, 1, 270);

        }
        else if ((lookRotation >= 315 && lookRotation < 360) || (lookRotation >= 0 && lookRotation < 45))
        {
            // 3
            if (rotation == 2) ChangeRotation(2, -1, 0);
            if (rotation == 0) ChangeRotation(0, 1, 0);

        }

        Debug.Log(string.Format("lookRotation: {0}, rotation: {1}, totalRotation: {2}, actualRotations: {3}", lookRotation, rotation, totalRotation, actualRotations));
        Debug.DrawRay(transform.position, transform.forward*10);

        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
    }
}