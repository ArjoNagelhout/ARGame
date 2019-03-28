using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public float maxSpeed;

    public Rigidbody rb;
    public Transform toFollow;

    void FixedUpdate()
    {
        MoveTowardsPosition(toFollow);

    }

    private void MoveTowardsPosition(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        targetDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)); // Verticale rotatie telt niet mee

        rb.AddForce(targetDirection * maxSpeed);
    }
}
