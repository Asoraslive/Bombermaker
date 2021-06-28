using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentWallText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public MousePlacesObjects mouseScript;
    public string countObj;
    private int currentCount;
    private int limitCount;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();


    }
    // Update is called once per frame
    public void LateUpdate()
    {
        if (countObj == "DesWall")
        {
            currentCount = mouseScript.CurrentWall;
            limitCount = mouseScript.LimitWall;
        }
        else if (countObj == "Exit")
        {
            currentCount = mouseScript.CurrentExit;
            limitCount = mouseScript.LimitExit;
        }
        else if (countObj == "PowerUp")
        {
            currentCount = mouseScript.CurrentPowerUp;
            limitCount = mouseScript.LimitPowerUp;
        }
        else if (countObj == "Trap")
        {
            currentCount = mouseScript.CurrentTrap;
            limitCount = mouseScript.LimitTrap;
        }
        else if (countObj == "Enemy")
        {
            currentCount = mouseScript.CurrentEnemy;
            limitCount = mouseScript.LimitEnemy;
        }
        text.SetText(currentCount + " / " + limitCount);
    }
}
