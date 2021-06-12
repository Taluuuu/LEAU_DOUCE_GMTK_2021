using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{

    public bool isTethered;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTethered) 
        {

            //Destroy(other.gameObject);
            
        
        
        }
    }

}
