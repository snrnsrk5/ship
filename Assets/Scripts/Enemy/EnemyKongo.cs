using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKongo : MonoBehaviour
{
    int hp = 8000;
    void Start()
    {

    }

    void Update()
    {
        if (hp < 0)
        {
            EXPsystem.Instance.exp = 100;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        hp -= 2000;
    }
}
