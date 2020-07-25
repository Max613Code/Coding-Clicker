using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneratorClass
{
    private float costBase;
    public float cost { get; private set; }
    private float growth_rate;
    private float productionBase;
    public float production { get; private set; }

    public float multiplier { get; private set; }

    public int owned { get; private set; }

    public float cooldownPercent { get; private set; }
    public float cooldown { get; private set; }

    public bool unlocked { get; private set; }

    private GameObject cooldownBar;
    private GameObject buyObj;
    public GameObject timeLeftObj;

    public void SetUp()
    {
        production = productionBase;
        cost = costBase;
        multiplier = 1;
    }

    public void CalculateProduction()
    {
        production = (productionBase * owned) * multiplier;
    }

    public void CalculateCost()
    {
        cost = (float)(costBase * Math.Pow(costBase, owned));
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

    public GeneratorClass(float CostBase, float ProductionBase, float CoolDown, GameObject CooldownBar, GameObject BuyObj, GameObject TimeLeftObj, bool Unlocked)
    {
        costBase = CostBase;
        productionBase = ProductionBase;
        cooldown = cooldown;
        cooldownBar = CooldownBar;
        buyObj = BuyObj;
        timeLeftObj = TimeLeftObj;
        unlocked = Unlocked;
        SetUp();
    }

}
