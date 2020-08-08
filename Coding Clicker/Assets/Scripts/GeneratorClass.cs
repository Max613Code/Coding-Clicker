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

    public int AutoClickerAmounts;

    public AutoClickerClass autoclicker;

    public string name { get; private set; }

    public GeneratorMaker genMaker;

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

    public void Load(int Owned = 0, decimal Cost = 0, decimal Production = 0, decimal Cooldown = -1, decimal CooldownLeft = -1, decimal Multiplier = 1)
    {
        owned = Owned;
        cost = Cost;
        production = Production;
        if (Cooldown != -1)
        {
            cooldown = Cooldown;
        }

        if (cooldownLeft > 0)
        {
            genMaker.ProduceMoney(false);
        }
        else if (CooldownLeft == -2)
        {
            cooldownLeft = 0;
        }
        else if (CooldownLeft != -1)
        {
            cooldownLeft = CooldownLeft;
        }
        multiplier = Multiplier;
        
        
    }

    public void SetMult(decimal Multiplier)
    {
        multiplier = Multiplier;
    }

    public GeneratorClass(string Name, decimal CostBase, float GrowthRate, decimal ProductionBase, decimal CoolDown, GameObject CooldownBar, GameObject BuyObj, GameObject TimeLeftObj, bool Unlocked, GeneratorMaker GenMaker)
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
        genMaker = GenMaker;
        SetUp();
    }

}
