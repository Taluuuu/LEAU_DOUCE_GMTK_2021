using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private bool _bar1;
    [SerializeField] private bool _bar2;
    [SerializeField] private bool _bar3;

    private UnityEngine.GameObject _player1;
    private UnityEngine.GameObject _player2;
    private UnityEngine.GameObject[] Players;


    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        if (Players[0].GetComponent<Movement>()._player1)
        {
            _player1 = Players[0];
            _player2 = Players[1];
        }
        else
        {
            _player1 = Players[1];
            _player2 = Players[0];
        }


    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        if (Players.Length > 1)
        {

            if (_bar1)
            {
                gameObject.GetComponent<Slider>().value = _player1.GetComponent<Movement>()._timeAttack1 / _player1.GetComponent<Movement>()._attackTime;
            }

            if (_bar2)
            {

                gameObject.GetComponent<Slider>().value = _player2.GetComponent<Movement>()._timeAttack2 / _player2.GetComponent<Movement>()._attackTime;
            }

            if(_bar3)
            {

                gameObject.GetComponent<Slider>().value = (_player1.GetComponent<Movement>()._rope._ropeCooldown - _player1.GetComponent<Movement>()._rope._timeRope) /_player1.GetComponent<Movement>()._rope._ropeCooldown;
            }
        }
    }

}
