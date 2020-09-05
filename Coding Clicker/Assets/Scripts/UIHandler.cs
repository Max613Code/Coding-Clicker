using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIHandler
{

    private static Text moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
    private static Text upgradeMoneyText = GameObject.Find("UpgradeMoneyText").GetComponent<Text>();    
    private static Text employeeMoneyText = GameObject.Find("EmployeeMoneyText").GetComponent<Text>();
    private static RectTransform AllUpgradeUI = GameObject.Find("UpgradeContainer").GetComponent<RectTransform>();
    private static RectTransform UnlockdUpgradeUI = GameObject.Find("UnlockedUpgradesContainer").GetComponent<RectTransform>();

    public static void UpdateMoney(decimal money = -1234, Text MoneyText = null)
    {
        if (MoneyText == null)
        {
            MoneyText = moneyText;
        }
        if (money == -1234)
        {
            money = LevelHandler.money;
        }

        moneyText.text = "$" + Math.Round(money, 2);
        upgradeMoneyText.text = "$" + Math.Round(money, 2);
        employeeMoneyText.text = "$" + Math.Round(money, 2);
    }

    public static void RePosUpgradeUI()
    {
        AllUpgradeUI.transform.position = new Vector3(AllUpgradeUI.transform.position.x, AllUpgradeUI.transform.position.y + ((AllUpgradeUI.localScale.y - 830) / 2), AllUpgradeUI.transform.position.z);
    }

    public static void RePosUnlockedUpgradesUI()
    {
        UnlockdUpgradeUI.transform.position = new Vector3(UnlockdUpgradeUI.transform.position.x, UnlockdUpgradeUI.transform.position.y + ((UnlockdUpgradeUI.localScale.y - 830) / 2), UnlockdUpgradeUI.transform.position.z);
    }

}
