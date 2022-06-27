using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoArmedCameraCtrl : OneArmedCameraCtrl
{
    [Header("어뢰축")]
    public GameObject[] secondArmed;
    public int orderSecondArmed = 0;

    [Header("스크립트")]
    public ArmsChange armsChange = null;
    protected override void NormalCamera()
    {
        if (armsChange.armsTypeState == ArmsChange.ArmsTypeState.MAIN)
        {
            //부모 함수 사용
            SmoothCamera(minZoom, mainArmed[orderMainArmed]);
            //무장 시선 바꾸기
            if (Input.GetKeyUp(KeyCode.C) && orderMainArmed < mainArmed.Length) { orderMainArmed++; }
            if (Input.GetKeyUp(KeyCode.C) && orderMainArmed == mainArmed.Length) { orderMainArmed = 0; }
        }

        if (armsChange.armsTypeState == ArmsChange.ArmsTypeState.TOP)
        {
            //부모 함수 사용
            SmoothCamera(minZoom, secondArmed[orderSecondArmed]);
            //무장 시선 바꾸기
            if (Input.GetKeyUp(KeyCode.C) && orderSecondArmed < secondArmed.Length) { orderSecondArmed++; }
            if (Input.GetKeyUp(KeyCode.C) && orderSecondArmed == secondArmed.Length) { orderSecondArmed = 0; }
            armsChange.TopLine(orderSecondArmed);
        }
    }
}
