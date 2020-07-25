using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIHandler
{
    public static void UpdateMoney(float money, Text moneyText = null)
    {
        if (moneyText == null)
        {
            moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        }

        moneyText.text = "$" + money;
    }
}
