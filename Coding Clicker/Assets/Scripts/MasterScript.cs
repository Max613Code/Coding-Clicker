using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{

    private GeneratorMaker BlockCoding;
    private GeneratorMaker Console;
    private GeneratorMaker GUIs;
    public bool ComputerAutoClickerExists;
    public AutoClickerClass autoClickerClass;

    private decimal PlayTime;


    private void Start()
    {
        LevelHandler.AddMoney(1234123334);
        UIHandler.UpdateMoney(LevelHandler.money);
        BlockCoding = GameObject.Find("BlockCoding").GetComponent<GeneratorMaker>();
        Console = GameObject.Find("Console").GetComponent<GeneratorMaker>();
        GUIs = GameObject.Find("GUIs").GetComponent<GeneratorMaker>();
        ComputerAutoClickerExists = false;
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
    }

    private void Update()
    {
        PlayTime += (decimal)Time.deltaTime;
    }

}