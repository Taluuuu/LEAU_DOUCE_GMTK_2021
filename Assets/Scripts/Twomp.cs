using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twomp : MonoBehaviour
{
    [SerializeField] private float _dropSpeed;
    [SerializeField] private float _upSpeed;
    [SerializeField] private float _detectorHeight;

    private Vector3 _initialPosition;
    private bool falling = false;
    private int nbrplayerUnder = 0;

    private Rigidbody _rb;
    private Vector2 force;
    private float movement = 0;

    private bool armee = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.drag = 1;
        _initialPosition = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector3(0, 0, 0);

        if (transform.position.y >= _initialPosition.y)
        {
            //went back

            armee = true;
            force.y = 0;

        }

        if (transform.position.y <= _detectorHeight)
        {
            //touch the ground
          
            armee = false;
            force.y = 0;
        }

        if (armee & falling)
        {
            //going down
            
            force.y = -1;
            movement = _dropSpeed;
        }

        if (!armee)
        {
            //going up
           
            force.y = 1;
            movement = _upSpeed;

        }

        _rb.AddForce(force * movement);
    }

    public void TwompDetect()
    {
        nbrplayerUnder++;
        falling = true;
    }

    public void TwompNoDetect()
    {
        nbrplayerUnder--;
        if(nbrplayerUnder == 0)
            falling = false;
    }

    public void SetDetectorHeight(Vector3 _newDetectorheight)
    {
        _detectorHeight = _newDetectorheight.y;
    }
}
