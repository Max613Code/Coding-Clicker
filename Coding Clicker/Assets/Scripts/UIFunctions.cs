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

    public Canvas EmployeeCanvas;
    public RectTransform EmployeeShop;
    public RectTransform EmployeeMenu;

    private Vector3 employeeMenuPosition;

    private void Start()
    {
        UnlockedUpgrades.gameObject.SetActive(false);
        EmployeeMenu.gameObject.transform.position = new Vector3(2000, 0, 2000);
        employeeMenuPosition = EmployeeShop.transform.position;
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

    public void SwitchEmployeeShop()
    {
        if (EmployeeCanvas.enabled )
        {
            EmployeeShop.gameObject.SetActive(true);
            EmployeeMenu.gameObject.transform.position = new Vector3(2000, 0, 2000);
        }
    }
    
    public void SwitchEmployeeMenu()
    {
        if (EmployeeCanvas.enabled )
        {
            EmployeeShop.gameObject.SetActive(false);
            EmployeeMenu.gameObject.transform.position = employeeMenuPosition;
        }
    }

    public void UpdateMoney()
    {
        moneyText.text = "$" + Math.Round(LevelHandler.money, 2);
    }
}
