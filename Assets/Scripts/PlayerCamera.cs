using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float transitionSpeed;

    public float movementSpeed;
    float moveSpeed;
    public float damping;
    public Vector3 offset;

    public float changeSpeedDistance;

    public float smoothChangeTime = 4f;


    public bool StartPlayer;


    private void Start()
    {
        moveSpeed = transitionSpeed;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) < changeSpeedDistance)
        {
            StartPlayer = true;
            if (moveSpeed < movementSpeed)
            {
                moveSpeed += smoothChangeTime * Time.deltaTime;
            }
            else
            {
                moveSpeed = movementSpeed;
            }
        }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime * damping);
    }
}
