using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClickerClass
{

    private decimal cooldown;

    private decimal cooldownLeft;

    private bool Going = true;

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
            ComputerHandler.ComputerClicked();
        }
    }

    public AutoClickerClass(decimal Cooldown)
    {
        cooldown = Cooldown;
        cooldownLeft = cooldown;
    }

    public void setCooldown(decimal Cooldown)
    {
        cooldownLeft *= (Cooldown / cooldown);
        cooldown = Cooldown;
    }

}
