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

    public decimal globalMultiplier = 1;

    public int owned { get; private set; }

    public decimal cooldownLeft;
    public decimal cooldown;
    public bool unlocked { get; private set; }

    private GameObject cooldownBar;
    private GameObject buyObj;
    private GameObject timeLeftObj;

    public int AutoClickerAmounts;

    public AutoClickerClass autoclicker;

    public string name { get; private set; }

    public GeneratorMaker genMaker;

    public decimal employeeSpeedMultiplier = 1;
    public decimal employeeWorkerMultiplier = 1;
    public decimal employeeMultiplier = 1;
    public decimal calculatedCooldown;

    public MasterScript Master;
    public List<string> synergyGeneratorList = new List<string>();
    public List<float> SynergyValues = new List<float>();

    public void SetUp()
    {
        owned = 0;
        production = 0;
        cost = costBase;
        multiplier = 1;
        cooldownLeft = 0;
        Master = GameObject.Find("Master").GetComponent<MasterScript>();
        calculateRealCooldown();
    }

    public void CalculateProduction()
    {
        if (synergyGeneratorList.Count == 0)
        {
            production = (productionBase * owned) * multiplier * employeeMultiplier * globalMultiplier;
        }
        else
        {
            production = (productionBase * owned) * multiplier * employeeMultiplier;
            for (int i = 0; i < synergyGeneratorList.Count; i++)
            {
                production *= (Decimal)(1 + (SynergyValues[i] * GameObject.Find(synergyGeneratorList[i].Replace(" ","")).GetComponent<GeneratorMaker>().gen.owned));
            }
            

            production *= globalMultiplier;
            genMaker.UpdateTexts();
        }
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
    
        foreach (var i in Master.genList)
        {
            i.gen.CalculateProduction();
        }
    }

    public void unlock(bool ownedPlusPlus=true)
    {
        if (ownedPlusPlus)
        {
            owned += 1;
        }
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
        calculateRealCooldown();
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

    public void calculateRealCooldown() 
    {
        calculatedCooldown = cooldown / (employeeSpeedMultiplier * employeeWorkerMultiplier);
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
