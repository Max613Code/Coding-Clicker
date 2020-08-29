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
    public decimal multiplier { get; private set; }
    public string action { get; private set; }
    public GeneratorMaker genScript { get; private set; }

    public bool Unlocked { get; private set; }

    public Sprite image { get; private set; }
    public Sprite upgradeEffectOnWhatImg { get; private set; }

    public Sprite computerUpgradeImage { get; private set; }

    public float autoClickerCooldown;

    public MasterScript Master;
    
    public UpgradeClass(string Name, string Description, decimal Cost, float Multiplier, string Action, GeneratorMaker GenScript, Sprite img, Sprite UpgradeEffectOnWhatImg, Sprite ComputerUpgradeImage, float autoClickCooldown)
    {
        name = Name;
        description = Description.Replace("\\n", "\n").Replace("*", "×");
        cost = Cost;
        multiplier = (decimal)Multiplier;
        action = Action;
        genScript = GenScript;
        Unlocked = false;
        image = img;
        upgradeEffectOnWhatImg = UpgradeEffectOnWhatImg;
        computerUpgradeImage = ComputerUpgradeImage;
        autoClickerCooldown = autoClickCooldown;

        Master = GameObject.Find("Master").GetComponent<MasterScript>();
    }

    public void Buy()
    {
        if (LevelHandler.money >= cost)
        {
            LevelHandler.SubtractMoney(this.cost);
            UIHandler.UpdateMoney();
            Unlocked = true;
            Master.CalculateMPS();
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
                if (Master.ComputerAutoClickerExists == false)
                {
                    Master.ComputerAutoClickerExists = true;
                    Master.autoClickerClass = new AutoClickerClass((decimal)autoClickerCooldown, true);
                    Master.autoClickers.Add(Master.autoClickerClass);
                    Master.StartComputerAutoClick();
                }
            }
            else if (action == "GeneratorAutoClicker")
            {
                if (genScript.gen.AutoClickerAmounts == 0)
                {
                    genScript.gen.autoclicker = new AutoClickerClass((decimal)autoClickerCooldown, false, -1, genScript);
                    Master.autoClickers.Add(genScript.gen.autoclicker);
                    genScript.StartAutoClick();
                }
            }
            else if (action == "GlobalProduction")
            {
                Master.GlobalProductionMultiply(multiplier);
            }
            else if (action == "GlobalSpeed")
            {
                Master.GlobalSpeedMultiply(multiplier);
            }
        }
    }
}
