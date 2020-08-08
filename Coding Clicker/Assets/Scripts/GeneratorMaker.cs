using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorMaker : MonoBehaviour
{
    [Header("Physical Objects")]
    public GameObject button;
    public GameObject cooldownBar;
    public GameObject buy;
    public GameObject timer;
    public Text productionAmountText;
    public Text costAmountText;
    public Text timerText;
    public Text amountText;
    public Sprite LockImage;
    public Sprite Image;

    [Header("Generator Params")]
    public string genName;
    public float costBase;
    public float growthRate;
    public float productionBase;
    public float cooldown;

    public GeneratorClass gen { get; private set; }
    private bool producing = false;
    private Vector3 startPos;
    private Vector3 startScale;
    private decimal widthIncrease;

    private void Start()
    {
        gen = new GeneratorClass(genName, (decimal)costBase, growthRate, (decimal)productionBase, (decimal)cooldown, cooldownBar, buy, timer, false, this);
        startPos = cooldownBar.transform.position;
        startScale = cooldownBar.transform.localScale;

        button.GetComponent<Image>().sprite = LockImage;

        amountText.text = "";
        amountText.transform.GetChild(0).gameObject.SetActive(false);

        UpdateTexts();

    }

    public void Buy(int amount)
    {
        if (LevelHandler.money >= (decimal)gen.cost)
        {
            LevelHandler.SubtractMoney((decimal)gen.cost);
            if (gen.owned == 0)
            {
                gen.unlock();
                amountText.transform.GetChild(0).gameObject.SetActive(true);
                UpdateTexts();
                button.GetComponent<Image>().sprite = Image;
                
            }
            else
            {
                gen.Buy(amount);
                UpdateTexts();
            }
        }
    }

    public void LoadUnlock()
    {
        gen.unlock();
        amountText.transform.GetChild(0).gameObject.SetActive(true);
        UpdateTexts();
        button.GetComponent<Image>().sprite = Image;
        UpdateTexts();
    }

    public void ProduceMoney(bool ResetCooldownLeft = true)
    {
        StartCoroutine(ProduceMoneyIE(ResetCooldownLeft));
    }

    public IEnumerator ProduceMoneyIE(bool ResetCooldownLeft = true)
    {
        if (!producing)
        {
            producing = true;
            widthIncrease = (decimal)cooldownBar.transform.localScale.x/gen.cooldown;
            cooldownBar.transform.localScale = new Vector3(0, cooldownBar.transform.localScale.y, cooldownBar.transform.localScale.z);

            if (!ResetCooldownLeft)
            {
                cooldownBar.transform.localScale = new Vector3(cooldownBar.transform.localScale.x + (float)(gen.cooldown - gen.cooldownLeft) * (float)widthIncrease, cooldownBar.transform.localScale.y, cooldownBar.transform.localScale.z);
                cooldownBar.transform.position = new Vector3(startPos.x - startScale.x / 2 + (cooldownBar.transform.localScale.x / 2), startPos.y, startPos.z);
            }
            else
            {
                gen.SetCoolDownLeft(gen.cooldown);
            }
            while (gen.cooldownLeft > 0)
            {
                cooldownBar.transform.localScale = new Vector3(cooldownBar.transform.localScale.x + Time.deltaTime * (float)widthIncrease, cooldownBar.transform.localScale.y, cooldownBar.transform.localScale.z);
                cooldownBar.transform.position = new Vector3(startPos.x - startScale.x/2 + (cooldownBar.transform.localScale.x/2), startPos.y, startPos.z);
                gen.SubCoolDownLeft((decimal)Time.deltaTime);
                yield return new WaitForSeconds(0);
                timerText.text = Math.Round(gen.cooldownLeft,2).ToString() + " s";
            }
            gen.SetCoolDownLeft(0);
            LevelHandler.AddMoney((decimal)gen.production);
            cooldownBar.transform.position = startPos;
            cooldownBar.transform.localScale = startScale;
            producing = false;
            UpdateTexts();
        }
    }

    public void UpdateTexts()
    {
        productionAmountText.text = "$" + gen.production.ToString();
        costAmountText.text = "$" + gen.cost.ToString();
        timerText.text = gen.cooldownLeft.ToString() + " s";
        if (gen.owned != 0)
        {
            amountText.text = gen.owned.ToString();
        }
        UIHandler.UpdateMoney();
    }

    public void Multiply(decimal value)
    {
        gen.Multiply(value);
        gen.CalculateProduction();
        UpdateTexts();
    }

    public void SpeedUp(decimal value)
    {
        gen.SetCoolDown(gen.cooldown / value);
        gen.SetCoolDownLeft(gen.cooldownLeft / value);
    }

    public void StartAutoClick()
    {
        StartCoroutine(gen.autoclicker.IEStartAutoClick());
    }
    
}