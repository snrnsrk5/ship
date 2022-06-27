using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSystem : MonoBehaviour
{
    public EXPsystem eXPsystem;
    public ShipInfo shipInfo;
    public ShipHPsystem shipHPsystem;
    public ShipCtrl shipCtrl;
    public ArmsShoot armsShoot;
    public Ro[] ro;
    public ShipStats shipStats;
    public ReLoadCavasSetValue reLoadCavasSetValue;

    public string[] shipInfoTree = { "I SAMPSON", "II WICKES", "III CLEMSON", "IV NICHOLAS", "V FARRAGUT",
        "VI MAHAN", "VII BENSON", "VIII FLETCHER", "IX ALLEN M. SUMNER", "X GEARING", "XI MITSCHER", "XII FORREST SHERMAN",
    "XIII KING", "XIV CHARLES F. ADAMS"};

    public int[] shipHPTree = { 9100, 9600, 10900, 13100, 13600, 14100, 15300, 17100, 18500, 19400, 23500, 24900, 27500, 28800 };

    public float[] maxNotTree = { 29.5f, 34f, 35f, 37f, 36.5f, 35f, 38f, 36.5f, 36.5f, 36f, 34f, 33.9f , 32f, 33f};
    public float[] steeringTimeTree = { 2.7f, 2.7f, 2.8f, 2.7f, 2.7f, 2.7f, 2.7f, 3f, 3.3f, 3.3f, 5.1f, 5.3f, 6.2f, 6.6f };
    public float[] steeringRadiusTree = { 520f, 520f, 520f, 600f, 560f, 560f, 570f, 560f, 640f, 640f, 680f, 680f , 750f, 770f};

    public float[] gunReLoadTree = { 7f, 6f, 5f, 4f, 4f, 3.2f, 3.3f, 3.2f, 3.2f, 3f, 1.5f, 1.4f, 0.4f, 0.4f};
    public int[] gunBarrelTree = { 1, 1, 2, 1, 1, 1, 1, 1, 2, 2, 1, 1 , 1, 1};
    public int[] gunMountTree = { 4, 4, 2, 4, 5, 4, 4, 5, 3, 3, 3, 3 , 1, 2};
    public float[] topReLoadTree = { 90f, 90f, 85f, 80f, 80f, 75f, 75f, 70f, 65f, 65f, 60f, 55f, 50f, 45f};
    public int[] topBarrelTree = { 2, 3, 3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5 };

    public float[] armsDegTree = { 15.7f, 15.7f, 15.7f, 13.5f, 12.5f, 12f, 10f, 7.7f, 6.4f, 5.3f, 4.7f, 4.5f, 3f, 3f };

    public int shipTreeLV = 0;

    private void Update()
    {
        InfoCh();
    }

    public void InfoCh()
    {
        if (shipTreeLV < 14)
        {
            shipInfo.shipName = shipInfoTree[shipTreeLV];
            shipHPsystem.maxHpf = shipHPTree[shipTreeLV];
            armsShoot.cgunReLoadTime = gunReLoadTree[shipTreeLV];
            armsShoot.ctopReLoadTime = topReLoadTree[shipTreeLV];
            for (int a = 0; a < ro.Length; a++)
            {
                ro[a].degSec = armsDegTree[shipTreeLV];
            }
            shipCtrl.maxNot = maxNotTree[shipTreeLV];
            shipCtrl.steeringTime = steeringTimeTree[shipTreeLV];
            shipCtrl.steeringRadius = steeringRadiusTree[shipTreeLV];

            armsShoot.gunBarrel = gunBarrelTree[shipTreeLV];
            armsShoot.topBarrel = topBarrelTree[shipTreeLV];

            armsShoot.gunMount = 5 - gunMountTree[shipTreeLV];

            reLoadCavasSetValue.gunMount = gunMountTree[shipTreeLV];

            shipCtrl.StatstoJustice();
            shipStats.RunStats();
        }
    }
}
