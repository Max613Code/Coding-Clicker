using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EmployeeSO", order = 2)]
public class EmployeeSOScript : ScriptableObject
{
    public string employeeName;
    public string effectsDescription;
    public string description;

    public Sprite image;

    public float salary = 1;
    public float salaryTimeSecs = 1;

    public int tier = 1;

    [Header("Computer Effects")]
    public float clickPowerEffect = 1;
    public float computerAutoClickerEffect = 1;

    [Header("Generator Effects")]

    public List<string> genNames;

    public List<float> productionEffects;
    public List<float> speedEffects;
    public List<float> autoClickerEffects;
    public List<bool> genAutoClickers;
}