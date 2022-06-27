using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ������ ���� �θ� Ŭ����
/// </summary>
/*abstract*/
public abstract class ShipCtrl : MonoBehaviour
{
    [Header("�⺻��ġ")]
    public float maxNot = 32.5f; //�⺻���� ��Ʈ
    public float acceleration = 1.0f; // �ʴ� �ö󰡴� �ִ� ��Ʈ��
    public float steeringTime = 6.5f; // ��Ÿ�� �ɸ��� �ð�
    public float steeringRadius = 700.0f; // 180�� ȸ�� �Ÿ�

    [Header("������ġ")]
    [Range(-1, 4)] public int accel = 0; // ���� �Ǽ� �ܰ�
    [Range(-2, 2)] public int steeringAccel = 0; // ���� ��Ÿ �Ǽ� �ܰ�

    protected float lFull, lHalf, lrStop, rFull, rHalf; // ��Ÿ�Ҷ� ���� ����
    protected bool isSteeringCtrl = false; // AD Ű �����ϱ� ���� ��

    [Header("�����ġ")]
    //��� ���ϰ� �ϱ����� ���� ����
    [SerializeField] protected float maxNotInt; // 32.5��Ʈ�� 3250 �� ��
    [SerializeField] protected float realMaxNotInt; // ��Ÿ�� �ӵ��� �������°��� ����ϱ����� ����
    [SerializeField] protected float accelerationInt; // 2.5�� 250�� ��
    [SerializeField] protected float realSteeringRadius; // ��Ÿ�� �ɸ��� �ð��� ���� �ӵ��� �Ÿ��� ��������� ��

    [Header("�������")]
    [SerializeField] protected float not = 0; // �� ��Ʈ (�ӵ�)
    [SerializeField] protected float steering = 0; // �� ��Ÿ��

    [Header("���Ȼ���")]
    // ���� ������ ���� ��ġ
    [SerializeField] protected float maxNotSt;
    [SerializeField] protected float accelerationSt;
    [SerializeField] protected float steeringTimeSt;

    [Header("�������̽�")]
    //��Ʈ���� ��Ÿ�� ǥ�� �ؽ�Ʈ
    [SerializeField] protected Text ktsText = null;
    [SerializeField] protected Text jtText = null;
    [SerializeField] protected Image KtsLine = null;
    [SerializeField] protected Image jtLine = null;

    void Awake()
    {
        // �⺻ ����
        InvokeRepeating("Kts", 0f, 0.5f);
    }
    void Start()
    {
        JusticeSpdAndStr();
    }
    /// <summary>
    /// ���۰����� ������ ����!
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
    /// �������� �Լ��� ������ ����!
    /// </summary>
    void FixedUpdate()
    {
        Acceleration();
        SteeringAcceleration();
        RealMove();
        
    }

    /// <summary>
    /// ��Ÿ �� �ִ�ӵ� ��� ����
    /// </summary>
    public void JusticeSpdAndStr()
    {
        //�Ҽ��� ���ֱ� ���ؼ� ��� 
        maxNotInt = maxNotSt * 100f;
        accelerationInt = accelerationSt * 100f;
        // �ݰ濡 �ӵ��� ���� ��Ÿ���� �Է�
        realSteeringRadius = (58.5f * maxNotInt * 0.01f) / (steeringRadius * 0.01f);

        //��Ÿ�� ��ġ ����
        rFull = realSteeringRadius * +1.0f;
        rHalf = realSteeringRadius * +0.5f;
        lrStop = realSteeringRadius * +0.0f;
        lHalf = realSteeringRadius * -0.5f;
        lFull = realSteeringRadius * -1.0f;

    }

    /// <summary>
    /// �������ӱ� ���� �� �Լ�
    /// </summary>
    /// <param name="per">���ӵ� ����</param>
    public void EngineSkilltoJusticeSpd(float per)
    {
        maxNotInt = maxNotInt * per;
    }

