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
        _initialPosition = transform.position;

    }

    private void FixedUpdate()
    {
        _rb.AddForce(force * movement);

    }

    // Update is called once per frame
    void Update()
    {
        force.y = 0;

        if (transform.position == _initialPosition)
            armee = true;

        if (armee & falling)
        {
            //going down
            force.y = -1;
            movement = _dropSpeed;


            if(transform.position.y == _detectorHeight)
            {
                //touch the ground
                armee = false;
            }
        }

        if(!armee)
        {
            //going up
            force.y = 1;
            movement = _upSpeed;
        }

    }

    private void TwompDetect()
    {
        nbrplayerUnder++;
        falling = true;
    }

    private void TwompNoDetect()
    {
        nbrplayerUnder--;
        if(nbrplayerUnder == 0)
            falling = false;
    }
}
