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
    public RectTransform EmployeeDetails;
    public RectTransform EmployeeNav;

    private Vector3 employeeMenuPosition;

    private Sprite employeeDetailsImage;
    private Text employeeDetailsPoints;
    private Text employeeDetailsName;
    private Text employeeDetailsEffects;
    private Text employeeDetailsDescription;
    private Text employeeDetailsSalary;

    private void Start()
    {
        UnlockedUpgrades.gameObject.SetActive(false);
        EmployeeMenu.gameObject.transform.position = new Vector3(2000, 0, 2000);
        employeeMenuPosition = EmployeeShop.transform.position;

        employeeDetailsImage = EmployeeDetails.transform.Find("Image").GetComponent<Sprite>();
        employeeDetailsPoints = EmployeeDetails.transform.Find("Points").GetComponent<Text>();
        employeeDetailsName = EmployeeDetails.transform.Find("Name").GetComponent<Text>();
        employeeDetailsEffects = EmployeeDetails.transform.Find("Effect").GetComponent<Text>();
        employeeDetailsDescription = EmployeeDetails.transform.Find("Description").GetComponent<Text>();
        employeeDetailsSalary = EmployeeDetails.transform.Find("Salary").GetComponent<Text>();
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
        if (EmployeeCanvas.enabled)
        {
            EmployeeShop.gameObject.SetActive(true);
            EmployeeMenu.gameObject.transform.position = new Vector3(2000, 0, 2000);
            EmployeeDetails.gameObject.SetActive(false);
        }
    }

    public void SwitchEmployeeMenu()
    {
        if (EmployeeCanvas.enabled)
        {
            EmployeeShop.gameObject.SetActive(false);
            EmployeeMenu.gameObject.transform.position = employeeMenuPosition;
            EmployeeDetails.gameObject.SetActive(false);
            EmployeeNav.gameObject.SetActive(true);
        }
    }

    public void SwitchEmployeeDetails(Sprite Image, decimal EmployeePoints, string EmployeeName, string EmployeeEffects, string EmployeeDescription, string EmployeeSalary)
    {
        employeeDetailsImage = Image;
        employeeDetailsPoints.text = EmployeePoints.ToString();
        employeeDetailsName.text = EmployeeName;
        employeeDetailsEffects.text = EmployeeEffects.ToString();
        employeeDetailsDescription.text = EmployeeDescription.ToString();
        employeeDetailsSalary.text = EmployeeSalary.ToString();

        EmployeeDetails.gameObject.SetActive(true);
        EmployeeShop.gameObject.SetActive(false);
        EmployeeNav.gameObject.SetActive(false);
        EmployeeMenu.gameObject.transform.position = new Vector3(2000, 0, 2000);
    }

    public void UpdateMoney()
    {
        moneyText.text = "$" + Math.Round(LevelHandler.money, 2);
    }
}
