using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKongo : MonoBehaviour
{
    public int hp = 7000;
    public ParticleSystem effect;
    float diedTime = 3;

    void Start()
    {

    }

    void Update()
    {
        if (hp < 0)
        {
            EXPsystem.Instance.exp = 100;
            effect.gameObject.SetActive(true);

            //Destroy(gameObject, 5f);
            TimeDown();
        }
    }

    void TimeDown()
    {
        diedTime -= Time.deltaTime;
        if(diedTime < 0) { Destroy(gameObject); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        effect.gameObject.SetActive(true);

        TimeDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        hp -= 2000;
    }
}
