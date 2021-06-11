using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance._onHit.AddListener(OnHit);
    }

    private void OnHit()
    {
        Destroy(gameObject);
    }
}