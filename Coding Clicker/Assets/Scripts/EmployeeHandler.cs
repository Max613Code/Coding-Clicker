using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmployeeHandler : MonoBehaviour
{
    public List<EmployeeSOScript> employeeList;

    public GameObject employeeShopPanel;
    public GameObject ownedEmployeesPanel;

    public GameObject employeePrefab;
    public GameObject employeeShopPrefab;

    public List<EmployeeSOScript> employeeListSorted;
    public List<EmployeeClass> employeeClasses;

    public GameObject Master;

    private MasterScript MasterScript;

    private GameObject container;
    private EmployeeSOScript employeeSO;

    private GameObject employee;
    private EmployeeClass employeeClass;

    private List<EmployeeSOScript> closestEmployees;

    public float EmployeeShopCooldownSecondsFloatParameter = 1200;

    public decimal EmployeeShopCooldownSeconds;
    public decimal timer = 0;

    public List<decimal> SalaryToSecs;

    public void SetUpShop()
    {
        MasterScript.CalculateMPS();
        SalaryToSecs = (from o in employeeListSorted select Convert.ToDecimal(o.salary / o.salaryTimeSecs)).ToList();

        var closestIndex = SalaryToSecs.IndexOf(SalaryToSecs.Aggregate((x, y) => Math.Abs(x - MasterScript.mps) < Math.Abs(y - MasterScript.mps) ? x : y));

        if(employeeListSorted.Count <= 5)
        {
            closestEmployees = employeeListSorted;
        } 
        else if ((closestIndex+1) > (employeeListSorted.Count-5))
        {
            closestEmployees = employeeListSorted.Where(o => employeeListSorted.IndexOf(o) > (employeeListSorted.Count - 5)).ToList();
        }
        else if ((closestIndex + 1) < (employeeListSorted.Count-5))
        {
            closestEmployees = employeeListSorted.Where(o => employeeListSorted.IndexOf(o) < (5)).ToList();
        }
        else
        {
            closestEmployees = new List<EmployeeSOScript>{ employeeListSorted[closestIndex - 2], employeeListSorted[closestIndex - 1] , employeeListSorted[closestIndex] , employeeListSorted[closestIndex + 1] , employeeListSorted[closestIndex + 2] };
        }

        var selectedEmployees = closestEmployees.OrderBy(x => RandomFunctions.RandomInt(1,150)).Take(3).ToList();

        foreach(EmployeeSOScript emp in selectedEmployees)
        {
            var employeeShopPrefabInstance = Instantiate(employeeShopPrefab);
            var employeeShopScript = employeeShopPrefabInstance.GetComponent<EmployeeShopPrefabScript>();
            var amount = RandomFunctions.RandomInt(1, 15);
            employeeShopScript.SetUp(emp.employeeName, emp.effectsDescription, "$" + emp.salary + " per " + Math.Round(Convert.ToDecimal(emp.salaryTimeSecs) / 60,2) + " minutes.", emp.image, employeeClasses.Where(x => x.description == emp.description).ToList()[0], Convert.ToDecimal((emp.salary / emp.salaryTimeSecs) * RandomFunctions.RandomInt(25, 50) * amount * RandomFunctions.RandomInt(10,20)/10), amount);

            employeeShopPrefabInstance.gameObject.transform.SetParent(employeeShopPanel.transform);
        }

    }

    private void Start()
    {
        MasterScript = Master.GetComponent<MasterScript>();

        employeeListSorted = employeeList.OrderBy(o => (o.salary / o.salaryTimeSecs)).ToList();

        EmployeeShopCooldownSeconds = Convert.ToDecimal(EmployeeShopCooldownSecondsFloatParameter);

        for (int i = 0; i < employeeListSorted.Count; i++)
        {
            employeeSO = employeeListSorted[i];
            container = GameObject.Find("EmployeeTier" + employeeSO.tier).transform.Find("Container").gameObject;

            employee = (GameObject)Instantiate(employeePrefab);
            employee.GetComponent<EmployeeClass>().SetUp(employeeSO.employeeName, employeeSO.effectsDescription, employeeSO.description, employeeSO.image, employeeSO.salary, employeeSO.salaryTimeSecs, employeeSO.clickPowerEffect, employeeSO.computerAutoClickerEffect, employeeSO.genNames,employeeSO.productionEffects, employeeSO.speedEffects, employeeSO.autoClickerEffects, employeeSO.genAutoClickers) ;

            employee.transform.SetParent(container.transform);

            employeeClasses.Add(employee.GetComponent<EmployeeClass>());
        }

        SetUpShop();

    }
    private void Update()
    {
        timer += Convert.ToDecimal(Time.deltaTime);
        if (timer >= EmployeeShopCooldownSeconds)
        {
            SetUpShop();
        }
    }
}
