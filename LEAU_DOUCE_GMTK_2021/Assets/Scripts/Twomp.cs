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
    private Vector2D force;
    private float movement;

    private bool armee = true;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance._playerHit.AddListener(PlayerHit);
        _initialPosition = _model.position;

    }

    private void FixedUpdate()
    {
        _rb.AddForce(force * movement);

    }

    // Update is called once per frame
    void Update()
    {
        force.y = 0;

        if (_model.position = _initialPosition)
            armee = true;

        if (armee = true & falling = true)
        {
            //going down
            force.y = -1;
            movement = _dropSpeed;


            if(_model.position = _detectorHeight)
            {
                //touch the ground
                armee = false;
            }
        }

        if(armee = false)
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
        if(nbrplayerUnder = 0)
            falling = false;
    }
}
