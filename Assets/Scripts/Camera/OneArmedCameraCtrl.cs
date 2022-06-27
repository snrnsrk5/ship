using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneArmedCameraCtrl : CameraCtrl
{
    [Header("함포축")]
    public GameObject[] mainArmed;
    public int orderMainArmed = 0;

    //첫번째 장비 고정
    protected override void NormalCamera()
    {
        //부모 함수 사용
        SmoothCamera(minZoom, mainArmed[orderMainArmed]);
        //무장 시선 바꾸기
        if (Input.GetKeyUp(KeyCode.C) && orderMainArmed < mainArmed.Length) { orderMainArmed++; }
        if (Input.GetKeyUp(KeyCode.C) && orderMainArmed == mainArmed.Length) { orderMainArmed = 0; }
    }
}
