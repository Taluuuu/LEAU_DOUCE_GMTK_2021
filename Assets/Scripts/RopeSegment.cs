using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    [HideInInspector] public RopeSegment PreviousSegment;
    [HideInInspector] public RopeSegment NextSegment;
    [HideInInspector] public float SegmentSeparation;
    [HideInInspector] public float BondingForce;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector3 leftDelta = PreviousSegment.transform.position - transform.position;
            Vector3 rightDelta = NextSegment.transform.position - transform.position;

            Vector3 forceToApply = Vector3.zero;

            if (leftDelta.magnitude > SegmentSeparation)
                forceToApply += leftDelta;

            if (rightDelta.magnitude > SegmentSeparation)
                forceToApply += rightDelta;

            _rb.AddForce(forceToApply * BondingForce);
        }
    }
}
