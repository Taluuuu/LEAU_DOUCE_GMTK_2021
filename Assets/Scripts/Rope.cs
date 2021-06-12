
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

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private bool stuck1;
    [SerializeField] private bool stuck2;

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

            //if (i == 0)
            //    Destroy(rb);

            //if (i == _segmentCount - 1)
            //    Destroy(rb);
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

    private void Update()
    {
        GameObject first = (GameObject)_ropeSegments[0];
        GameObject last = (GameObject)_ropeSegments[_segmentCount - 1];

        if (!stuck1)
        {
            p1.transform.position = first.transform.position;
            first.GetComponent<Rigidbody>().WakeUp();
        }
        else
        {
            first.transform.position = p1.transform.position;
            first.GetComponent<Rigidbody>().Sleep();
        }

        if (!stuck2)
        {
            p2.transform.position = last.transform.position;
            last.GetComponent<Rigidbody>().WakeUp();
        }
        else
        {
            last.transform.position = p2.transform.position;
            last.GetComponent<Rigidbody>().Sleep();
        }
    }
}
