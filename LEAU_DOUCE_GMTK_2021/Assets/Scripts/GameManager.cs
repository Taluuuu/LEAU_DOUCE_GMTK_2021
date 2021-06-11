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


    public int CoinsCollected { get => _coinsCollected; }
    private int _coinsCollected = 0;

    // Events
    public UnityEvent _onStartGame;
    public UnityEvent _onCoinCollected;
    public UnityEvent _onPlayerFuckingDying;

    public GameManager()
    {
        _onStartGame = new UnityEvent();
        _onStartGame.AddListener(OnStartGame);

        _onCoinCollected = new UnityEvent();
        _onCoinCollected.AddListener(OnCoinCollected);

        _onPlayerFuckingDying = new UnityEvent();
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("TestScene").handle);
    }

    private void OnCoinCollected()
    {
        _coinsCollected++;
    }
}
