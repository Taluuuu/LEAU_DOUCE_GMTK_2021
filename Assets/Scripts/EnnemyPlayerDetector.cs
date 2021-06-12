using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPlayerDetector : MonoBehaviour
{
    private Rigidbody rb;
    private Player[] Players;
    private void Awake()
    {
        rb = transform.GetComponent­<Rigidbody>();
        Players = Player.FindObjectsOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((Players[0].transform.position-rb.position).magnitude > (Players[1].transform.position - rb.position).magnitude)
            {
                Players[1].Hit();
            }
            if ((Players[1].transform.position - rb.position).magnitude > (Players[0].transform.position - rb.position).magnitude)
            {
                Players[0].Hit();
            }
        }
    }
}
