using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public Text moneyText;

    public Canvas UpgradeCanvas;
    public RectTransform AllUpgrades;
    public RectTransform UnlockedUpgrades;
    public Text UpgradeTitle;

    private void Start()
    {
        UnlockedUpgrades.gameObject.SetActive(false);
    }

    public void SwitchAllUpgrades()
    {
        if (UpgradeCanvas.enabled)
        {
            AllUpgrades.gameObject.SetActive(true);
            UnlockedUpgrades.gameObject.SetActive(false);
            UpgradeTitle.text = "Upgrades";
        }
    }

    public void SwitchUnlockedUpgrades()
    {
        if (UpgradeCanvas.enabled)
        {
            UnlockedUpgrades.gameObject.SetActive(true);
            AllUpgrades.gameObject.SetActive(false);
            UpgradeTitle.text = "Unlocked";
        }
    }

    public void UpdateMoney()
    {
        moneyText.text = "$" + Math.Round(LevelHandler.money, 2);
    }
}
