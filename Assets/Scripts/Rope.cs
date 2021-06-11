
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private int _segmentCount;
    [SerializeField] private GameObject _ropeSegmentPrefab;
    [SerializeField] private float _segmentSeparation;
    [SerializeField] private float _bondingForce;
    [SerializeField] private float _segmentDrag;
    [SerializeField] private float _mass;
    private ArrayList _ropeSegments;

    void Start()
    {
        _ropeSegments = new ArrayList(_segmentCount);

        for (int i = 0; i < _segmentCount; i++)
        {
            var position = new Vector3(i * _segmentSeparation, 0.0f) + transform.position;
            var newSeg = Instantiate(_ropeSegmentPrefab, position, Quaternion.identity);
            newSeg.transform.SetParent(transform);
            _ropeSegments.Add(newSeg);

            var rb = newSeg.GetComponent<Rigidbody>();
            if (i > 0 && i < _segmentCount - 1)
            {
                rb.drag = _segmentDrag;
                rb.mass = 1.0f / _segmentCount;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            }

            if (i == 0)
                Destroy(rb);
        }

        for (int i = 0; i < _segmentCount; i++)
        {
            RopeSegment seg = ((GameObject)_ropeSegments[i]).GetComponent<RopeSegment>();
            RopeSegment previousSeg = null;
            if(i > 0)
                previousSeg = ((GameObject)_ropeSegments[i - 1]).GetComponent<RopeSegment>();
            RopeSegment nextSeg = null;
            if(i < _segmentCount - 1)
                nextSeg = ((GameObject)_ropeSegments[i + 1]).GetComponent<RopeSegment>();
            seg.PreviousSegment = previousSeg;
            seg.NextSegment = nextSeg;
            seg.SegmentSeparation = _segmentSeparation;
            seg.BondingForce = _bondingForce;
        }
    }
}
