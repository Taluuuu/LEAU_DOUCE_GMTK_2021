using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
        //movement._ball (qui est un bool) pour savoir si le joueur est ball ou non
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Transform>().position.y <= -20)
        {
            Hit();
        }
    }

    public void Hit()
    {

        GameManager.Instance._onDead.Invoke();

        Destroy(gameObject);

    }

}
