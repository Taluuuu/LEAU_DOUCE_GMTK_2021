using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float smoothSpeed;
    private Camera Cam;
    private float PreviousCam;
    public float Ratio;
    void Awake()
    {
        Cam = FindObjectOfType<Camera>();
        smoothSpeed = 0.125f;
    }
    void Update()
    {
        if (PreviousCam != Cam.transform.position.x)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, (Cam.transform.position-transform.position)*Ratio+transform.position, smoothSpeed);
            transform.position = smoothedPosition;
            PreviousCam = Cam.transform.position.x;
        }
    }
}
