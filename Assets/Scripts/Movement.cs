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
    private bool _stuckToWall;
    private bool _jump;
    public bool _ball = false;
    [SerializeField] private float _jumpingSpeed;
    [SerializeField] private PhysicMaterial _bouncy;


    private void Start()
    {
        _rB = GetComponent<Rigidbody>();
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

        if (_stuckToWall)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rB.velocity = Vector3.zero;
                _rB.Sleep();
            }
        }

        if (_ball)
        {
            //faire du dommage aux ennemis --> autre classe
            //devenir une boule --> autre classe

            GetComponent<Collider>().material = _bouncy;
            
        }
        else
        {
            GetComponent<Collider>().material = null;
        }

    }

    void Update()
    {

        //Determiner les forces
        _force.x = 0;
        _force.y = 0;

        Jump();

        if (_player1)
        {
            MovementPerso1();
            AttackPerso1();
        }

        if (_player2)
        {
            MovementPerso2();
            AttackPerso2();
        }

        _force.Normalize();

        if (_force.x == 1)
        {
            _rB.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (_force.x == -1)
        {
            _rB.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

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

    private void AttackPerso1()
    {
        if (Input.GetKey(KeyCode.E))
        {

            _ball = true;
        }
        else
            _ball = false;
    }

    private void AttackPerso2()
    {
        if (Input.GetKey(KeyCode.L))
        {
            _ball = true;
        }
        else
            _ball = false;
    }

    private void Jump()
    {
        if (Physics.Raycast(_rB.position, Vector3.down, 0.8f, 1 << 3))
        {
            _jump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _stuckToWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _stuckToWall = false;
        }
    }

}
