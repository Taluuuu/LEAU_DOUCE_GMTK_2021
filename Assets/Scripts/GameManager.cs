using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float _time;
    private float _timeBeforeReset = 2;
    private bool _dead;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                GameObject gameObject = new GameObject();
                DontDestroyOnLoad(gameObject);
                _instance = gameObject.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    private static GameManager _instance;

    public UnityEvent _onDead;

    public GameManager()
    {
        _onDead = new UnityEvent();
        _onDead.AddListener(OnDead);
    }

    // Update is called once per frame
    void Update()
    {
        if (_time >= _timeBeforeReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _time = 0;
            Time.timeScale = 1;
            _dead = false;
        }
        else
            if (_dead)
            {
                _time += Time.unscaledDeltaTime;
                Time.timeScale = 0;
            }
    }

    public void OnDead()
    {
        _dead = true;
    }
}
