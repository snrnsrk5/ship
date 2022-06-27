using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCavans : MonoBehaviour
{
    public TreeSystem treeSystem;

    public Text leftName;
    public Text leftHp;
    public Text leftBatery;
    public Text leftTop;
    public Text leftMaxnot;
    public Text leftRotTime;
    public Text leftRotRad;

    public Text rightName;
    public Text rightHp;
    public Text rightBatery;
    public Text rightTop;
    public Text rightMaxnot;
    public Text rightRotTime;
    public Text rightRotRad;

    public Text arrow;

    public Button tierUp;
    public Text ifText;

    void Update()
    {
        if ((int)treeSystem.shipTreeLV < 13)
        {
            leftName.text = string.Format($"{treeSystem.shipInfoTree[treeSystem.shipTreeLV]}");
            leftHp.text = string.Format($"{treeSystem.shipHPTree[treeSystem.shipTreeLV]}");

            leftBatery.text = string.Format
                ($"{treeSystem.gunMountTree[treeSystem.shipTreeLV]}X{treeSystem.gunBarrelTree[treeSystem.shipTreeLV]} " +
                $"{treeSystem.gunReLoadTree[treeSystem.shipTreeLV]}s {treeSystem.armsDegTree[treeSystem.shipTreeLV]}s");
            leftTop.text = string.Format
                ($"2X{treeSystem.topBarrelTree[treeSystem.shipTreeLV]} {treeSystem.topReLoadTree[treeSystem.shipTreeLV]}s 7.3s");

            leftMaxnot.text = string.Format($"{treeSystem.maxNotTree[treeSystem.shipTreeLV]}");
            leftRotTime.text = string.Format($"{treeSystem.steeringTimeTree[treeSystem.shipTreeLV]}");
            leftRotRad.text = string.Format($"{treeSystem.steeringRadiusTree[treeSystem.shipTreeLV]}");
        }

        if ((int)treeSystem.shipTreeLV < 13)
        {
            rightName.text = string.Format($"{treeSystem.shipInfoTree[treeSystem.shipTreeLV + 1]}");
            rightHp.text = string.Format($"{treeSystem.shipHPTree[treeSystem.shipTreeLV + 1]}");

            rightBatery.text = string.Format
                ($"{treeSystem.gunMountTree[treeSystem.shipTreeLV + 1]}X{treeSystem.gunBarrelTree[treeSystem.shipTreeLV + 1]} " +
                $"{treeSystem.gunReLoadTree[treeSystem.shipTreeLV + 1]}s {treeSystem.armsDegTree[treeSystem.shipTreeLV + 1]}s");
            rightTop.text = string.Format
                ($"2X{treeSystem.topBarrelTree[((int)treeSystem.shipTreeLV) + 1]} {treeSystem.topReLoadTree[treeSystem.shipTreeLV + 1]}s 7.3s");

            rightMaxnot.text = string.Format($"{treeSystem.maxNotTree[treeSystem.shipTreeLV + 1]}");
            rightRotTime.text = string.Format($"{treeSystem.steeringTimeTree[treeSystem.shipTreeLV + 1]}");
            rightRotRad.text = string.Format($"{treeSystem.steeringRadiusTree[treeSystem.shipTreeLV + 1]}");
        }
        else
        {
            rightName.gameObject.SetActive(false);
            rightHp.gameObject.SetActive(false);
            rightBatery.gameObject.SetActive(false);
            rightTop.gameObject.SetActive(false);
            rightMaxnot.gameObject.SetActive(false);
            rightRotTime.gameObject.SetActive(false);
            rightRotRad.gameObject.SetActive(false);
            arrow.gameObject.SetActive(false);
            ifText.gameObject.SetActive(false);
        }

        if ((int)(treeSystem.eXPsystem.shipLv * 0.1f) > treeSystem.shipTreeLV && treeSystem.shipTreeLV < 13)
        {
            tierUp.gameObject.SetActive(true);
        }
        else
        {
            tierUp.gameObject.SetActive(false);
            ifText.text = string.Format($"{(treeSystem.shipTreeLV + 1) * 10}LV and over");
        }
    }

    public void TierUp()
    {
        if ((int)(treeSystem.eXPsystem.shipLv * 0.1f) > treeSystem.shipTreeLV && treeSystem.shipTreeLV < 13)
        {
            treeSystem.shipTreeLV++;
        }
    }
}
