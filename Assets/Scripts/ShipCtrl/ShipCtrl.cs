using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 배 움직임 관련 부모 클래스
/// </summary>
/*abstract*/
public abstract class ShipCtrl : MonoBehaviour
{
    [Header("기본수치")]
    public float maxNot = 32.5f; //기본단위 노트
    public float acceleration = 1.0f; // 초당 올라가는 최대 노트수
    public float steeringTime = 6.5f; // 전타에 걸리는 시간
    public float steeringRadius = 700.0f; // 180도 회전 거리

    [Header("조작장치")]
    [Range(-1, 4)] public int accel = 0; // 현재 악셀 단계
    [Range(-2, 2)] public int steeringAccel = 0; // 현재 조타 악셀 단계

    protected float lFull, lHalf, lrStop, rFull, rHalf; // 조타할때 위와 동일
    protected bool isSteeringCtrl = false; // AD 키 감지하기 위에 씀

    [Header("계산장치")]
    //계산 편하게 하기위한 최종 변수
    [SerializeField] protected float maxNotInt; // 32.5노트가 3250 이 됨
    [SerializeField] protected float realMaxNotInt; // 조타시 속도가 느려지는것을 사용하기위한 변수
    [SerializeField] protected float accelerationInt; // 2.5가 250이 됨
    [SerializeField] protected float realSteeringRadius; // 전타시 걸리는 시간에 따른 속도와 거리에 상관때문에 씀

    [Header("현재상태")]
    [SerializeField] protected float not = 0; // 현 노트 (속도)
    [SerializeField] protected float steering = 0; // 현 조타량

    [Header("스탯상태")]
    // 스탯 레벨에 따른 수치
    [SerializeField] protected float maxNotSt;
    [SerializeField] protected float accelerationSt;
    [SerializeField] protected float steeringTimeSt;

    [Header("인터페이스")]
    //노트수와 조타량 표시 텍스트
    [SerializeField] protected Text ktsText = null;
    [SerializeField] protected Text jtText = null;
    [SerializeField] protected Image KtsLine = null;
    [SerializeField] protected Image jtLine = null;

    void Awake()
    {
        // 기본 정의
        InvokeRepeating("Kts", 0f, 0.5f);
    }
    void Start()
    {
        JusticeSpdAndStr();
    }
    /// <summary>
    /// 조작관련은 여따가 넣자!
    /// </summary>
    void Update()
    {
        Accel();
        SteeringAccel();
        //Kts();
        Jt();
        GetSetInfo();
        RealRotation();
    }
    /// <summary>
    /// 물리관련 함수는 여따가 넣자!
    /// </summary>
    void FixedUpdate()
    {
        Acceleration();
        SteeringAcceleration();
        RealMove();
        
    }

    /// <summary>
    /// 조타 및 최대속도 비례 단위
    /// </summary>
    public void JusticeSpdAndStr()
    {
        //소수점 없애기 위해서 사용 
        maxNotInt = maxNotSt * 100f;
        accelerationInt = accelerationSt * 100f;
        // 반경에 속도에 따른 전타량을 입력
        realSteeringRadius = (58.5f * maxNotInt * 0.01f) / (steeringRadius * 0.01f);

        //조타량 수치 조절
        rFull = realSteeringRadius * +1.0f;
        rHalf = realSteeringRadius * +0.5f;
        lrStop = realSteeringRadius * +0.0f;
        lHalf = realSteeringRadius * -0.5f;
        lFull = realSteeringRadius * -1.0f;

    }

    /// <summary>
    /// 엔진가속기 사용시 쓸 함수
    /// </summary>
    /// <param name="per">가속될 배율</param>
    public void EngineSkilltoJusticeSpd(float per)
    {
        maxNotInt = maxNotInt * per;
    }

    /// <summary>
    /// 스탯 정의함수
    /// </summary>
    /// <param name="maxNotLV"></param>
    /// <param name="acLV"></param>
    /// <param name="strTimeLV"></param>
    public void StatstoJustice(float maxNotPer, float acPer, float strTimePer)
    {
        maxNotSt = maxNot * maxNotPer;
        accelerationSt = acceleration * acPer;
        steeringTimeSt = steeringTime / strTimePer;
        JusticeSpdAndStr();
    }

    /// <summary>
    /// 스탯 정의함수(트리전용)
    /// </summary>
    public void StatstoJustice()
    {
        maxNotSt = maxNot;
        accelerationSt = acceleration;
        steeringTimeSt = steeringTime;
        JusticeSpdAndStr();
    }

    /// <summary>
    /// 실제로 움직일때 사용하는 함수
    /// </summary>
    void RealMove()
    {
        // *1.852f 는 km/h 단위로 변환 /3600 은 초단위로 계산하기위해
        realMaxNotInt = maxNotInt / (1 + ((Mathf.Abs(steering) / realSteeringRadius) / 2f));
        transform.Translate(new Vector3(not / 3600 * 1.852f, 0, 0));
    }

