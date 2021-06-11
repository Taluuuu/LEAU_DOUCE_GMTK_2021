using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwompPlayerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance._OnHit.Invoke();
        }
    }
}
