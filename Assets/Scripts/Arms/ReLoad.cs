using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReLoad : MonoBehaviour
{
    public float reloadTime;
    public float time;
    public Image thisReUI;
    public Text gunTexts;
    public AudioSource cannon;
    void Awake()
    {
        time = reloadTime;
    }
    void Update()
    {
        Reui();
        TimeDown();

    }
    void TimeDown()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }
    }
    public void Reui()
    {
        thisReUI.fillAmount = time / reloadTime;

        if (time < 10)
        {
            gunTexts.text = string.Format("{0:0.0#}s", Mathf.Round(time * 10) * 0.1f);
        }
        else
        {
            gunTexts.text = string.Format("{0:#}s", time);
        }

    }

    public void CannonSound()
    {
        cannon.Play();
    }
}
