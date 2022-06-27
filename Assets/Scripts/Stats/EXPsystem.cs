using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPsystem : MonoBehaviour
{
    public StatsUI statsUI;
    public Image expBar;
    public Text shipLvText;
    public Text expText;

    public float shipLv;
    public float exp;

    public static EXPsystem Instance;

    private float maxexp;
    public int plusExp;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("EXPsystem is multiple running!");
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        maxexp = 100;// * shipLv * 1.2f;
        shipLvText.text = string.Format($"LV {shipLv}");
        expText.text = string.Format("{0:0.0#}%", (exp / maxexp) *100);
        expBar.fillAmount = exp / maxexp;
        exp += plusExp * Time.deltaTime;
        if(maxexp < exp)
        {
            exp = 0;
            shipLv++;
            statsUI.plusStats++;
        }
    }
}
