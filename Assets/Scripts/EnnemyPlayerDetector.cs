using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlayerDetector : MonoBehaviour
{

    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hit();
        }
    }
}
