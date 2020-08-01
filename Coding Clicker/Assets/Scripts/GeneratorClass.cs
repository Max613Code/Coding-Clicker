using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneratorClass
{
    private decimal costBase;
    public decimal cost { get; private set; }
    private float growthRate;
    private decimal productionBase;
    public decimal production { get; private set; }

    public decimal multiplier { get; private set; }

    public int owned { get; private set; }

    public decimal cooldownLeft { get; private set; }
    public decimal cooldown { get; private set; }
    public bool unlocked { get; private set; }

    private GameObject cooldownBar;
    private GameObject buyObj;
    private GameObject timeLeftObj;

    public string name { get; private set; }

    public void SetUp()
    {
        owned = 0;
        production = 0;
        cost = costBase;
        multiplier = 1;
        cooldownLeft = 0;
    }

    public void CalculateProduction()
    {
        production = (productionBase * owned) * multiplier;
    }

    public void CalculateCost()
    {
        cost = (decimal)Math.Round((double)costBase * Math.Pow(growthRate, owned), 2);
    }

    public void Buy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            owned += 1;
            CalculateProduction();
            CalculateCost();
        }
    }

    public void unlock()
    {
        owned += 1;
        unlocked = true;
        CalculateProduction();
        CalculateCost();
    }

    public void SubCoolDownLeft(decimal value)
    {
        cooldownLeft -= value;
    }

    public void SetCoolDownLeft(decimal value)
    {
        cooldownLeft = value;
    }

    public void SetCoolDown(decimal value)
    {
        cooldown = value;
    }

    public void Multiply(decimal value)
    {
        multiplier *= value;
    }

    public GeneratorClass(string Name, decimal CostBase, float GrowthRate, decimal ProductionBase, decimal CoolDown, GameObject CooldownBar, GameObject BuyObj, GameObject TimeLeftObj, bool Unlocked)
    {
        name = Name;
        costBase = CostBase;
        growthRate = GrowthRate; 
        productionBase = ProductionBase;
        cooldown = CoolDown;
        cooldownBar = CooldownBar;
        buyObj = BuyObj;
        timeLeftObj = TimeLeftObj;
        unlocked = Unlocked;
        SetUp();
    }

}
