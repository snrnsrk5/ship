using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab을 받을 public 변수 입니다.
    int enmey = 1;
    void SpawnEnemy()
    {
        float randomX = Random.Range(-5000f, 5000f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        float randomY = Random.Range(-5000f, 5000f); //적이 나타날 Y좌표를 랜덤으로 생성해 줍니다.
        if (enableSpawn)
        {
            if(-1000 < randomX && randomX > 1000 && -1000 < randomY && randomX > 1000 )
            {
            
            }
            else
            {
                GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomX, 7.5f, randomY), Quaternion.identity);
            }
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3, 0.5f); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행 시킵니다.
    }
}
