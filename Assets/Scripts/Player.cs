using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Movement movement = GetComponent<Movement>();
        //movement._ball (qui est un bool) pour savoir si le joueur est ball ou non
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
