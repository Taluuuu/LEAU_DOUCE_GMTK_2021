using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed;

    public Transform Player1;
    public Transform Player2;

    void FixedUpdate()
    {

        Vector3 desiredPosition = new Vector3(((Player1.position.x + Player2.position.x) / 2), transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;



    }
}
