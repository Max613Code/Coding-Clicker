using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class UpgradeClass
{

    public string name { get; private set; }
    public string description { get; private set; }

    public decimal cost { get; private set; }
    public float multiplier { get; private set; }
    public string action { get; private set; }
    public GeneratorMaker genScript { get; private set; }

    public bool Unlocked { get; private set; }

    public Sprite image { get; private set; }
    public Sprite upgradeEffectOnWhatImg { get; private set; }

    public Sprite computerUpgradeImage { get; private set; }

    public float autoClickerCooldown;
    
    public UpgradeClass(string Name, string Description, decimal Cost, float Multiplier, string Action, GeneratorMaker GenScript, Sprite img, Sprite UpgradeEffectOnWhatImg, Sprite ComputerUpgradeImage, float autoClickCooldown)
    {
        name = Name;
        description = Description.Replace("\\n", "\n").Replace("*", "×");
        cost = Cost;
        multiplier = Multiplier;
        action = Action;
        genScript = GenScript;
        Unlocked = false;
        image = img;
        upgradeEffectOnWhatImg = UpgradeEffectOnWhatImg;
        computerUpgradeImage = ComputerUpgradeImage;
        autoClickerCooldown = autoClickCooldown;
    }

    public void Buy()
    {
        if (LevelHandler.money >= cost)
        {
            LevelHandler.SubtractMoney(this.cost);
            UIHandler.UpdateMoney();
            Unlocked = true;
            if (action == "GeneratorProductionBase")
            {
                genScript.Multiply((decimal)multiplier);
            }
            else if (action == "ComputerClickPower")
            {
                ComputerHandler.MultiplyClickPower((decimal)multiplier);
            }
            else if (action == "GeneratorSpeed")
            {
                genScript.SpeedUp((decimal)multiplier);
            }
            else if (action == "UpgradeComputerType")
            {
                ComputerHandler.MultiplyClickPower((decimal)multiplier);
                ComputerHandler.ChangeComputerImage(computerUpgradeImage);
            }
            else if (action == "ComputerAutoClicker")
            {
                MasterScript Master = GameObject.Find("Master").GetComponent<MasterScript>();
                if (!Master.ComputerAutoClickerExists)
                {
                    Master.ComputerAutoClickerExists = true;
                    Master.autoClickerClass = new AutoClickerClass((decimal)autoClickerCooldown);
                    Master.StartComputerAutoClick();
                }
            }
        }
    }
}
