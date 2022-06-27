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
        float randomX = Random.Range(-1000f, 1000f); //���� ��Ÿ�� X��ǥ�� �������� ������ �ݴϴ�.
        float randomY = Random.Range(-1000f, 1000f); //���� ��Ÿ�� Y��ǥ�� �������� ������ �ݴϴ�.
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomX, 7.5f, randomY), Quaternion.identity); //������ ��ġ��, ȭ�� ���� ������ Enemy�� �ϳ� �������ݴϴ�.
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3, 1); //3���� ����, SpawnEnemy�Լ��� 1�ʸ��� �ݺ��ؼ� ���� ��ŵ�ϴ�.
    }
}
