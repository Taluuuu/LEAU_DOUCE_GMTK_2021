
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private float _segmentLength = 0.25f;
    [SerializeField] private int _segmentCount = 35;
    [SerializeField] private float _ropeWidth = 0.1f;

    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;

    [SerializeField] private GameObject _ropeCollider;

    private LineRenderer _lineRenderer;
    private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
    private List<Rigidbody> _ropeColliders = new List<Rigidbody>();


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = _p1.position;

        float delta = (_p2.position - _p1.position).magnitude / _segmentCount;
        Vector3 direction = (_p2.position - _p1.position).normalized;

        for (int i = 0; i < _segmentCount; i++)
        {
            var segment = Instantiate(_ropeCollider, ropeStartPoint, Quaternion.identity);
            segment.AddComponent<RopeColliderScript>().Index = i;
            segment.GetComponent<RopeColliderScript>().Rope = this;
            _ropeColliders.Add(segment.GetComponent<Rigidbody>());
            _ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint += direction * delta;
        }
    }

    void Update()
    {
        // Draw line
        float lineWidth = _ropeWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[_segmentCount];
        for (int i = 0; i < _segmentCount; i++)
        {
            ropePositions[i] = _ropeSegments[i].posNow;
        }

        _lineRenderer.positionCount = ropePositions.Length;
        _lineRenderer.SetPositions(ropePositions);
    }

    private void FixedUpdate()
    {
        // Simulate shit
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < _segmentCount; i++)
        {
            RopeSegment firstSegment = _ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            _ropeSegments[i] = firstSegment;
        }

        for (int i = 0; i < 50; i++)
        {
            ApplyConstraint();
        }

        for (int i = 0; i < _segmentCount; i++)
        {
            _ropeColliders[i].transform.position = _ropeSegments[i].posNow;
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = _ropeSegments[0];
        firstSegment.posNow = _p1.position;
        _ropeSegments[0] = firstSegment;

        firstSegment.posNow = _p2.position;
        _ropeSegments[_segmentCount - 1] = firstSegment;

        for (int i = 0; i < _segmentCount - 1; i++)
        {
            RopeSegment firstSeg = _ropeSegments[i];
            RopeSegment secondSeg = _ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - _segmentLength);
            Vector2 changeDir = Vector2.zero;

            if (dist > _segmentLength)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < _segmentLength)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                _ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                _ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                _ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    public void RopeSegmentCollision(int index)
    {
        var currentSegment = _ropeSegments[index];
        currentSegment.posOld = currentSegment.posNow;
        currentSegment.posNow = _ropeColliders[index].transform.position;
        _ropeSegments[index] = currentSegment;
    }
}
