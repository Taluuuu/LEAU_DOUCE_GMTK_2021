using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlayerDetector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Movement>()._ball)
            {
                Destroy(gameObject);
            }
            else
                other.GetComponent<Player>().Hit();
        }
    }
}
