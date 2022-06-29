using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArmsChange : MonoBehaviour
{

    [Header("스크립트")]
    public CameraCtrl cameraCtrl;
    public ArmsShoot armsShoot;

    [Header("무기타입")]
    public int armsType = 1;
    public int armsCach;

    public int heap = 0;

    public enum ArmsTypeState { MAIN, TOP };
    public ArmsTypeState armsTypeState = ArmsTypeState.MAIN;
    public enum ArmsState { HE, AP, SAP, TOP, UTOP };
    public ArmsState armsState = ArmsState.HE;

    [Header("인터페이스")]
    [SerializeField] private Image[] armsTypeImage;
    [SerializeField] public Image armsTypeLine;
    [SerializeField] public GameObject[] topLine;

    public GameObject topCan;
    public GameObject gunCan;

    void Start()
    {
        for (int i = 0; i < armsTypeImage.Length; i++)
        {
            armsTypeImage[i].rectTransform.anchoredPosition = new Vector2((-50 * (armsTypeImage.Length - 1)) + (100 * i), 100);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && armsCach != 0)
        {
            if (heap == 1)
            {
                for (int a = 0; a < armsShoot.gunTime.Length; a++)
                {
                    for (int i = 0; i < armsShoot.gunTime.Length; i++)
                    {
                        armsShoot.gunTime[i] = armsShoot.gunReLoadTime;
                    }
                }
            }
            TopLine(-1);
            if (armsType != 0)
            {
                cameraCtrl.distance = 110;
                cameraCtrl.zoomDistanceLevel = 0;
                gunCan.SetActive(true);
                topCan.SetActive(false);
            }
            armsState = ArmsState.HE;
            armsTypeState = ArmsTypeState.MAIN;
            armsType = 0;
            armsCach = 0;
            heap = 0;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && armsCach != 1)
        {
            if (heap == 0)
            {
                for (int a = 0; a < armsShoot.gunTime.Length; a++)
                {
                    for (int i = 0; i < armsShoot.gunTime.Length; i++)
                    {
                        armsShoot.gunTime[i] = armsShoot.gunReLoadTime;
                    }
                }
            }
            TopLine(-1);
            if (armsType != 0)
            {
                cameraCtrl.distance = 110;
                cameraCtrl.zoomDistanceLevel = 0;
                gunCan.SetActive(true);
                topCan.SetActive(false);
            }
            armsState = ArmsState.AP;
            armsTypeState = ArmsTypeState.MAIN;
            armsType = 0;
            armsCach = 1;
            heap = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && armsCach != 2)
        {
            TopLine(0);
            if (armsType != 1)
            {
                cameraCtrl.distance = 110;
                cameraCtrl.zoomDistanceLevel = 0;
                gunCan.SetActive(false);
                topCan.SetActive(true);

            }
            armsState = ArmsState.TOP;
            armsTypeState = ArmsTypeState.TOP;
            armsType = 1;
            armsCach = 2;
        }
        armsTypeLine.rectTransform.anchoredPosition = new Vector2(((-50 * (armsTypeImage.Length - 1)) + (100 * armsCach)), 100);
    }

    /// <summary>
    /// 어뢰 판정라인 끄고켜기 함수
    /// </summary>
    /// <param name="num">몇번째 어뢰 판정 라인인지 ( -1 는 모두 끔 )</param>
    public void TopLine(int num)
    {
        // 모든 오브젝트 끄고
        for (int fj = 0; fj < topLine.Length; fj++)
        {
            topLine[fj].SetActive(false);
        }

        //필요한거 하나만 키기
        if (num != -1) { topLine[num].SetActive(true); }
    }
}
