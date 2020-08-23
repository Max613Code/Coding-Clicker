using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class EmployeeClass : MonoBehaviour
{
    public string employeeName;
    public string effectsDescription;
    public string description;

    public Sprite image;

    public decimal salary;
    public decimal salaryTimeSecs;

    private decimal level;
    public decimal employeePoints;

    public int tier;

    public decimal timeCounter { get; private set; }

    public decimal clickPowerEffect = 1;
    public decimal computerAutoClickerEffect = 1;

    public List<GeneratorMaker> gens;

    public List<decimal> productionEffects;
    public List<decimal> speedEffects;
    public List<decimal> autoClickerEffects;
    public List<bool> genAutoClickers;

    public GeneratorMaker gen;

    public bool working = true;
    public bool unlocked { get; private set; }

    private Image imageContainer;
    private Text nameText;
    private Text effectsText;

    private void Start()
    {
        timeCounter = 0;
    }

    public void Update()
    {
        if (working)
        {
            timeCounter += (decimal)Time.deltaTime;
            if (timeCounter >= salaryTimeSecs)
            {
                timeCounter = 0;
                LevelHandler.SubtractMoney(salary);
                UIHandler.UpdateMoney();
            }
        }
    }

    public void StartWorking()
    {
        if (!working)
        {
            working = true;
            for (int i = 0; i < gens.Count; i++)
            {
                gen = gens[i];
                if (productionEffects.Count == gens.Count)
                {
                    gen.gen.employeeMultiplier *= productionEffects[i];
                }
                if (speedEffects.Count == gens.Count)
                {
                    gen.gen.employeeSpeedMultiplier *= productionEffects[i];
                }
                if ((genAutoClickers.Count == gens.Count))
                {
                    if (genAutoClickers[i])
                    {
                        gen.gen.employeeWorkerMultiplier += 1;
                    }
                }
            }
        }
    }

    public void StopWorking()
    {
        if (!working)
        {
            working = false;
            for (int i = 0; i < gens.Count; i++)
            {
                gen = gens[i];
                if (productionEffects.Count == gens.Count)
                {
                    gen.gen.employeeMultiplier /= productionEffects[i];
                }
                if (speedEffects.Count == gens.Count)
                {
                    gen.gen.employeeSpeedMultiplier /= productionEffects[i];
                }
                if ((genAutoClickers.Count == gens.Count))
                {
                    if (genAutoClickers[i])
                    {
                        gen.gen.employeeWorkerMultiplier -= 1;
                    }
                }
            }
        }
    }

    public void UpdateTexts()
    {
        if (!unlocked)
        {
            imageContainer.sprite = image;
            nameText.text = "???";
            effectsText.text = "???";
        }
        else
        {
            imageContainer.sprite = image;
            nameText.text = employeeName;
            effectsText.text = effectsDescription;
        }
    }

    public void Unlock()
    {
        unlocked = true;
        UpdateTexts();
        StartWorking();
    }

    public void SetUp(string EmployeeName, string EffectsDescription, string Description, Sprite Image, float Salary, float SalaryTimeSecs, float ClickPowerEffect, float ComputerAutoClickerEffect, List<string> Gens, List<float> ProductionEffects, List<float> SpeedEffects, List<float> AutoClickerEffects, List<bool> GenAutoClickers, bool Unlocked = false)
    {
        employeeName = EmployeeName;
        effectsDescription = EffectsDescription;
        description = Description;
        image = Image;
        salary = Convert.ToDecimal(Salary);
        salaryTimeSecs = Convert.ToDecimal(SalaryTimeSecs);
        clickPowerEffect = Convert.ToDecimal(ClickPowerEffect);
        computerAutoClickerEffect = Convert.ToDecimal(ComputerAutoClickerEffect);
        gens = (from o in Gens select GameObject.Find(o).GetComponent<GeneratorMaker>()).ToList();
        autoClickerEffects = (from o in AutoClickerEffects select Convert.ToDecimal(o)).ToList();
        productionEffects = (from o in ProductionEffects select Convert.ToDecimal(o)).ToList();
        speedEffects = (from o in SpeedEffects select Convert.ToDecimal(o)).ToList();
        genAutoClickers = GenAutoClickers;

        imageContainer = gameObject.transform.Find("Image").GetComponent<Image>();
        nameText = gameObject.transform.Find("EmployeeName").GetComponent<Text>();
        effectsText = gameObject.transform.Find("Effects").GetComponent<Text>();

        unlocked = Unlocked;

        if (!unlocked)
        {
            imageContainer.sprite = image;
            nameText.text = "???";
            effectsText.text = "???";
        } 
        else
        {
            imageContainer.sprite = image;
            nameText.text = employeeName;
            effectsText.text = effectsDescription;
        }
    }

    public EmployeeClass(string EmployeeName, string EffectsDescription,string Description, Sprite Image, float Salary, float SalaryTimeSecs, float ClickPowerEffect, float ComputerAutoClickerEffect, List<string> Gens, List<float> ProductionEffects, List<float> SpeedEffects, List<float> AutoClickerEffects, List<bool> GenAutoClickers, bool Unlocked)
    {
        employeeName = EmployeeName;
        effectsDescription = EffectsDescription;
        description = Description;
        image = Image;
        salary = Convert.ToDecimal(Salary);
        salaryTimeSecs = Convert.ToDecimal(SalaryTimeSecs);
        clickPowerEffect = Convert.ToDecimal(ClickPowerEffect);
        computerAutoClickerEffect = Convert.ToDecimal(ComputerAutoClickerEffect);
        gens = (from o in Gens select GameObject.Find(o).GetComponent<GeneratorMaker>()).ToList();
        autoClickerEffects = (from o in AutoClickerEffects select Convert.ToDecimal(o)).ToList();
        productionEffects = (from o in ProductionEffects select Convert.ToDecimal(o)).ToList();
        speedEffects = (from o in SpeedEffects select Convert.ToDecimal(o)).ToList();
        genAutoClickers = GenAutoClickers;
        unlocked = Unlocked;
    }

    public EmployeeClass(string EmployeeName, string EffectsDescription, string Description, Sprite Image, decimal Salary, decimal SalaryTimeSecs, decimal ClickPowerEffect, decimal ComputerAutoClickerEffect, List<GeneratorMaker> Gens, List<decimal> ProductionEffects, List<decimal> SpeedEffects, List<decimal> AutoClickerEffects,List<bool> GenAutoClickers, bool Unlocked)
    {
        employeeName = EmployeeName;
        effectsDescription = EffectsDescription;
        description = Description;
        image = Image;
        salary = Salary;
        salaryTimeSecs = SalaryTimeSecs;
        clickPowerEffect = ClickPowerEffect;
        computerAutoClickerEffect = ComputerAutoClickerEffect;
        gens = Gens;
        autoClickerEffects = AutoClickerEffects;
        productionEffects = ProductionEffects;
        speedEffects = SpeedEffects;
        genAutoClickers = GenAutoClickers;
        unlocked = Unlocked;
    }
}
