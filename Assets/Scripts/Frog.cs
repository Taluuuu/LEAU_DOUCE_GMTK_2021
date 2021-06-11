using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Frog : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject[] PlayerArray;
    public List<GameObject> PlayerList;
    private IEnumerator coroutine;
    private void Awake()
    {
        rb = transform.GetComponent­<Rigidbody>();
    }
    private void Start()
    {
        InvokeRepeating("Detection", 0.5f, 1.5f);
    }
    void Move(float Orientation)
    {
        rb.AddForce(new Vector2(Mathf.Abs((PlayerArray[0].transform.position - rb.position).normalized.x) * Orientation, 0.5f + (PlayerArray[0].transform.position - rb.position).normalized.y) * 4, ForceMode.Impulse);
    }
    private void Detection()
    {
        if (Physics.Raycast(rb.position, Vector3.down, 1f, 1 << 3))
        {
            PlayerArray = GameObject.FindGameObjectsWithTag("Player");
            PlayerArray = PlayerArray.OrderBy(w => (w.transform.position - rb.position).magnitude).ToArray();
            if (Mathf.Abs((PlayerArray[0].transform.position - rb.transform.position).magnitude) < 100)
            {
                if (PlayerArray[0].transform.position.x - rb.transform.position.x < 0)
                {
                    Move(-1f);
                }
                if (PlayerArray[0].transform.position.x - rb.transform.position.x > 0)
                {
                    Move(1f);
                }
            }
        }
    }
}