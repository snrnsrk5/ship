using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoArmedCameraCtrl : OneArmedCameraCtrl
{
    [Header("�����")]
    public GameObject[] secondArmed;
    public int orderSecondArmed = 0;

    [Header("��ũ��Ʈ")]
    public ArmsChange armsChange = null;
    protected override void NormalCamera()
    {
        if (armsChange.armsTypeState == ArmsChange.ArmsTypeState.MAIN)
        {
            //�θ� �Լ� ���
            SmoothCamera(minZoom, mainArmed[orderMainArmed]);
            //���� �ü� �ٲٱ�
            if (Input.GetKeyUp(KeyCode.C) && orderMainArmed < mainArmed.Length) { orderMainArmed++; }
            if (Input.GetKeyUp(KeyCode.C) && orderMainArmed == mainArmed.Length) { orderMainArmed = 0; }
        }

        if (armsChange.armsTypeState == ArmsChange.ArmsTypeState.TOP)
        {
            //�θ� �Լ� ���
            SmoothCamera(minZoom, secondArmed[orderSecondArmed]);
            //���� �ü� �ٲٱ�
            if (Input.GetKeyUp(KeyCode.C) && orderSecondArmed < secondArmed.Length) { orderSecondArmed++; }
            if (Input.GetKeyUp(KeyCode.C) && orderSecondArmed == secondArmed.Length) { orderSecondArmed = 0; }
            armsChange.TopLine(orderSecondArmed);
        }
    }
}