    /// <summary>
    /// 실제로 조타할때 사용하는 함수
    /// </summary>
    void RealRotation()
    {
        // 현 속도 대비 조타량을 조절하기 위함
        float realSteering = steering * Time.deltaTime * 0.05f * (not / maxNotInt);
        transform.Rotate(new Vector3(0f, realSteering, 0f));
    }

    /// <summary>
    /// 악셀 변경하는 함수
    /// </summary>
    void Accel()
    {
        if (Input.GetKeyDown(KeyCode.W) && accel != 4) { accel += 1; }
        else if (Input.GetKeyDown(KeyCode.S) && accel != -1) { accel -= 1; }

        KtsLine.rectTransform.anchoredPosition = new Vector2(-100, 110 + (accel * 50));
    }

    /// <summary>
    /// 조타악셀 변경 함수
    /// </summary>
    void SteeringAccel()
    {
        // Q는 왼쪽 (-) E 는 오른쪽 (+) 눌르면 유지됨
        if (Input.GetKeyDown(KeyCode.E) && steeringAccel != 2) { SteeringAccelCtrl(false, 1); }
        else if (Input.GetKeyDown(KeyCode.Q) && steeringAccel != -2) { SteeringAccelCtrl(false, -1); }

        // A는 왼쪽 (-) D 는 오른쪽 (+) 때면 0으로 돌아옴
        else if (Input.GetKey(KeyCode.D)) { SteeringAccelCtrl(true, 2); }
        else if (Input.GetKey(KeyCode.A)) { SteeringAccelCtrl(true, -2); }
        // 0으로 돌아오게하는 조건
        else { if (isSteeringCtrl == true) { steeringAccel = 0; } }

        //초록색 조타라인 표시기 좌표
        jtLine.rectTransform.anchoredPosition = new Vector2(steeringAccel * 100, 0);
    }

    /// <summary>
    ///  AD키를 통해 조타할때 인식하는 함수
    /// </summary>
    /// <param name="isStr">AD 를 눌르면 활성화 QE 를 눌르면 비활성화</param>
    /// <param name="ac">현재 조타 악셀 변경량</param>
    void SteeringAccelCtrl(bool isStr, int ac)
    {
        isSteeringCtrl = isStr;
        if (Mathf.Abs(ac) != 1) { steeringAccel = ac; }
        else { steeringAccel += ac; }
    }

    /// <summary>
    /// 가속 함수
    /// </summary>
    protected virtual void Acceleration()
    {

    }

    /// <summary>
    /// 조타량 가속 함수
    /// </summary>
    void SteeringAcceleration()
    {
        // 조타량 시간 맞추기위해 변수 정의
        float realSteeringAcceleration = realSteeringRadius / steeringTimeSt * 0.04f;
        realSteeringAcceleration = Mathf.Round(realSteeringAcceleration * 10) * 0.1f;

        switch (steeringAccel)
        {
            case 2:
                if (rFull > steering) steering += (realSteeringAcceleration);
                break;

            case 1:
                if (rHalf > steering) steering += (realSteeringAcceleration);
                if (rHalf < steering) steering -= (realSteeringAcceleration);
                break;

            case 0:
                if (lrStop > steering) steering += (realSteeringAcceleration);
                if (lrStop < steering) steering -= (realSteeringAcceleration);
                break;

            case -1:
                if (lHalf > steering) steering += (realSteeringAcceleration);
                if (lHalf < steering) steering -= (realSteeringAcceleration);
                break;

            case -2:
                if (lFull < steering) steering -= (realSteeringAcceleration);
                break;
        }

        steering = Mathf.Round(steering * 10) * 0.1f;
    }

    /// <summary>
    /// 노트수 표현 함수
    /// </summary>
    void Kts()
    {
        not = Mathf.Round(not);
        ktsText.text = string.Format("{0:0.0#} kts >\n", Mathf.Round(not * 0.1f) * 0.1f);
        float y = 200f * (1f / (maxNotSt / (not * 0.01f))) + 110f;
        if (not == 0f)
        {
            y = 110f;
        }
        if (not < 0)
        {
            y = 100f * (1f / (maxNotSt / (not * 0.01f))) + 110f;
        }
        ktsText.rectTransform.anchoredPosition = new Vector2(-225, y);

    }

    /// <summary>
    /// 조타수 표현 함수
    /// </summary>
    void Jt()
    {
        float x = 200f * (steering / realSteeringRadius);
        if (steering == 0f)
        {
            x = 0f;
        }
        jtText.rectTransform.anchoredPosition = new Vector2(x, -50);
    }

    public float _maxNotInt;
    public float _accelerationInt;
    public float _steeringTime;

    /// <summary>
    /// OnGUI로 안전하게 넘기기 위한 함수
    /// </summary>
    public void GetSetInfo()
    {
        _maxNotInt = maxNotInt / 100;
        _accelerationInt = accelerationInt / 100;
        _steeringTime = steeringTimeSt;
    }
}