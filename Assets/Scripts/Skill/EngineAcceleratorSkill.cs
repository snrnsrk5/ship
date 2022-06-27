using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 엔진 가속기 스크립트
/// </summary>
public class EngineAcceleratorSkill : MonoBehaviour
{
    [Header("스크립트")]
    [SerializeField] ShipCtrl shipCtrl = null; // 배 움직임 코드를 가져옴

    [Header("기본수치")]
    [SerializeField] float coolTime = 60f; // 재사용 대기시간
    [SerializeField] float functioningTime = 60f; // 스킬 사용시간
    [SerializeField] float engineAcceleratorAmount = 1.2f; // 엔진 가속 지수 1.2 = 20% 증가

    [Header("현재수치")]
    [SerializeField] float toCoolTime = 0f; // 현재 남은 재사용 대기시간
    [SerializeField] float toFunctioningTime = 0f; // 현재 남은 스킬 사용시간
    [SerializeField] bool isUse = false; // 스킬을 사용하고 있는가?

    [Header("인터페이스")]
    [SerializeField] Image fastIcon;
    [SerializeField] Image fastIconColor;

    [Header("키코드")]
    [SerializeField] KeyCode useskill = KeyCode.T;
    void Update()
    {
        // 스킬 사용 키를 눌렀고 스킬을 비사용 중이며 쿨타임이 없을때 가속 스킬함수를 실행
        if (Input.GetKeyDown(useskill) && isUse == false && toCoolTime > coolTime) { Fast(); }
        // 스킬을 사용중이고 사용시간이 다했을때 원상복구 코드를 실행
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
        //쿨타임들 줄임
        toCoolTime += 0.02f;
        toFunctioningTime -= 0.02f;
    }

    /// <summary>
    /// 엔진 추가 가속 함수
    /// </summary>
    void Fast()
    {
        //배 최고속도 증가 시킴 
        shipCtrl.EngineSkilltoJusticeSpd(engineAcceleratorAmount);
        // 스킬 사용시간 초기화
        toFunctioningTime = functioningTime;
        isUse = true;
    }

    /// <summary>
    /// 엔진 추가 가속 종료 함수
    /// </summary>
    void Normal()
    {
        // 원래속도로 복구
        shipCtrl.EngineSkilltoJusticeSpd(1.0f);
        //쿨타임 초기화
        toCoolTime = 0f;
        isUse = false;
    }

    /// <summary>
    /// 사용중에는 초록색으로 
    /// </summary>
    void useFastIcon()
    {
        fastIcon.fillAmount = ((100 / functioningTime) * toFunctioningTime) / 100;
    }
    /// <summary>
    /// 쿨타임중에는 빨간색으로
    /// </summary>
    void coolFastIcon()
    {
        fastIcon.fillAmount = ((100 / coolTime) * toCoolTime) / 100;
    }
}
