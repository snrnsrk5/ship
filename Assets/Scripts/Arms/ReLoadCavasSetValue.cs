using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ƽ� ���� ���� ���� UI ��ȭ ��ũ��Ʈ
/// </summary>
public class ReLoadCavasSetValue : MonoBehaviour
{
    public GameObject[] gun;
    public GameObject[] realGun;
    public int gunMount;

    void Update()
    {
        switch (gunMount)
        {
            case 1:
                gun[0].SetActive(true);
                gun[1].SetActive(false);
                gun[2].SetActive(false);
                gun[3].SetActive(false);
                gun[4].SetActive(false);
                realGun[0].SetActive(true);
                realGun[1].SetActive(false);
                realGun[2].SetActive(false);
                realGun[3].SetActive(false);
                realGun[4].SetActive(false);
                break;
            case 2:
                gun[0].SetActive(true);
                gun[1].SetActive(true);
                gun[2].SetActive(false);
                gun[3].SetActive(false);
                gun[4].SetActive(false);
                realGun[0].SetActive(true);
                realGun[1].SetActive(true);
                realGun[2].SetActive(false);
                realGun[3].SetActive(false);
                realGun[4].SetActive(false);
                break;
            case 3:
                gun[0].SetActive(true);
                gun[1].SetActive(true);
                gun[2].SetActive(true);
                gun[3].SetActive(false);
                gun[4].SetActive(false);
                realGun[0].SetActive(true);
                realGun[1].SetActive(true);
                realGun[2].SetActive(true);
                realGun[3].SetActive(false);
                realGun[4].SetActive(false);
                break;
            case 4:
                gun[0].SetActive(true);
                gun[1].SetActive(true);
                gun[2].SetActive(true);
                gun[3].SetActive(true);
                gun[4].SetActive(false);
                realGun[0].SetActive(true);
                realGun[1].SetActive(true);
                realGun[2].SetActive(true);
                realGun[3].SetActive(true);
                realGun[4].SetActive(false);
                break;
            case 5:
                gun[0].SetActive(true);
                gun[1].SetActive(true);
                gun[2].SetActive(true);
                gun[3].SetActive(true);
                gun[4].SetActive(true);
                realGun[0].SetActive(true);
                realGun[1].SetActive(true);
                realGun[2].SetActive(true);
                realGun[3].SetActive(true);
                realGun[4].SetActive(true);
                break;
        }
    }
}
