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
            {
                if (other.GetComponent<Movement>()._ropeBool)
                {
                    other.GetComponent<Movement>().BrokeRope();
                }
                else
                    other.GetComponent<Player>().Hit();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.CompareTag("Player"))
        {
            if(other.collider.GetComponent<Movement>()._ball)
            {
                Destroy(gameObject);
            }
            else
            {
                if(other.collider.GetComponent<Movement>()._ropeBool)
                {
                    other.collider.GetComponent<Movement>().BrokeRope();
                }
                else
                    other.collider.GetComponent<Player>().Hit();
            }
                
        }
    }
}
