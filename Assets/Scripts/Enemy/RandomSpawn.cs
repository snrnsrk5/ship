using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab�� ���� public ���� �Դϴ�.
    int enmey = 1;
    void SpawnEnemy()
    {
        float randomX = Random.Range(-5000f, 5000f); //���� ��Ÿ�� X��ǥ�� �������� ������ �ݴϴ�.
        float randomY = Random.Range(-5000f, 5000f); //���� ��Ÿ�� Y��ǥ�� �������� ������ �ݴϴ�.
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
        InvokeRepeating("SpawnEnemy", 3, 0.5f); //3���� ����, SpawnEnemy�Լ��� 1�ʸ��� �ݺ��ؼ� ���� ��ŵ�ϴ�.
    }
}
