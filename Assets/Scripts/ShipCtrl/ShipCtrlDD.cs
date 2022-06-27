using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtrlDD : ShipCtrl
{
    /// <summary>
    /// 구축함급 가속 함수
    /// </summary>
    protected override void Acceleration()
    {
        base.Acceleration();

        if (accel == 4)
        {
            if (realMaxNotInt * 0.875f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * 0.9375f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.96875f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 1f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * 1.125f < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 1.0625f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 1.03125f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * 1f < not) not -= accelerationInt * 0.02f * 0.06f;
        }
        if (accel == 3)
        {
            if (realMaxNotInt * 0.625f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * 0.6875f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.71875f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.75f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * 0.875 < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.8125f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.78125f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * 0.75f < not) not -= accelerationInt * 0.02f * 0.06f;
        }
        if (accel == 2)
        {
            if (realMaxNotInt * 0.375f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * 0.4375f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.46875f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.5f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * 0.625f < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.5625f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.53125f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * 0.5f < not) not -= accelerationInt * 0.02f * 0.06f;
        }
        if (accel == 1)
        {
            if (realMaxNotInt * 0.125f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * 0.1875f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.21875f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.25f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * 0.375f < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.3125f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.28125f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * 0.25f < not) not -= accelerationInt * 0.02f * 0.06f;
        }
        if (accel == 0)
        {
            if (realMaxNotInt * -0.125f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * -0.0625f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * -0.03125f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * 0.125f < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * 0.0625f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * 0.03125f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * 0f < not) not -= accelerationInt * 0.02f * 0.06f;
        }
        if (accel == -1)
        {
            if (realMaxNotInt * -0.625f > not) not += accelerationInt * 0.02f * 1f;
            else if (realMaxNotInt * -0.5625f > not) not += accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * -0.53125f > not) not += accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * -0.5f > not) not += accelerationInt * 0.02f * 0.12f;

            else if (realMaxNotInt * -0.375f < not) not -= accelerationInt * 0.02f * 0.5f;
            else if (realMaxNotInt * -0.4375f < not) not -= accelerationInt * 0.02f * 0.25f;
            else if (realMaxNotInt * -0.46875f < not) not -= accelerationInt * 0.02f * 0.12f;
            else if (realMaxNotInt * -0.5f < not) not -= accelerationInt * 0.02f * 0.06f;
            
        }
    }
    
}
