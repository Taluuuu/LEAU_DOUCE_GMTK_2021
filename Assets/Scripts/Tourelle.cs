using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _projSpeed;

    //public Player _player1;
    //public Player _player2;
    private UnityEngine.GameObject _player1;
    private UnityEngine.GameObject _player2;

    Vector3 _range1;
    Vector3 _range2;

    public ProjectileTourelle _projectileTourelle;

    private bool _meetPlayer1;
    private bool _meetPlayer2;

    private Ray _ray1;
    private Ray _ray2;

    private float _timeSinceShoot;

    private UnityEngine.GameObject[] Players;

    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        _player1 = Players[0];
        _player2 = Players[1];
    }

    // Update is called once per frame
    void Update()
    {

        _meetPlayer1 = false;
        _meetPlayer2 = false;

        _timeSinceShoot += Time.deltaTime;

        _range1 = _player1.transform.position - transform.position;
        _range2 = _player2.transform.position - transform.position;

        TestVew1();
        TestVew2();

        if (_ray1.direction.x > 0)
        {

            transform.Rotate(0, -180, 0);


        }

        if (_ray1.direction.x < 0)
        {


            transform.Rotate(0, 180, 0);

        }

        if (_ray2.direction.x > 0)
        {

            transform.rotation = Quaternion.Euler(0, 180, 0);


        }

        if (_ray2.direction.x < 0)
        {


            transform.rotation = Quaternion.Euler(0, 0, 0);

        }



        if (_meetPlayer1 && _meetPlayer2)
        {

            if (_range1.magnitude <= _range2.magnitude)
            {
                ShootPlayer1();
            }
            else
                ShootPlayer2();
        }
        else
            if(_meetPlayer1)
             {
                ShootPlayer1();
             }
              else
                if (_meetPlayer2)
                 {
                     ShootPlayer2();
                 }

        if (!_meetPlayer1 & !_meetPlayer2)
            _timeSinceShoot = 0;


    }

    private void TestVew1()
    {

        _ray1 = new Ray(transform.position, _range1);

        RaycastHit hit1;

        if (Physics.Raycast(_ray1, out hit1))
        {
            // Player has been hit by the raycast
            if (hit1.collider.CompareTag("Player"))
            {
                _meetPlayer1 = true;
            }

        }
    }

    private void TestVew2()
    {
        _ray2 = new Ray(transform.position, _range2);

        RaycastHit hit2;

        if (Physics.Raycast(_ray2, out hit2))
        {
            // Player has been hit by the raycast
            if (hit2.collider.CompareTag("Player"))
            {
                _meetPlayer2 = true;
            }

         }
    }

    private void ShootPlayer1()
    {
        if (_timeSinceShoot >= _shootSpeed)
        {
            _timeSinceShoot = 0;
            ProjectileTourelle _proj = Instantiate(_projectileTourelle, transform.position + _ray1.direction.normalized * 2, transform.rotation);


            _proj.GetComponent<Rigidbody>().velocity = _ray1.direction * _projSpeed;
        }
    }

    private void ShootPlayer2()
    {
        if (_timeSinceShoot >= _shootSpeed)
        {
            _timeSinceShoot = 0;

            ProjectileTourelle _proj = Instantiate(_projectileTourelle, transform.position + _ray2.direction.normalized * 2, transform.rotation);

            

            _proj.GetComponent<Rigidbody>().velocity = _ray2.direction * _projSpeed;
        }
    }
}
