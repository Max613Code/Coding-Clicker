﻿using System;
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
        LevelHandler.AddMoney(clickPower);
        UIHandler.UpdateMoney();
    }

    public static void MultiplyClickPower(decimal mult)
    {
        clickPower *= mult;
    }

    public static void AddClickPower(decimal amount)
    {
        clickPower += amount;
    }

    public static void SetClickPower(decimal amount)
    {
        clickPower = amount;
    }

    public static void ChangeComputerImage(Sprite image)
    {
        computer.GetComponent<Image>().sprite = image;
    }
}
