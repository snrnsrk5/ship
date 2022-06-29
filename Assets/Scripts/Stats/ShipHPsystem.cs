using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShipHPsystem : MonoBehaviour
{
    public int maxHp;
    public float maxHpf;
    public int hp;

    public float autoRecoveryPer;

    public Text hpText;
    public Image hpBar;

    public Text died;
    public Button quit;
    void Awake()
    {
        died.gameObject.SetActive(false);
        hp = maxHp;
        maxHpf = maxHp;
        InvokeRepeating("AutoRecovery", 0f, 1f);
    }

    void Update()
    {
        HpUI();
        MinHp();
        
    }

    private void HpUI()
    {
        float _hp = hp;
        float _maxHp = maxHp;
        float run = _hp / _maxHp;
        hpText.text = hpText.text = string.Format($"{hp} / {maxHp}");;
        if (run > 0.66f)
        {
            hpBar.color = new Color(200f / 255f, 255f / 255f, 200f / 255f);

        }
        if (run > 0.33f && run < 0.66f)
        {
            hpBar.color = new Color(255f / 255f, 255f / 255f, 200f / 255f);

        }
        if (run < 0.33f)
        {
            hpBar.color = new Color(255f / 255f, 200f / 255f, 200f / 255f);
        }

        hpBar.DOFillAmount(run, 0.1f).SetEase(Ease.InQuad);
        hpText.text = string.Format($"{hp} / {maxHp}");
    }
    private void OnCollisionEnter(Collision collision)
    {
        hp = 0;
        
    }

    public void MinHp()
    {
        if(hp <= 0)
        {
            died.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Statsjustice(float maxHpPer)
    {
        maxHp = (int)(maxHpf * maxHpPer);
    }

    public void AutoRecovery()
    {
        if(hp < maxHp)
        {
            hp += (int)(maxHp * autoRecoveryPer);
        }
        else
        {
            hp = maxHp;
        }

    }

    public void Quits()
    {
        Application.Quit();
    }
}
