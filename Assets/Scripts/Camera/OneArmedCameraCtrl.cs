using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneArmedCameraCtrl : CameraCtrl
{
    [Header("������")]
    public GameObject[] mainArmed;
    public int orderMainArmed = 0;

    //ù��° ��� ����
    protected override void NormalCamera()
    {
        //�θ� �Լ� ���
        SmoothCamera(minZoom, mainArmed[orderMainArmed]);
        //���� �ü� �ٲٱ�
        if (Input.GetKeyUp(KeyCode.C) && orderMainArmed < mainArmed.Length) { orderMainArmed++; }
        if (Input.GetKeyUp(KeyCode.C) && orderMainArmed == mainArmed.Length) { orderMainArmed = 0; }
    }
}
