using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public ShipStats shipStats;

    public int maxLevel = 12;

    public int plusStats = 0;

    public Canvas statsCanvas;
    public Canvas TreeCanvas;
    public Text plusStatsText;
    private bool isStatsCanvas = false;
    private bool isTreeCanvas = false;

    public Image maxNotUI;
    public Image accelerationUI;
    public Image steeringTimeUI;
    public Image maxHpUI;
    public Image reLoadUI;

    public Button maxNotButton;
    public Button accelerationButton;
    public Button steeringTimeButton;
    public Button maxHpButton;
    public Button reLoadButton;

    public void Update()
    {
        LevelUI();
        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            statsCanvas.gameObject.SetActive(false);
            isStatsCanvas = false;
            TreeCanvas.gameObject.SetActive(false);
            isTreeCanvas = false;
        }
    }

    public void StatsSetTrue()
    {
        if (isStatsCanvas)
        {
            statsCanvas.gameObject.SetActive(false);
            isStatsCanvas = false;
        }
        else
        {
            statsCanvas.gameObject.SetActive(true);
            TreeCanvas.gameObject.SetActive(false);
            isStatsCanvas = true;
            isTreeCanvas = false;
        }
    }

    public void TreeSetTrue()
    {
        if (isTreeCanvas)
        {
            TreeCanvas.gameObject.SetActive(false);
            isTreeCanvas = false;
        }
        else
        {
            TreeCanvas.gameObject.SetActive(true);
            statsCanvas.gameObject.SetActive(false);
            isTreeCanvas = true;
            isStatsCanvas = false;
        }
    }

    public void MaxNotUp()
    {
        if (shipStats.maxNotStats != maxLevel && plusStats != 0)
        {
            shipStats.maxNotStats++;
            shipStats.RunStats();
            plusStats--;
        }
    }

    public void AccelerationUp()
    {
        if (shipStats.accelerationStats != maxLevel && plusStats != 0)
        {
            shipStats.accelerationStats++;
            shipStats.RunStats();
            plusStats--;
        }
    }
    public void SteeringTimeUp()
    {
        if (shipStats.steeringTimeStats != maxLevel && plusStats != 0)
        {
            shipStats.steeringTimeStats++;
            shipStats.RunStats();
            plusStats--;
        }

    }

    public void MaxHpUp()
    {
        if (shipStats.maxHpStats != maxLevel && plusStats != 0)
        {
            shipStats.maxHpStats++;
            shipStats.RunStats();
            plusStats--;
        }

    }

    public void MaxReLoadUp()
    {
        if (shipStats.reLoadStats != maxLevel && plusStats != 0)
        {
            shipStats.reLoadStats++;
            shipStats.RunStats();
            plusStats--;
        }
    }

    public void LevelUI()
    {
        maxNotUI.fillAmount = (float)shipStats.maxNotStats / maxLevel;
        accelerationUI.fillAmount = (float)shipStats.accelerationStats / maxLevel;
        steeringTimeUI.fillAmount = (float)shipStats.steeringTimeStats / maxLevel;
        maxHpUI.fillAmount = (float)shipStats.maxHpStats / maxLevel;
        reLoadUI.fillAmount = (float)shipStats.reLoadStats / maxLevel;

        plusStatsText.text = string.Format($"stat : {plusStats}");

        if (plusStats == 0)
        {
            maxNotButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            steeringTimeButton.gameObject.SetActive(false);
            maxHpButton.gameObject.SetActive(false);
            reLoadButton.gameObject.SetActive(false);
        }
        else
        {
            setActiceButton(maxNotButton, shipStats.maxNotStats);
            setActiceButton(accelerationButton, shipStats.accelerationStats);
            setActiceButton(steeringTimeButton, shipStats.steeringTimeStats);
            setActiceButton(maxHpButton, shipStats.maxHpStats);
            setActiceButton(reLoadButton, shipStats.reLoadStats);
        }
    }

    void setActiceButton(Button bu, int stat)
    {
        if (stat != maxLevel)
        {
            bu.gameObject.SetActive(true);
        }
        else
        {
            bu.gameObject.SetActive(false);
        }
    }

}
