using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 Direction;
    public bool switched;
    private IEnumerator coroutine;

    private void Awake()
    {
        rb = transform.GetComponent­<Rigidbody>();
    }
    void Start()
    {
        switched = false;
        Direction = Vector3.right;
        Debug.Log(Quaternion.Euler(0, 0, -90) * Direction);
    }

    void Update()
    {
        if (switched == false)
        {
            if (Physics.Raycast(rb.position + Quaternion.Euler(0, 0, -90) * Direction * (-0.1f+rb.transform.lossyScale.x/2) - Direction * (rb.transform.lossyScale.x / 2), Quaternion.Euler(0, 0, -90) * Direction, 0.3f, 1 << 3)==false)
            {
                Direction = Quaternion.Euler(0, 0, -90) * Direction;

                StartCoroutine(Wait());
            }
            if (Physics.Raycast(rb.position + Direction  *(-0.1f + rb.transform.lossyScale.x/2), Direction, 0.3f, 1 << 3)==true)
            {
                Direction = Quaternion.Euler(0, 0, 90) * Direction;

                StartCoroutine(Wait());
            }
        }
        rb.velocity=Direction*5;
    }
    private IEnumerator Wait()
    {
        switched = true;
        yield return new WaitForSeconds(1.0f);
        switched = false;
    }
}
