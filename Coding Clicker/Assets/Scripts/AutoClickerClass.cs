using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerClass
{

    public decimal cooldown;
    public decimal calculatedCooldown;

    public decimal cooldownLeft;
    public decimal calculatedCooldownLeft;

    public bool Going = true;

    public bool forComputer;

    private GeneratorMaker gen;

    public string genName;

    public int activeEmployeeCount;

    public IEnumerator IEStartAutoClick()
    {
        if (activeEmployeeCount > 0)
        {
            calculateCooldown();
        }
        while (Going)
        {
            calculatedCooldownLeft = calculatedCooldown;
            cooldownLeft = cooldown;
            while (calculatedCooldownLeft > 0)
            {
                calculatedCooldownLeft -= (decimal)Time.deltaTime;
                yield return new WaitForSeconds(0);
            }
            if (forComputer)
            {
                ComputerHandler.ComputerClicked();
            }
            if (gen && gen.gen.unlocked && !gen.producing)
            {
                gen.ProduceMoney();
            }

            if (activeEmployeeCount == 0)
            {
                Going = false;
            }
        }
    }

    public AutoClickerClass(decimal Cooldown, bool ForComputer,decimal CooldownLeft = -1, GeneratorMaker Gen = null )
    {
        cooldown = Cooldown;
        forComputer = ForComputer;
        gen = Gen;
        if (cooldownLeft == -1)
        {
            cooldownLeft = cooldown;
        }
        else
        {
            cooldownLeft = CooldownLeft;
        }

        if (gen != null)
        {
            genName = gen.gen.name;
        }
        else
        {
            genName = "";
        }
    }

    public void setCooldown(decimal Cooldown)
    {
        cooldownLeft *= (Cooldown / cooldown);
        cooldown = Cooldown;
        calculateCooldown();
    }

    public void calculateCooldown()
    {
        if (activeEmployeeCount != 0) 
        {
            calculatedCooldown = cooldown * Convert.ToDecimal(Mathf.Clamp((activeEmployeeCount - 1),1, float.MaxValue)) / (activeEmployeeCount);
            if (calculatedCooldownLeft != calculatedCooldown)
            {
                calculatedCooldownLeft = calculatedCooldownLeft * (activeEmployeeCount - 1) / (activeEmployeeCount);
            }
        }
        else
        {
            Debug.Log("The function calculateCooldowns called with no active employees.");
        }
    }

}
