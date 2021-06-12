using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlayerDetector : MonoBehaviour
{
    private Rigidbody rb;
    public Player Player1;
    public Player Player2;
    private void Awake()
    {
        rb = transform.GetComponent­<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((Player1.transform.position-rb.position).magnitude > (Player2.transform.position - rb.position).magnitude)
            {
                Player2.Hit();
            }
            if ((Player2.transform.position - rb.position).magnitude > (Player1.transform.position - rb.position).magnitude)
            {
                Player1.Hit();
            }
        }
    }
}
