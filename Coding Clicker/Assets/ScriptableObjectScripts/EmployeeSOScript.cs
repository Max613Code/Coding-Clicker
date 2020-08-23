using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EmployeeSO", order = 2)]
public class EmployeeSOScript : ScriptableObject
{
    public string employeeName;
    public string description;

    public Sprite image;

    public float salary;
    public float salaryTimeSecs;

    [Header("Computer Effects")]
    public float clickPowerEffect = 1;
    public float autoClickerEffect = 1;

    [Header("Block Coding Effects")]
    public float blockCodingProductionEffect = 1;
    public float blockCodingSpeedEffect = 1;
    public float blockCodingAutoClickerEffect = 1;
    public bool blockCodingAutoClicker = false;
    [Header("Console Effects")]
    public float consoleProductionEffect = 1;
    public float consoleSpeedEffect = 1;
    public float consoleAutoClickerEffect = 1;
    public bool consoleAutoClicker = false;
    [Header("GUIS Effects")]
    public float GUISProductionEffect = 1;
    public float GUISSpeedEffect = 1;
    public float GUISAutoClickerEffect = 1;
    public bool GUISAutoClicker = false;
    [Header("Global Effects")]
    public float globalProductionEffect = 1;
    public float globalSpeedEffect = 1;
}