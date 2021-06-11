using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwompPlayerDetector : MonoBehaviour
{
    public Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Hit();
        }
    }
}
