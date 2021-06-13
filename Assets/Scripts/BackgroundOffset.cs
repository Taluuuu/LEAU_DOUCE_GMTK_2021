using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    private float smoothSpeed;
    private Camera Cam;
    private float PreviousCam;
    public float Ratio;
    void Awake()
    {
        Cam = FindObjectOfType<Camera>();
        smoothSpeed = 0.5f;
    }
    void Update()
    {
        if (PreviousCam != Cam.transform.position.x)
        {
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3((Cam.transform.position.x-transform.position.x)*Ratio+transform.position.x,transform.position.y,transform.position.z), smoothSpeed);
            transform.position = new Vector3((Cam.transform.position.x - PreviousCam) * Ratio + transform.position.x, transform.position.y, transform.position.z);
            PreviousCam = Cam.transform.position.x;
        }
    }
}
