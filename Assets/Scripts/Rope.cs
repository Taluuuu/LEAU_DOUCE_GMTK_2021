
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float SegmentLength = 0.25f;
    public bool RopeIsEnabled
    {
        get => _ropeIsEnabled;
        set
        {
            _ropeIsEnabled = value;
            _lineRenderer.enabled = _ropeIsEnabled;
            for (int i = 0; i < _segmentCount; i++)
                _ropeColliders[i].enabled = _ropeIsEnabled;        
        }
    }
    private bool _ropeIsEnabled = true;
    [SerializeField] private int _segmentCount = 35;
    [SerializeField] private float _ropeWidth = 0.1f;
    [SerializeField] private bool _elastique = false;

    [SerializeField] private Rigidbody _p1;
    [SerializeField] private Rigidbody _p2;
    [SerializeField] private bool _p1Stuck;
    [SerializeField] private bool _p2Stuck;

    [SerializeField] private float _force = 50.0f;
    [SerializeField] private bool _applyForceBetweenObjects = false;

    private LineRenderer _lineRenderer;
    private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
    private List<SphereCollider> _ropeColliders = new List<SphereCollider>();


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = _p1.position;

        float delta = (_p2.position - _p1.position).magnitude / _segmentCount;
        Vector3 direction = (_p2.position - _p1.position).normalized;

        gameObject.layer = 6;

        for (int i = 0; i < _segmentCount; i++)
        {
            _ropeSegments.Add(new RopeSegment(ropeStartPoint));
            var col = gameObject.AddComponent<SphereCollider>();
            col.center = ropeStartPoint;
            col.radius = 0.05f;
            _ropeColliders.Add(col);
            ropeStartPoint += direction * delta;
        }
    }

    void Update()
    {
        if (RopeIsEnabled)
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
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (RopeIsEnabled)
        {
            // Simulate
            Vector2 forceGravity = new Vector2(0f, -1.5f);

            for (int i = 0; i < _segmentCount; i++)
            {
                RopeSegment firstSegment = _ropeSegments[i];
                Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
                firstSegment.posOld = firstSegment.posNow;
                firstSegment.posNow += velocity;
                firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
                _ropeSegments[i] = firstSegment;
            }

            for (int i = 0; i < (_elastique ? 1 : 50); i++)
            {
                ApplyConstraint();
            }

            // Loop through all segments
            for (int i = 0; i < _segmentCount; i++)
            {
                var curCollider = _ropeColliders[i];
                // Set collider's initial position to that of the rope segment
                curCollider.center = _ropeSegments[i].posNow;
                // Get all collisions with the segment's sphere collider
                var encounters = Physics.OverlapSphere(curCollider.center, curCollider.radius);
                // Loop through all encountered colliders
                for (int c = 0; c < encounters.Length; c++)
                {
                    var encounter = encounters[c];
                    // Skip itself and the player
                    if (encounter != curCollider && encounter.gameObject.layer != 7 && encounter.gameObject.layer != 6)
                    {
                        // Calculate new position
                        Physics.ComputePenetration(
                            curCollider, transform.position, curCollider.transform.rotation,
                            encounter, encounter.transform.position, encounter.transform.rotation,
                            out Vector3 dir, out float dis);

                        curCollider.center += dir * dis * 0.9f;
                    }
                }

                // Apply position to rope
                var curSeg = _ropeSegments[i];
                curSeg.posNow = curCollider.center;
                _ropeSegments[i] = curSeg;
            }

            // Add forces to tied rigidbodies
            var currentRopeLength = CalculateRopeLength();
            var ropeExtension = Mathf.Clamp(currentRopeLength - _segmentCount * SegmentLength, 0.0f, SegmentLength * 10.0f);

            if (_applyForceBetweenObjects)
            {
                if (_p1Stuck)
                {
                    if (ropeExtension > 0.0f)
                        _p1.AddForce((_ropeSegments[_segmentCount / 2].posNow - _ropeSegments[0].posNow).normalized * _force * ropeExtension);
                    //_p1.AddForce((_p2.position - _p1.position).normalized * ropeExtension * _force);
                    _p1.useGravity = true;
                }
                else
                {
                    _p1.position = _ropeSegments[0].posNow;
                    _p1.useGravity = false;
                }

                if (_p2Stuck)
                {
                    if (ropeExtension > 0.0f)
                        _p2.AddForce((_ropeSegments[_segmentCount / 2].posNow - _ropeSegments[_segmentCount - 1].posNow).normalized * _force * ropeExtension);
                    //_p2.AddForce((_p1.position - _p2.position).normalized * ropeExtension * _force);
                    _p2.useGravity = true;
                }
                else
                {
                    _p2.position = _ropeSegments[_segmentCount - 1].posNow;
                    _p2.useGravity = false;
                }
            }
        }
    }

    private float CalculateRopeLength()
    {
        float total = 0.0f;
        for(int i = 0; i < _segmentCount - 1; i++)
            total += (_ropeSegments[i].posNow - _ropeSegments[i + 1].posNow).magnitude;

        return total;
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = _ropeSegments[0];
        RopeSegment endSegment = _ropeSegments[_segmentCount - 1];

        if(_p1Stuck)
            firstSegment.posNow = _p1.position;

        if(_p2Stuck)
            endSegment.posNow = _p2.position;

        _ropeSegments[0] = firstSegment;
        _ropeSegments[_segmentCount - 1] = endSegment;

        if (_p1Stuck)
        {
            for (int i = 0; i < _segmentCount - 1; i++)
            {
                RopeSegment firstSeg = _ropeSegments[i];
                RopeSegment secondSeg = _ropeSegments[i + 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
                float error = Mathf.Abs(dist - SegmentLength);
                Vector2 changeDir = Vector2.zero;

                if (dist > SegmentLength)
                {
                    changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
                }
                else if (dist < SegmentLength)
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
        else
        {
            for (int i = _segmentCount - 1; i > 0; i--)
            {
                RopeSegment firstSeg = _ropeSegments[i];
                RopeSegment secondSeg = _ropeSegments[i - 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
                float error = Mathf.Abs(dist - SegmentLength);
                Vector2 changeDir = Vector2.zero;

                if (dist > SegmentLength)
                {
                    changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
                }
                else if (dist < SegmentLength)
                {
                    changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
                }

                Vector2 changeAmount = changeDir * error;
                if (i != 0)
                {
                    firstSeg.posNow -= changeAmount * 0.5f;
                    _ropeSegments[i] = firstSeg;
                    secondSeg.posNow += changeAmount * 0.5f;
                    _ropeSegments[i - 1] = secondSeg;
                }
                else
                {
                    secondSeg.posNow += changeAmount;
                    _ropeSegments[i - 1] = secondSeg;
                }
            }
        }
    }
}
