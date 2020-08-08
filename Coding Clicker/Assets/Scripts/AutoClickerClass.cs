using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerClass
{

    public decimal cooldown;

    public decimal cooldownLeft;

    public bool Going = true;

    public bool forComputer;

    private GeneratorMaker gen;

    public string genName;

    public IEnumerator IEStartAutoClick()
    {
        while (Going)
        {
            cooldownLeft = cooldown;
            while (cooldownLeft > 0)
            {
                cooldownLeft -= (decimal)Time.deltaTime;
                yield return new WaitForSeconds(0);
            }
            if (forComputer)
            {
                ComputerHandler.ComputerClicked();
            }
            else
            {
                gen.ProduceMoney();
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
    }

}
