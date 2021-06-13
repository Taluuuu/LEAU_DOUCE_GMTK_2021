using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _contractedLength = 0.1f, _extendedLength = 0.2f;

    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _extendedMaterial;

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

    private Vector3 _normal = new Vector3();

    [SerializeField] private float _jumpingSpeed;
    [SerializeField] private PhysicMaterial _bouncy;

    [SerializeField] public Rope _rope;


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

                    if (Mathf.Abs(_normal.x) > Mathf.Abs(_normal.y))
                    {
                        _animator.SetBool("AggripeMur", true);
                    }
                    else
                    {
                        if (_normal.y > 0.0f)
                        {
                            _animator.SetBool("AggripeSol", true);
                        }
                        else
                        {
                            _animator.SetBool("AggripePlafond", true);
                        }
                    }
                }
                else
                {
                    _rB.WakeUp();
                    _animator.SetBool("AggripeMur", false);
                    _animator.SetBool("AggripeSol", false);
                    _animator.SetBool("AggripePlafond", false);
                }
            }

            if (_player2)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    _rB.velocity = Vector3.zero;
                    _rB.Sleep();

                    if (Mathf.Abs(_normal.x) > Mathf.Abs(_normal.y))
                    {
                        _animator.SetBool("AggripeMur", true);
                    }
                    else
                    {
                        if (_normal.y > 0.0f)
                        {
                            _animator.SetBool("AggripeSol", true);
                        }
                        else
                        {
                            _animator.SetBool("AggripePlafond", true);
                        }
                    }
                }
                else
                {
                    _rB.WakeUp();
                    _animator.SetBool("AggripeMur", false);
                    _animator.SetBool("AggripeSol", false);
                    _animator.SetBool("AggripePlafond", false);
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

        UnityEngine.GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

        if(Players[0].GetComponent<Transform>().position == transform.position)
        {
            if ((Players[1].GetComponent<Transform>().position - transform.position).magnitude <= 2)
            {
                _rope.RopeIsEnabled = true;
                _rope._timeRope = 0;
            }
        }
        else
        {

            if ((Players[0].GetComponent<Transform>().position - transform.position).magnitude <= 2)
            {
                _rope.RopeIsEnabled = true;
                _rope._timeRope = 0;
            }
        }
       
         //Determiner les forces
        _force.x = 0;
        _force.y = 0;

        _animator.SetBool("Jumping", false);

        Jump();

        if (_player1)
        {
            MovementPerso1();
            AttackPerso1();
            _animator.SetBool("Running", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        }

        if (_player2)
        {
            MovementPerso2();
            AttackPerso2();
            _animator.SetBool("Running", Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
        }

        _force.Normalize();

        if (_force.x == 1)
        {
            _rB.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }

        if (_force.x == -1)
        {
            _rB.transform.rotation = Quaternion.Euler(-90, 180, 0);
        }


        if(Input.GetKey(KeyCode.Space))
        {
            _rope.SegmentLength = _extendedLength;
            _rope.GetComponent<LineRenderer>().material = _extendedMaterial;
        }
        else
        {
            _rope.SegmentLength = _contractedLength;
            _rope.GetComponent<LineRenderer>().material = _normalMaterial;
        }
    }

    private void MovementPerso1()
    {
        // Note de charlo : WTF
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
            _animator.SetBool("Jumping", true);
        }
    }

    private void SautPerso2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _jump = true;
            _rB.AddForce(Vector2.up * _jumpingSpeed, ForceMode.Impulse);
            _animator.SetBool("Jumping", true);
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
        if (Physics.Raycast(_rB.position, Vector3.down, 2.5f, 1 << 3))
        {
            _jump = false;
            _animator.SetBool("Falling", false);
        }
        else
        {
            _animator.SetBool("Falling", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _stuckToWall = true;
            _normal = collision.GetContact(0).normal;  
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _stuckToWall = false;
        }
    }

    public void BrokeRope()
    {
        _rope.RopeIsEnabled = false;
    }

}
