using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInfo : MonoBehaviour
{
    ShipCtrl shipCtrl;
    ArmsShoot armsShoot;
    GUIStyle style = new GUIStyle();

    public string shipName;
    public Text shipNameText;

    float rect_pos_x = 10f;
    float rect_pos_y = 10f;
    float w = Screen.width;
    float h = 30f;

    private void Awake()
    {
        shipCtrl = GetComponent<ShipCtrl>();
        armsShoot = GetComponent<ArmsShoot>();
        DontDestroyOnLoad(gameObject);

        style.normal.textColor = Color.green;
        style.fontSize = 30;
    }
    private void Update()
    {
        shipNameText.text = string.Format($"{shipName}");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(rect_pos_x, rect_pos_y, w, h), "�ְ�ӵ� : " + (Mathf.Round(shipCtrl._maxNotInt * 10) * 0.1f).ToString(), style);
        GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 1, w, h), "���ӷ� : " + (Mathf.Round(shipCtrl._accelerationInt * 10) * 0.1f).ToString(), style);
        GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 2, w, h), "��Ÿ�ð� : " + (Mathf.Round(shipCtrl._steeringTime * 10) * 0.1f).ToString(), style);
        GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 3, w, h), "���������ð� : " + (Mathf.Round(armsShoot.gunReLoadTime * 10)*0.1f).ToString(), style);
        GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 4, w, h), "��������ð� : " + (Mathf.Round(armsShoot.topReLoadTime * 10)*0.1f).ToString(), style);
        //GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 0, w, h), "���콺 �������� ���� ��Ʈ�� Ȧ��", style);
    }
}
