using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Avatar;
    public Vector3 offeset;
    public float maxRotation;
    public float maxRotationFactor = 0.1f;

    Vector3 movementVector;
    float mouseX;
    float mouseY;

    private void Start()
    {
        maxRotation = 0.3f;
    }

    private void LateUpdate()
    {
        FollowAvatar();
    }

    void FollowAvatar()
    {
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");
        transform.position = Avatar.position + offeset;
        if (transform.rotation.y<= maxRotation & transform.rotation.y >= -maxRotation & transform.rotation.x <= maxRotation & transform.rotation.x >= -maxRotation)
        {
            movementVector = new Vector3(-mouseY, mouseX, 0);
            transform.Rotate(movementVector);
        }
        else
        {
            if (transform.rotation.y > maxRotation)
            {
                movementVector = new Vector3(0, maxRotation-transform.rotation.y- maxRotationFactor, 0);
                transform.Rotate(movementVector);
            }
            else if(transform.rotation.y < -maxRotation)
            {
                movementVector = new Vector3(0, - maxRotation - transform.rotation.y+ maxRotationFactor, 0);
                transform.Rotate(movementVector);
            }

            if (transform.rotation.x > maxRotation)
            {
                movementVector = new Vector3(maxRotation-transform.rotation.x - maxRotationFactor, 0, 0);
                transform.Rotate(movementVector);
            }
            else if (transform.rotation.x < -maxRotation)
            {
                movementVector = new Vector3(-maxRotation - transform.rotation.x + maxRotationFactor, 0, 0);
                transform.Rotate(movementVector);
            }
        }
    }
}
