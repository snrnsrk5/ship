using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���ӱ� ��ũ��Ʈ
/// </summary>
public class EngineAcceleratorSkill : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    [SerializeField] ShipCtrl shipCtrl = null; // �� ������ �ڵ带 ������

    [Header("�⺻��ġ")]
    [SerializeField] float coolTime = 60f; // ���� ���ð�
    [SerializeField] float functioningTime = 60f; // ��ų ���ð�
    [SerializeField] float engineAcceleratorAmount = 1.2f; // ���� ���� ���� 1.2 = 20% ����

    [Header("�����ġ")]
    [SerializeField] float toCoolTime = 0f; // ���� ���� ���� ���ð�
    [SerializeField] float toFunctioningTime = 0f; // ���� ���� ��ų ���ð�
    [SerializeField] bool isUse = false; // ��ų�� ����ϰ� �ִ°�?

    [Header("�������̽�")]
    [SerializeField] Image fastIcon;
    [SerializeField] Image fastIconColor;

    [Header("Ű�ڵ�")]
    [SerializeField] KeyCode useskill = KeyCode.T;
    void Update()
    {
        // ��ų ��� Ű�� ������ ��ų�� ���� ���̸� ��Ÿ���� ������ ���� ��ų�Լ��� ����
        if (Input.GetKeyDown(useskill) && isUse == false && toCoolTime > coolTime) { Fast(); }
        // ��ų�� ������̰� ���ð��� �������� ���󺹱� �ڵ带 ����
        if (isUse == true && toFunctioningTime < 0f) { Normal(); }
        if(isUse == true) 
        {
            fastIconColor.color = new Color(0, 1, 0, 0.5f);
            useFastIcon(); 
        }
        if (isUse == false) 
        {
            fastIconColor.color = new Color(1, 0, 0, 0.5f);
            coolFastIcon(); 
        }
    }
    void FixedUpdate()
    {
        //��Ÿ�ӵ� ����
        toCoolTime += 0.02f;
        toFunctioningTime -= 0.02f;
    }

    /// <summary>
    /// ���� �߰� ���� �Լ�
    /// </summary>
    void Fast()
    {
        //�� �ְ�ӵ� ���� ��Ŵ 
        shipCtrl.EngineSkilltoJusticeSpd(engineAcceleratorAmount);
        // ��ų ���ð� �ʱ�ȭ
        toFunctioningTime = functioningTime;
        isUse = true;
    }

    /// <summary>
    /// ���� �߰� ���� ���� �Լ�
    /// </summary>
    void Normal()
    {
        // �����ӵ��� ����
        shipCtrl.EngineSkilltoJusticeSpd(1.0f);
        //��Ÿ�� �ʱ�ȭ
        toCoolTime = 0f;
        isUse = false;
    }

    /// <summary>
    /// ����߿��� �ʷϻ����� 
    /// </summary>
    void useFastIcon()
    {
        fastIcon.fillAmount = ((100 / functioningTime) * toFunctioningTime) / 100;
    }
    /// <summary>
    /// ��Ÿ���߿��� ����������
    /// </summary>
    void coolFastIcon()
    {
        fastIcon.fillAmount = ((100 / coolTime) * toCoolTime) / 100;
    }
}
