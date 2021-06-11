using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public bool horizontalMovement;
    public bool verticalMovement;

    private Vector3 startPos;
    private Vector3 endPos;

    public float distance;
    public float speed;

    void Start()
    {

        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (horizontalMovement) 
        {

            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
            Debug.Log("Horizontal");
        
        }

        if (verticalMovement) 
        {

            endPos = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
            Debug.Log("Vertical");
        
        }

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time*speed, 1));


    }
}
