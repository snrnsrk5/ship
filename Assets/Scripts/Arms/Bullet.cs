using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int spd;
    public float dis;

    public int deTime;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void Update()
    {        
        //프레임마다 오브젝트를 로컬좌표상에서 앞으로 1의 힘만큼 날아가라        
        transform.Translate(Vector3.up * spd * Time.deltaTime);

        //transform.LookAt(rayCastCtrl.cach);
        //transform.position = Vector3.MoveTowards(transform.position, rayCastCtrl.hit.point, spd * Time.deltaTime);

    }
}
