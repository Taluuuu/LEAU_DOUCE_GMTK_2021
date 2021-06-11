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
            Vector3 leftDelta = Vector3.zero;
            if (PreviousSegment != null)
                leftDelta = PreviousSegment.transform.position - transform.position;

            Vector3 rightDelta = Vector3.zero;
            if (NextSegment != null)
                rightDelta = NextSegment.transform.position - transform.position;

            float angle = Mathf.Atan2(leftDelta.y, leftDelta.x);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Rad2Deg * angle + 90.0f);
            transform.localScale = new Vector3(0.1f, leftDelta.magnitude * 0.5f * 0.9f, 0.1f);

            Vector3 forceToApply = Vector3.zero;

            if (leftDelta.magnitude > SegmentSeparation)
                forceToApply += leftDelta;

            if (rightDelta.magnitude > SegmentSeparation)
                forceToApply += rightDelta;

            _rb.AddForce(forceToApply * BondingForce);
        }
    }
}
