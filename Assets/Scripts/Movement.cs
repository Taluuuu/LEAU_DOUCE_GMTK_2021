using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _contractedLength = 0.1f, _extendedLength = 0.2f;

    private Rigidbody _rB;
    Vector2 _force = new Vector2();
    private float _speed;
    [SerializeField] private float _normalSpeed;
    [SerializeField] public float _attackTime;
    [SerializeField] private float _attackCooldown;
    [SerializeField] public bool _player1;
    [SerializeField] public bool _player2;

    private float _timeSinceAttack1;
    private float _timeSinceAttack2;
    public float _timeAttack1;
    public float _timeAttack2;
    private bool _stuckToWall;
    private bool _jump;
    public bool _ball = false;

    [SerializeField] private float _jumpingSpeed;
    [SerializeField] private PhysicMaterial _bouncy;

    [SerializeField] private Rope _rope;


    private void Start()
    {

        _rB = GetComponent<Rigidbody>();
        _timeAttack1 = _attackTime;
        _timeAttack2 = _attackTime;
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
            if (_player1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _rB.velocity = Vector3.zero;
                    _rB.Sleep();
                }
                else
                {
                    _rB.WakeUp();
                }
            }

            if (_player2)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    _rB.velocity = Vector3.zero;
                    _rB.Sleep();
                }
                else
                {
                    _rB.WakeUp();
                }
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


        _rope.SegmentLength = Input.GetKey(KeyCode.Space) ? _extendedLength : _contractedLength;
    }

    private void MovementPerso1()
    {
        if (!Physics.Raycast(_rB.position, Vector3.left, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.A))
            {
                _force.x -= 1.0f;
            }
        }

        if (!Physics.Raycast(_rB.position, Vector3.right, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.D))
            {
                _force.x += 1.0f;
            }
        }

    }

    private void MovementPerso2()
    {
        if (!Physics.Raycast(_rB.position, Vector3.left, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _force.x -= 1.0f;
            }
        }

        if (!Physics.Raycast(_rB.position, Vector3.right, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _force.x += 1.0f;
            }
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
        _timeSinceAttack1 += Time.deltaTime;

        if (_ball)
        {
            _timeAttack1 -= Time.deltaTime;
        }

        if(_timeAttack1 <= 0)
        {
            _ball = false;
        }

        if(!_ball & _timeSinceAttack1 - _attackTime >= _attackCooldown)
        {
            _timeAttack1 = _attackTime;
        }

        if (Input.GetKey(KeyCode.E) & _timeAttack1 == _attackTime)
        {

            _ball = true;
            _timeSinceAttack1 = 0;
        }


    }

    private void AttackPerso2()
    {
        _timeSinceAttack2 += Time.deltaTime;

        if (_ball)
        {
            _timeAttack2 -= Time.deltaTime;
        }

        if (_timeAttack2 <= 0)
        {
            _ball = false;
        }

        if (!_ball & _timeSinceAttack2 - _attackTime >= _attackCooldown)
        {
            _timeAttack2 = _attackTime;
        }

        if (Input.GetKey(KeyCode.L) & _timeAttack2 == _attackTime)
        {

            _ball = true;
            _timeSinceAttack2 = 0;
        }
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
