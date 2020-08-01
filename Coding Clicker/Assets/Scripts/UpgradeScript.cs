using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    public UpgradeClass upgClass;

    public RectTransform UnlockedUpgrades;

    public void Buy()
    {
        if (LevelHandler.money >= upgClass.cost)
        {
            upgClass.Buy();
            gameObject.transform.SetParent(UnlockedUpgrades);
            UIHandler.RePosUpgradeUI();
            UIHandler.RePosUnlockedUpgradesUI();
        }
    }

    public void SetUp()
    {
        gameObject.transform.Find("Name").GetComponent<Text>().text = upgClass.name;
        gameObject.transform.Find("Description").GetComponent<Text>().text = upgClass.description;
        gameObject.transform.Find("UpgradeImage").GetComponent<Image>().sprite = upgClass.image;
        gameObject.transform.Find("CostText").GetComponent<Text>().text = "$" + upgClass.cost.ToString();
        gameObject.transform.Find("UpgradeEffectOnWhatImage").GetComponent<Image>().sprite = upgClass.upgradeEffectOnWhatImg;
        UnlockedUpgrades = GameObject.Find("UnlockedUpgradesContainer").GetComponent<RectTransform>();
    }
}
