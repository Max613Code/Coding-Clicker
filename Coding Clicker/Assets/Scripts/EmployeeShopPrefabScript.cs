using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeShopPrefabScript : MonoBehaviour
{
    public string employeeName;
    public string effects;
    public string salaryDescription;
    public Sprite image;

    public EmployeeClass employee;

    public decimal cost;
    public int amount;

    private Text nameText;
    private Text effectsText;
    private Text salaryText;
    private Image employeeImage;
    private Text amountText;
    private Text costText;

    private bool bought = false;

    public void Buy()
    {
        if (LevelHandler.money >= cost && !bought)
        {
            bought = true;
            LevelHandler.SubtractMoney(cost);
            employee.employeePoints += amount;
            employee.Unlock();
            UIHandler.UpdateMoney();
            costText.text = "Bought";
        }
    }

    public void SetUp(string Name, string Effects, string SalaryDescription, Sprite Image,EmployeeClass Employee, decimal Cost, int Amount)
    {
        employeeName = Name;
        effects = Effects;
        salaryDescription = SalaryDescription;
        image = Image;
        employee = Employee;
        cost = Cost;
        amount = Amount;

        nameText = gameObject.transform.Find("EmployeeName").gameObject.GetComponent<Text>();
        effectsText = gameObject.transform.Find("Effects").gameObject.GetComponent<Text>();
        salaryText = gameObject.transform.Find("Salary").gameObject.GetComponent<Text>();
        amountText = gameObject.transform.Find("Amount").Find("AmountText").GetComponent<Text>();
        costText = gameObject.transform.Find("CostImage").Find("Cost").GetComponent<Text>();

        employeeImage = gameObject.transform.Find("Image").GetComponent<Image>();

        nameText.text = employeeName;
        effectsText.text = effects;
        salaryText.text = salaryDescription;
        amountText.text = amount.ToString();
        employeeImage.sprite = image;
        costText.text = "$" + cost;
    }
}
