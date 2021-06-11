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

    public GameManager()
    {
        _onTwompDetect = new UnityEvent();
        _onTwompDetect.AddListener(OnTwompDetect);

        _onTwompNoDetect = new UnityEvent();
        _onTwompNoDetect.AddListener(OnTwompNoDetect);
    }
}
