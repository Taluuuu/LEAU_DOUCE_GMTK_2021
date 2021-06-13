using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Frog : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject[] PlayerArray;
    private List<GameObject> PlayerList;
    private IEnumerator coroutine;
    public float Range;
    public float WaitTime;
    public float xspeed;
    public float yspeed;
    public float minimumyspeed;

    public Transform model;

    public Animator _animator;
    private void Awake()
    {
        rb = transform.GetComponent­<Rigidbody>();
    }
    private void Start()
    {
        InvokeRepeating("Detection", 0.5f, WaitTime);
    }
    private void Detection()
    {
        if (Physics.Raycast(rb.position, Vector3.down, 1f, 1 << 3))
        {
            PlayerArray = GameObject.FindGameObjectsWithTag("Player");
            PlayerArray = PlayerArray.OrderBy(w => (w.transform.position - rb.position).magnitude).ToArray();
            if (Mathf.Abs((PlayerArray[0].transform.position - rb.transform.position).magnitude) < Range)
            {
                if (Mathf.Abs(PlayerArray[0].transform.position.x) - Mathf.Abs(rb.transform.position.x) > 0) rb.rotation = Quaternion.Euler(0 ,180, 0);
                if (Mathf.Abs(PlayerArray[0].transform.position.x) - Mathf.Abs(rb.transform.position.x) < 0) rb.rotation = Quaternion.Euler(0, 0, 0);
                rb.AddForce(new Vector2((PlayerArray[0].transform.position - rb.position).normalized.x * xspeed, minimumyspeed + (PlayerArray[0].transform.position - rb.position).normalized.y) * yspeed, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _animator.SetBool("DansLesAirs", false);
    }

    private void OnCollisionExit(Collision collision)
    {
        _animator.SetBool("DansLesAirs", true);
    }
}