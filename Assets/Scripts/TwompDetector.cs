using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwompDetector : MonoBehaviour
{
    public Twomp _twomp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _twomp.TwompDetect();
            _twomp.SetDetectorHeight(transform.position);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _twomp.TwompNoDetect();
        }
    }
}
