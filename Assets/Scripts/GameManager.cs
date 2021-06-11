using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager
{
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                _instance = new GameManager();

            return _instance;
        }
    }

    private static GameManager _instance;


    // Events
    public UnityEvent _onTwompDetect;
    public UnityEvent _onTwompNoDetect;
    public UnityEvent _onHit;

    public GameManager()
    {
        _onTwompDetect = new UnityEvent();

        _onTwompNoDetect = new UnityEvent();
        _onHit = new UnityEvent();
    }
}
