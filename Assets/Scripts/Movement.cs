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
    [SerializeField] public AudioManager AudioManager;
    [SerializeField] public AudioSource[] Music;

    private float _timeSinceAttack1;
    private float _timeSinceAttack2;
    public float _timeAttack1;
    public float _timeAttack2;
    private bool _grabbing;
    private bool _jump;
    public bool _ball = false;

    [SerializeField] private float _jumpingSpeed;
    [SerializeField] private PhysicMaterial _bouncy;

    [SerializeField] public Rope _rope;

    private void Awake()
    {
        AudioManager = FindObjectOfType<AudioManager>();
        Music = FindObjectsOfType<AudioSource>();
    }
    private void Start()
    {

        _rB = GetComponent<Rigidbody>();
        _timeAttack1 = _attackTime;
        _timeAttack2 = _attackTime;
    }

    private void FixedUpdate()
    {
        bool peutGrab = false;
        string grabDir = "";
        if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3))
        {
            peutGrab = true;
            grabDir = "AggripeSol";
        }
        else if (Physics.Raycast(_rB.position, Vector3.up, 2f, 1 << 3))
        {
            peutGrab = true;
            grabDir = "AggripePlafond";
        }
        else if (Physics.Raycast(_rB.position, Vector3.left, 1f, 1 << 3))
        {
            peutGrab = true;
            grabDir = "AggripeMur";
        }
        else if (Physics.Raycast(_rB.position, Vector3.right, 1f, 1 << 3))
        {
            peutGrab = true;
            grabDir = "AggripeMur";
        }

        _grabbing = false;
        if (peutGrab)
        {
            if (_player1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        AudioManager.PlaySound("grab");
                    }
                    _rB.velocity = Vector3.zero;
                    _rB.isKinematic = true;
                    _rB.useGravity = false;
                    //_rB.Sleep();
                    _animator.SetBool(grabDir, true);
                    _grabbing = true;
                }
                else
                {
                    //_rB.WakeUp();
                    _rB.isKinematic = false;
                    _rB.useGravity = true;
                    _animator.SetBool("AggripeMur", false);
                    _animator.SetBool("AggripeSol", false);
                    _animator.SetBool("AggripePlafond", false);
                }
            }

            if (_player2)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        AudioManager.PlaySound("grab");
                    }
                    _rB.velocity = Vector3.zero;
                    _rB.isKinematic = true;
                    _rB.useGravity = false;
                    _animator.SetBool(grabDir, true);
                    _grabbing = true;
                }
                else
                {
                    //_rB.WakeUp();
                    _rB.isKinematic = false;
                    _rB.useGravity = true;
                    _animator.SetBool("AggripeMur", false);
                    _animator.SetBool("AggripeSol", false);
                    _animator.SetBool("AggripePlafond", false);
                }
            }
        }

        if (!_grabbing)
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

        if (Players[0].GetComponent<Transform>().position == transform.position)
        {
            if ((Players[1].GetComponent<Transform>().position - transform.position).magnitude <= 2)
            {
                _rope.RopeIsEnabled = true;
                Music[3].enabled = true;
                Music[2].enabled = false;
                _rope._timeRope = 0;
            }
        }
        else
        {

            if ((Players[0].GetComponent<Transform>().position - transform.position).magnitude <= 2)
            {
                _rope.RopeIsEnabled = true;
                Music[3].enabled = true;
                Music[2].enabled = false;
                _rope._timeRope = 0;
            }
        }


        if (!_grabbing)
        {
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
        }


        if(Input.GetKey(KeyCode.Space))
        {
            _rope.SegmentLength = _extendedLength;
            _rope.GetComponent<LineRenderer>().material = _extendedMaterial;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.PlaySound("coretensionmaximale");
            }
        }
        else
        {
            _rope.SegmentLength = _contractedLength;
            _rope.GetComponent<LineRenderer>().material = _normalMaterial;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                AudioManager.PlaySound("retractioncorde");
            }
        }
    }

    private void MovementPerso1()
    {
        if (!Physics.Raycast(_rB.position, Vector3.left, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.A))
            {
                _force.x -= 1.0f;
                if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3)) if (Random.Range(0, 100) == 1) AudioManager.PlaySound("cling");
            }
        }

        if (!Physics.Raycast(_rB.position, Vector3.right, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.D))
            {
                _force.x += 1.0f;
                if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3)) if (Random.Range(0, 100) == 1) AudioManager.PlaySound("cling");
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
                if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3)) if (Random.Range(0,100)==1)AudioManager.PlaySound("cling");
            }
        }

        if (!Physics.Raycast(_rB.position, Vector3.right, 0.8f, 1 << 3))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _force.x += 1.0f;
                if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3)) if (Random.Range(0, 100) == 1) AudioManager.PlaySound("cling");
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
            AudioManager.PlaySound("fshiou");
        }
    }

    private void SautPerso2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _jump = true;
            _rB.AddForce(Vector2.up * _jumpingSpeed, ForceMode.Impulse);
            _animator.SetBool("Jumping", true);
            AudioManager.PlaySound("fshiou");
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
            AudioManager.PlaySound("attackmode");
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
            AudioManager.PlaySound("attackmode");
            _timeSinceAttack2 = 0;
        }
    }

    private void Jump()
    {
        if (Physics.Raycast(_rB.position, Vector3.down, 2f, 1 << 3))
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
            //_stuckToWall = true;
            AudioManager.PlaySound("botkitapdenmure");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //_stuckToWall = false;
        }
    }

    public void BrokeRope()
    {
        _rope.RopeIsEnabled = false;
        AudioManager.PlaySound("cordequipete");
        Music[2].enabled = true;
        Music[3].enabled = false;
    }

}
