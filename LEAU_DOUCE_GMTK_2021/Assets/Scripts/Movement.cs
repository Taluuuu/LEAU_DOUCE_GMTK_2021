using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rB;
    Vector2 _force = new Vector2();
    private float _speed;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private bool _player1;
    [SerializeField] private bool _player2;
    private bool _jump;
    [SerializeField] private float _jumpingSpeed;


    private void Start()
    {
        _rB = GetComponent<Rigidbody>();
        //camera
    }

    private void FixedUpdate()
    {
        //Appliquer les physiques
        _rB.AddForce(_force * _speed);
        _speed = _normalSpeed;

        if (!_jump)
        {
            if (_player1)
            {
                SautPerso1();
            }

            if (_player2)
            {
                SautPerso2();
            }
        }

    }

    void Update()
    {
        //Determiner les forces
        _force.x = 0;
        _force.y = 0;

        if (_player1)
        {
            MovementPerso1();
        }

        if (_player2)
        {
            MovementPerso2();
        }

        _force.Normalize();

    }

    private void MovementPerso1()
    {

        if (Input.GetKey(KeyCode.A))
        {
            _force.x -= 1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _force.x += 1.0f;
        }

    }

    private void MovementPerso2()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _force.x -= 1.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _force.x += 1.0f;
        }
    }

    private void SautPerso1()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _jump = true;
            _rB.AddForce(Vector2.up * _jumpingSpeed, ForceMode.Impulse);
        }
    }

    private void SautPerso2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _jump = true;
            _rB.AddForce(Vector2.up * _jumpingSpeed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _jump = false;
        }
    }

}
