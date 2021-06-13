using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlayerDetector : MonoBehaviour
{
    [SerializeField] public ParticleSystem dust;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") )
        {
            if (other.GetComponent<Movement>()._ball)
            {
                dust.Play();
                Destroy(gameObject);
            }
            else
            {
                if (other.GetComponent<Movement>()._rope.RopeIsEnabled)
                {
                    other.GetComponent<Movement>().BrokeRope();
                }
                else
                {
                    other.GetComponent<Player>().Hit();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.CompareTag("Player") & !gameObject.CompareTag("Ball"))
        {
            if (other.collider.GetComponent<Movement>()._ball)
            {
                dust.Play();
                Destroy(gameObject);
            }
            else
            {
                if(other.collider.GetComponent<Movement>()._rope.RopeIsEnabled)
                {
                    other.collider.GetComponent<Movement>().BrokeRope();
                }
                else
                    other.collider.GetComponent<Player>().Hit();
            }
                
        }
    }
}
