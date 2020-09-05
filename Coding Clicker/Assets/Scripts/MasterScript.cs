using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MasterScript : MonoBehaviour
{

    private GeneratorMaker BlockCoding;
    private GeneratorMaker Console;
    private GeneratorMaker GUIs;

    private decimal playTime;
    public decimal mps { get; private set; }

    public UpgradeHandler UH;

    public List<bool> upgradesUnlockedList;

    private List<GeneratorMaker> genList;

    private List<String> genNames;

    public TimeSpan idleTime;
    public decimal idleTimeSecs;
    public decimal timeLeft;

    public decimal idleMoneyEarned;

    public RectTransform IdleAwayStatsInfo;

    public GameObject functions;

    private CameraHandler cameraHandler;
    
    [Header("Don't need to set up.")]

    public bool ComputerAutoClickerExists;
    public AutoClickerClass autoClickerClass;

    public List<AutoClickerClass> autoClickers = new List<AutoClickerClass>();

    public AutoClickerClass autoClickerLoad;
    private decimal autoClickerAmountEarned;
    public GeneratorMaker genAutoClickerLoad;

    public decimal globalProductionMult = 1;
    public decimal globalSpeedMult = 1;

    public EmployeeHandler employeeHandler;

    public struct ClickerStates
    {
        //String values used because json maker doesn't work with decimals.
        public string money;
        public string clickPower;
        public List<int> generatorCount;
        public List<string> generatorCost;
        public List<string> generatorProduction;
        public List<string> generatorCooldown;
        public List<string> generatorCooldownLeft;
        public List<string> generatorMultipliers;
        public List<bool> upgrades;
        public string saveTime;

        public string playTime;

        public List<string> acCooldowns;
        public List<string> acCooldownLefts;
        public List<bool> acForComputers;
        public List<string> acGenNames;

        public List<bool> employeesUnlocked;
        public List<string> employeePoints;
        public List<bool> employeeWorking;
        public List<string> employeeSalaryCooldowns;
        public List<string> employeeSalaryCooldownLefts;

        public string globalProduction;
        public string globalSpeed;
    }

    public ClickerStates ClickerState;

    private void Start()
    {
        
        SaveSystem.Init();

        ComputerHandler.SetClickPower(1);
        LevelHandler.AddMoney(1234123334);
        UIHandler.UpdateMoney(LevelHandler.money);
        BlockCoding = GameObject.Find("BlockCoding").GetComponent<GeneratorMaker>();
        Console = GameObject.Find("Console").GetComponent<GeneratorMaker>();
        GUIs = GameObject.Find("GUIs").GetComponent<GeneratorMaker>();

        genList = new List<GeneratorMaker>() { BlockCoding, Console, GUIs };
        genNames = (from gen in genList select gen.gen.name).ToList();

        ComputerAutoClickerExists = false;
        
        upgradesUnlockedList = Enumerable.Repeat(false, gameObject.GetComponent<UpgradeHandler>().upgradeList.Count).ToList();

        cameraHandler = functions.GetComponent<CameraHandler>();

        RandomFunctions.Randomize();

        autoClickers = Enumerable.Repeat((AutoClickerClass)null, genList.Count()).ToList();

        employeeHandler = gameObject.GetComponent<EmployeeHandler>();

        loadSavedData();
    }
    
    private void loadSavedData()
    {
        if (SaveSystem.SaveFound())
        {
            ClickerState = new ClickerStates();
            ClickerStates loadedSave = JsonUtility.FromJson<ClickerStates>(SaveSystem.Load());
            StartCoroutine(LoadingValues(loadedSave));
        }
    }

    public void saveData()
    {
        ClickerStates state = new ClickerStates
        {
            money = LevelHandler.money.ToString(),
            clickPower = ComputerHandler.clickPower.ToString(),
            generatorCount = (from gen in genList select gen.gen.owned).ToList(),
            generatorCost = (from gen in genList select gen.gen.cost.ToString()).ToList(),
            generatorProduction = (from gen in genList select gen.gen.production.ToString()).ToList(),
            generatorCooldown = (from gen in genList select gen.gen.cooldown.ToString()).ToList(),
            generatorCooldownLeft = (from gen in genList select gen.gen.cooldownLeft.ToString()).ToList(),
            generatorMultipliers = (from gen in genList select gen.gen.multiplier.ToString()).ToList(),
            upgrades = upgradesUnlockedList,
            saveTime = DateTime.Now.ToString(),


            playTime = playTime.ToString(),

            acCooldowns = (from ac in autoClickers select ((ac != null) ? Convert.ToString(ac.cooldown) : "-1")).ToList(),
            acCooldownLefts = (from ac in autoClickers select ((ac != null) ? Convert.ToString(ac.cooldownLeft) : "-1")).ToList(),
            acForComputers = (from ac in autoClickers select ((ac != null) ? ac.forComputer : false)).ToList(),
            acGenNames = (from ac in autoClickers select ((ac != null) ? ac.genName : "AutoClicker not set.")).ToList(),

            employeesUnlocked = (from employee in employeeHandler.employeeClasses select employee.unlocked).ToList(),
            employeePoints = (from employee in employeeHandler.employeeClasses select employee.employeePoints.ToString()).ToList(),
            employeeWorking = (from employee in employeeHandler.employeeClasses select employee.working).ToList(),
            employeeSalaryCooldowns = (from employee in employeeHandler.employeeClasses select employee.salaryTimeSecs.ToString()).ToList(),
            employeeSalaryCooldownLefts = (from employee in employeeHandler.employeeClasses select employee.timeCounter.ToString()).ToList(),

            globalProduction = globalProductionMult.ToString(),
            globalSpeed = globalSpeedMult.ToString()
        };
        string json = JsonUtility.ToJson(state);
        SaveSystem.Save(json);
    }

    public IEnumerator LoadingValues(ClickerStates loadedSave)
    {
        yield return new WaitForSeconds((float)0.5);

        idleTime = (DateTime.Now - Convert.ToDateTime(loadedSave.saveTime));
        idleTimeSecs = (decimal)idleTime.TotalSeconds;

        ComputerHandler.SetClickPower(Convert.ToDecimal(loadedSave.clickPower));

        LevelHandler.SetMoney(Convert.ToDecimal(loadedSave.money));
        UIHandler.UpdateMoney();

        globalSpeedMult = Convert.ToDecimal(loadedSave.globalSpeed);
        globalProductionMult = Convert.ToDecimal(loadedSave.globalProduction);

        for (int i = 0; i < loadedSave.generatorCount.Count; i++)
        {
            int count = loadedSave.generatorCount[i];
            genList[i].gen.globalMultiplier = globalProductionMult;

            if (count > 0)
            {
                timeLeft = (Convert.ToDecimal(loadedSave.generatorCooldownLeft[i])-idleTimeSecs);
                genList[i].gen.Load(count,Convert.ToDecimal(loadedSave.generatorCost[i]), Convert.ToDecimal(loadedSave.generatorProduction[i]), Convert.ToDecimal(loadedSave.generatorCooldown[i]), (timeLeft > 0)?timeLeft:-2 , Convert.ToDecimal(loadedSave.generatorMultipliers[i]));
                genList[i].LoadUnlock();
                if (timeLeft < 0)
                {
                    idleMoneyEarned += genList[i].gen.production;
                }
                else
                {
                    genList[i].ProduceMoney(false);
                }
            }
            else
            {
                genList[i].gen.SetMult(Convert.ToDecimal(loadedSave.generatorMultipliers[i]));
            }
            
        }
        for (int i = 0; i < loadedSave.upgrades.Count(); i++)
        {
            if (loadedSave.upgrades[i])
            {
                UH.upgradeScriptList[i].SwitchParent();
            }
        }
        UIHandler.RePosUpgradeUI();
        UIHandler.RePosUnlockedUpgradesUI();

        for (int i = 0; i < loadedSave.acCooldowns.Count(); i++)
        {
            if (loadedSave.acForComputers[i])
            {
                this.autoClickerClass = new AutoClickerClass(Convert.ToDecimal(loadedSave.acCooldowns[i]), true, Convert.ToDecimal(loadedSave.acCooldownLefts[i]));
                this.ComputerAutoClickerExists = true;
                autoClickerAmountEarned = ((idleTimeSecs - Convert.ToDecimal(loadedSave.acCooldownLefts[i])) > 0) ? (((int)((idleTimeSecs - Convert.ToDecimal(loadedSave.acCooldownLefts[i])) / this.autoClickerClass.cooldown)) * ComputerHandler.clickPower) : 0;
                this.StartComputerAutoClick();
                autoClickers[i] = this.autoClickerClass;
                idleMoneyEarned += autoClickerAmountEarned;
            }
            else if (loadedSave.acGenNames[i] != "AutoClicker not set.") 
            { 
                if (loadedSave.acGenNames[i].Equals("") == false) 
                {
                    genAutoClickerLoad = genList[genNames.IndexOf(loadedSave.acGenNames[i])];
                    genAutoClickerLoad.gen.AutoClickerAmounts += 1;
                    autoClickerAmountEarned = ((idleTimeSecs - Convert.ToDecimal(loadedSave.acCooldownLefts[i])) > 0) ? (((int)((idleTimeSecs - Convert.ToDecimal(loadedSave.acCooldownLefts[i])) / Convert.ToDecimal(loadedSave.acCooldowns[i]))) * genAutoClickerLoad.gen.production) : 0;
                    genAutoClickerLoad.gen.autoclicker = new AutoClickerClass(Convert.ToDecimal(loadedSave.acCooldowns[i]), true, Convert.ToDecimal(loadedSave.acCooldownLefts[i]));
                    genAutoClickerLoad.StartAutoClick();
                    autoClickers[i] = genAutoClickerLoad.gen.autoclicker;
                    idleMoneyEarned += autoClickerAmountEarned;
                }
            }
        }

        for (int i = 0; i < loadedSave.employeesUnlocked.Count; i++)
        {
            if (loadedSave.employeesUnlocked[i])
            {
                employeeHandler.employeeClasses[i].Unlock();
                employeeHandler.employeeClasses[i].employeePoints = Convert.ToDecimal(loadedSave.employeePoints[i]);
                decimal salaryTotal = ((idleTimeSecs - Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i])) > 0) ? (((int)((idleTimeSecs - Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i])) / Convert.ToDecimal(loadedSave.employeeSalaryCooldowns[i]))) * employeeHandler.employeeClasses[i].salary) : 0;
                if (loadedSave.employeeWorking[i]) 
                {
                    employeeHandler.employeeClasses[i].StartWorking();
                }
                if ((idleTimeSecs - Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i])) > 0) 
                {
                    employeeHandler.employeeClasses[i].timeCounter = idleTimeSecs - Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i]);
                }
                else
                {
                    employeeHandler.employeeClasses[i].timeCounter = (idleTimeSecs - Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i])) % Convert.ToDecimal(loadedSave.employeeSalaryCooldownLefts[i]);
                }
                idleMoneyEarned -= salaryTotal;
            }
        }

        playTime = Convert.ToDecimal(loadedSave.playTime);

        IdleAwayStatsInfo.Find("MoneyEarnedText").GetComponent<Text>().text = "While you were away, you earned $" + idleMoneyEarned + "!";
        IdleAwayStatsInfo.Find("AwayTimeText").GetComponent<Text>().text = "You were away for " + idleTime + ".";

        LevelHandler.AddMoney(idleMoneyEarned);
        UIHandler.UpdateMoney();

        cameraHandler.SwitchToCam3();
        CalculateMPS();
    }

    public void GlobalProductionMultiply(decimal amount)
    {
        foreach(GeneratorMaker gen in genList)
        {
            gen.gen.globalMultiplier *= amount;
            gen.UpdateTexts();
        }
        ComputerHandler.globalMult *= amount;
        globalProductionMult *= amount;
    }
    
    public void GlobalSpeedMultiply(decimal amount)
    {
        foreach(GeneratorMaker gen in genList)
        {
            gen.SpeedUp(amount);
            gen.UpdateTexts();
        }
        globalSpeedMult *= amount;
    }

    public AutoClickerClass CreateGeneratorAutoClicker(GeneratorMaker gen)
    {
        var autoClicker = new AutoClickerClass(Convert.ToDecimal(gen.defualtAutoClickerCooldown), false, -1, gen);

        autoClickers[genList.IndexOf(gen)] = autoClicker;

        return autoClicker;
    }

    public void ComputerClick()
    {
        ComputerHandler.ComputerClicked();
    }

    public void StartComputerAutoClick()
    {
        StartCoroutine(autoClickerClass.IEStartAutoClick());
    }
    

    public void BlockCodingClick()
    {
        if (BlockCoding.gen.unlocked)
        {
            BlockCoding.ProduceMoney();
        }
    }

    public void BlockCodingBuy()
    {
        BlockCoding.Buy(1);
        CalculateMPS();
    }

    public void ConsoleClick()
    {
        if (Console.gen.unlocked)
        {
            Console.ProduceMoney();
        }
    }

    public void ConsoleBuy()
    {
        Console.Buy(1);
        CalculateMPS();
    }

    public void GUIsClick()
    {
        if (GUIs.gen.unlocked)
        {
            GUIs.ProduceMoney();
        }
    }

    public void GUIsBuy()
    {
        GUIs.Buy(1);
        CalculateMPS();
    }

    public void CalculateMPS()
    {
        mps = 0;
        if (genList.Count > 0)
        {
            foreach (GeneratorMaker gen in genList)
            {
                mps += gen.gen.production / gen.gen.cooldown;
            }
        }
        mps += ComputerHandler.clickPower;
    }

    private void Update()
    {
        playTime += (decimal)Time.deltaTime;
    }

}