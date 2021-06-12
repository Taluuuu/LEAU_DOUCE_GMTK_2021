using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private float _numberPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _numberPlayer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_numberPlayer == 2)
        {
            Scene _thisScene = SceneManager.GetActiveScene();
            char _numberOne = _thisScene.name[7];
            char _numberTwo = _thisScene.name[8];

            int _numbers = ((int)_numberOne-48)*10 + (int)_numberTwo-48;
            _numbers++;

            

            if (_numbers >= 10)
                SceneManager.LoadScene("Niveau "+ _numbers.ToString(), LoadSceneMode.Single);
            else
                SceneManager.LoadScene("Niveau " + 0 + "" + _numbers.ToString(), LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _numberPlayer++;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _numberPlayer--;

        }
    }
}
