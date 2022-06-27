using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    // 최고속도, 가속력, 조타시간
    public ShipCtrl shipCtrl;
    // 최고 체력
    public ShipHPsystem shipHPsystem;
    // 장전속도
    public ArmsShoot armsShoot;

    public int maxNotStats = 0;
    public int accelerationStats = 0;
    public int steeringTimeStats = 0;
    public int maxHpStats = 0;
    public int reLoadStats = 0;

    void Start()
    {
        RunStats();
    }
    
    /// <summary>
    /// 스탯 투 퍼센트
    /// </summary>
    /// <param name="toPer"></param>
    /// <param name="stLV"></param>
    float Justice(int toPer, int stLV)
    {
        float per = 1 + (stLV * 0.01f * toPer);
        return per;
    }

    public void RunStats()
    {
        //LV 수치를 1.02, 1.06등 퍼센트 수치로 바꿈
        float maxNotPer = Justice(1, maxNotStats);
        float accelerationPer = Justice(1, accelerationStats);
        float steeringTimePer = Justice(1, steeringTimeStats);
        float maxHpPer = Justice(1, maxHpStats);
        float reLoadPer = Justice(1, reLoadStats);

        shipCtrl.StatstoJustice(maxNotPer, accelerationPer,steeringTimePer);
        shipHPsystem.Statsjustice(maxHpPer);
        armsShoot.Statsjustice(reLoadPer);

    }
}
