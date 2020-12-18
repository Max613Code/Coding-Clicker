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

    public decimal fastCounter;
    public decimal leftOverTime;

    public bool Going = true;

    public bool forComputer;

    private GeneratorMaker gen;

    public string genName;

    public int activeEmployeeCount;
    public float randomNum = 255;
    public Renderer renderer;

    public float counter = 2;
    public Color targetColor = new Color(130, 130, 130, 1);

    public IEnumerator IEStartAutoClick()
    {
        if (activeEmployeeCount > 0)
        {
            calculateCooldown();
        }
        while (Going)
        {
            if (calculatedCooldown > Convert.ToDecimal(0.1))
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
            else
            {
                fastCounter = (decimal)0.1;
                while (fastCounter > 0)
                {
                    fastCounter -= (decimal)Time.deltaTime;
                    yield return new WaitForSeconds(0);
                }
                if (forComputer)
                {
                    Debug.Log(calculatedCooldown);
                    for (int i = 0; i < Math.Floor(1/calculatedCooldown); i++)
                    {
                        ComputerHandler.ComputerClicked();
                    }
               
                }
                if (gen && gen.gen.unlocked)
                {
                    gen.ProduceMoney();
                    if (gen.gen.calculatedCooldown < (decimal)0.1)
                    {
                        renderer.material.SetColor("_Color",Color.Lerp(new Color(0.5f,0.5f,0.5f), new Color(0.9f,0.9f,0.9f), Mathf.PingPong(Time.time * 0.3f, 1)));
                    } 
                    else
                    {
                        gen.ProduceMoney();
                    }
                }
                leftOverTime += (Math.Floor(1 / calculatedCooldown) % 1);

                if (leftOverTime > calculatedCooldown)
                {
                    for (int i = 0; i < Math.Floor(leftOverTime / calculatedCooldown); i++)
                    {
                        if (forComputer)
                        {
                            ComputerHandler.ComputerClicked();
                        }
                    }
                    leftOverTime -= Math.Floor(leftOverTime / calculatedCooldown);
                }
              

                if (activeEmployeeCount == 0)
                {
                    Going = false;
                }
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
            renderer = gen.cooldownBar.GetComponent<Renderer>();
        }
        else
        {
            genName = "";
        }

        calculateCooldown();

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
        else if (!forComputer)
        {
            Debug.Log("The function calculateCooldowns called with no active employees.");
        }

        if (forComputer)
        {
            calculatedCooldown = cooldown;
        }
    }

}