    /// <summary>
    /// ���� �����Լ�
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
    /// ���� �����Լ�(Ʈ������)
    /// </summary>
    public void StatstoJustice()
    {
        maxNotSt = maxNot;
        accelerationSt = acceleration;
        steeringTimeSt = steeringTime;
        JusticeSpdAndStr();
    }

    /// <summary>
    /// ������ �����϶� ����ϴ� �Լ�
    /// </summary>
    void RealMove()
    {
        // *1.852f �� km/h ������ ��ȯ /3600 �� �ʴ����� ����ϱ�����
        realMaxNotInt = maxNotInt / (1 + ((Mathf.Abs(steering) / realSteeringRadius) / 2f));
        transform.Translate(new Vector3(not / 3600 * 1.852f, 0, 0));
    }

    /// <summary>
    /// ������ ��Ÿ�Ҷ� ����ϴ� �Լ�
    /// </summary>
    void RealRotation()
    {
        // �� �ӵ� ��� ��Ÿ���� �����ϱ� ����
        float realSteering = steering * Time.deltaTime * 0.05f * (not / maxNotInt);
        transform.Rotate(new Vector3(0f, realSteering, 0f));
    }

    /// <summary>
    /// �Ǽ� �����ϴ� �Լ�
    /// </summary>
    void Accel()
    {
        if (Input.GetKeyDown(KeyCode.W) && accel != 4) { accel += 1; }
        else if (Input.GetKeyDown(KeyCode.S) && accel != -1) { accel -= 1; }

        KtsLine.rectTransform.anchoredPosition = new Vector2(-100, 110 + (accel * 50));
    }

    /// <summary>
    /// ��Ÿ�Ǽ� ���� �Լ�
    /// </summary>
    void SteeringAccel()
    {
        // Q�� ���� (-) E �� ������ (+) ������ ������
        if (Input.GetKeyDown(KeyCode.E) && steeringAccel != 2) { SteeringAccelCtrl(false, 1); }
        else if (Input.GetKeyDown(KeyCode.Q) && steeringAccel != -2) { SteeringAccelCtrl(false, -1); }

        // A�� ���� (-) D �� ������ (+) ���� 0���� ���ƿ�
        else if (Input.GetKey(KeyCode.D)) { SteeringAccelCtrl(true, 2); }
        else if (Input.GetKey(KeyCode.A)) { SteeringAccelCtrl(true, -2); }
        // 0���� ���ƿ����ϴ� ����
        else { if (isSteeringCtrl == true) { steeringAccel = 0; } }

        //�ʷϻ� ��Ÿ���� ǥ�ñ� ��ǥ
        jtLine.rectTransform.anchoredPosition = new Vector2(steeringAccel * 100, 0);
    }

    /// <summary>
    ///  ADŰ�� ���� ��Ÿ�Ҷ� �ν��ϴ� �Լ�
    /// </summary>
    /// <param name="isStr">AD �� ������ Ȱ��ȭ QE �� ������ ��Ȱ��ȭ</param>
    /// <param name="ac">���� ��Ÿ �Ǽ� ���淮</param>
    void SteeringAccelCtrl(bool isStr, int ac)
    {
        isSteeringCtrl = isStr;
        if (Mathf.Abs(ac) != 1) { steeringAccel = ac; }
        else { steeringAccel += ac; }
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    protected virtual void Acceleration()
    {

    }

    /// <summary>
    /// ��Ÿ�� ���� �Լ�
    /// </summary>
    void SteeringAcceleration()
    {
        // ��Ÿ�� �ð� ���߱����� ���� ����
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
    /// ��Ʈ�� ǥ�� �Լ�
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
    /// ��Ÿ�� ǥ�� �Լ�
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
    /// OnGUI�� �����ϰ� �ѱ�� ���� �Լ�
    /// </summary>
    public void GetSetInfo()
    {
        _maxNotInt = maxNotInt / 100;
        _accelerationInt = accelerationInt / 100;
        _steeringTime = steeringTimeSt;
    }
}