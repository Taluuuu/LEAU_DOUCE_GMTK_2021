using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwompDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance._onTwompDetect.Invoke();
        }
    }

    private void OnTriggerLeave(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance._onTwompNoDetect.Invoke();
        }
    }
}
