using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsShoot : MonoBehaviour
{
    public ArmsChange armsChange;

    public float gunReLoadTime;
    public float cgunReLoadTime;
    public float[] gunTime;

    public float topReLoadTime;
    public float ctopReLoadTime;
    public float[] topTime;

    public Image[] gunReUI;
    public Text[] gunTexts;

    public Image[] topReUI;
    public Text[] topTexts;

    public GameObject he;
    public GameObject ap;
    public GameObject top;

    public Transform[] FirePos;
    public Transform[] topPos;

    int num = 0;
    int topnum = 0;
    bool check = true;

    public int gunBarrel;
    public int gunMount;
    public int topBarrel;

    void Awake()
    {
        cgunReLoadTime = gunReLoadTime;
        ctopReLoadTime = topReLoadTime;
        for (int i = 0; i < gunTime.Length; i++)
        {
            gunTime[i] = gunReLoadTime;
        }
        for (int i = 0; i < topTime.Length; i++)
        {
            topTime[i] = topReLoadTime;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            Shoot();
        }
        TimeDown();
        Reui();
    }

    void TimeDown()
    {
        for (int i = 0; i < gunTime.Length; i++)
        {
            if (gunTime[i] > 0)
            {
                gunTime[i] -= Time.deltaTime;
            }
            else
            {
                gunTime[i] = 0;
            }

        }
        for (int i = 0; i < topTime.Length; i++)
        {
            if (topTime[i] > 0)
            {
                topTime[i] -= Time.deltaTime;
            }
            else
            {
                topTime[i] = 0;
            }
        }
    }


    void Shoot()
    {
        if (Input.GetMouseButton(0) && gunTime[num] == 0 && check && armsChange.armsType == 0)
        {
            check = false;
            StartCoroutine(WaitForIt());
            gunTime[num] = gunReLoadTime;
            //복제한다. //'Bullet'을 'FirePos.transform.position' 위치에 'FirePos.transform.rotation' 회전값으로.   
            if (armsChange.armsCach == 0)
            {
                if(gunBarrel == 1)
                {
                    Instantiate(he, FirePos[num].transform.position, FirePos[num].transform.rotation);
                }
                if (gunBarrel == 2)
                {
                    Instantiate(he, FirePos[num].transform.position + new Vector3(0, 0, 1f), FirePos[num].transform.rotation);
                    Instantiate(he, FirePos[num].transform.position + new Vector3(0, 0, -1f), FirePos[num].transform.rotation);
                }
            }
            if (armsChange.armsCach == 1)
            {
                if (gunBarrel == 1)
                {
                    Instantiate(ap, FirePos[num].transform.position, FirePos[num].transform.rotation);
                }
                if (gunBarrel == 2)
                {
                    Instantiate(ap, FirePos[num].transform.position + new Vector3(0, 0, 1f), FirePos[num].transform.rotation);
                    Instantiate(ap, FirePos[num].transform.position + new Vector3(0, 0, -1f), FirePos[num].transform.rotation);
                }
            }

            num++;
            if (num == gunTime.Length - gunMount)
            {
                num = 0;
            }
        }

        if (Input.GetMouseButton(0) && topTime[topnum] == 0 && check && armsChange.armsType == 1)
        {
            check = false;
            StartCoroutine(WaitForIt());
            topTime[topnum] = topReLoadTime;

            switch (topBarrel)
            {
                case 2:
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-0.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -1));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(0.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 1));
                    break;
                case 3:
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-1f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -1.5f));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(0, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 0));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(1f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 1.5f));
                    break;
                case 4:
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-1.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -3));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-0.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -1));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(0.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 1));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(1.5f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 3));
                    break;
                case 5:
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-2f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -3f));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(-1f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, -1.5f));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(0, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 0));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(1f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 1.5f));
                    Instantiate(top, topPos[topnum].transform.position + new Vector3(2f, 0, 0), topPos[topnum].transform.rotation * Quaternion.Euler(0, 0, 3f));
                    break;
            }
            topnum++;
            if (topnum == topTime.Length)
            {
                topnum = 0;
            }
        }
    }

    public void Reui()
    {
        for (int i = 0; i < gunTime.Length; i++)
        {
            gunReUI[i].fillAmount = gunTime[i] / gunReLoadTime;

            if (gunTime[i] < 10)
            {
                gunTexts[i].text = string.Format("{0:0.0#}s", Mathf.Round(gunTime[i] * 10) * 0.1f);
            }
            else
            {
                gunTexts[i].text = string.Format("{0:#}s", gunTime[i]);
            }
        }
        for (int i = 0; i < topTime.Length; i++)
        {
            topReUI[i].fillAmount = topTime[i] / topReLoadTime;

            if (topTime[i] < 10)
            {
                topTexts[i].text = string.Format("{0:0.0#}s", Mathf.Round(topTime[i] * 10) * 0.1f);
            }
            else
            {
                topTexts[i].text = string.Format("{0:#}s", topTime[i]);
            }
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.2f);
        check = true;
    }

    public void Statsjustice(float maxHpPer)
    {
        gunReLoadTime = cgunReLoadTime / maxHpPer;
        topReLoadTime = ctopReLoadTime / maxHpPer;
    }

}
