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
    static RandomFunctions rand = functions.GetComponent<RandomFunctions>();

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

        LevelHandler.AddMoney(1);
        UIHandler.UpdateMoney(LevelHandler.money);
    }
}
