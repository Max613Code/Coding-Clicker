using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ComputerHandler
{
    static GameObject computer = GameObject.Find("Computer");
    static RectTransform computerTransform = computer.GetComponent<RectTransform>();

    static bool wobbly = false;

    static GameObject functions = GameObject.Find("Functions");

    public static decimal clickPower { get; private set; }
    public static decimal calculatedClickPower { get; private set; }

    public static decimal globalMult = 1;
    public static decimal employeeMult = 1;

    public static void ComputerClicked()
    {
        if (!wobbly)
        {/*  Make it bounce back and forward, like wobbling.
            wobbly = true;
            for (int i = 0; i < 25; i++)
            {
                float randNum = (float)rand.RandomInt(1, 2) / 100;
 
                computerTransform.localScale = new Vector3(computerTransform.localScale.x - randNum, computerTransform.localScale.y - randNum, computerTransform.localScale.z);
            }
            wobbly = false;
            */
        }
        LevelHandler.AddMoney(calculatedClickPower);
        UIHandler.UpdateMoney();
    }

    public static void MultiplyClickPower(decimal mult)
    {
        clickPower *= mult;
        CalculateClickPower();
    }

    public static void AddClickPower(decimal amount)
    {
        clickPower += amount;
        CalculateClickPower();
    }

    public static void SetClickPower(decimal amount)
    {
        clickPower = amount;
        CalculateClickPower();
    }

    public static void ChangeComputerImage(Sprite image)
    {
        computer.GetComponent<Image>().sprite = image;
    }

    public static void CalculateClickPower() {
        calculatedClickPower = clickPower * globalMult * employeeMult;
    }
}
