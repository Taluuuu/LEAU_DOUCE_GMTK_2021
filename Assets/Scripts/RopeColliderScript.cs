using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeColliderScript : MonoBehaviour
{
    public int Index;
    public Rope Rope;

    public bool HarassRope = false;

    private void Update()
    {
        // HELLO HELLO HELLO HELLO
        if(HarassRope)
            Rope.RopeSegmentCollision(Index);
    }

    private void FixedUpdate()
    {
        // fuck you
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HarassRope = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        HarassRope = false;
    }
}