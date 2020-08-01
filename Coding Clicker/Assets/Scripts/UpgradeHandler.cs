using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    public List<UpgradeSOScript> upgradeList;

    public GameObject UpgradePrefab;

    public GameObject UpgradeContainer;

    private UpgradeClass upgClass;

    private void Start()
    {
        upgradeList = upgradeList.OrderBy( o => o.cost ).ToList();
        foreach (UpgradeSOScript SO in upgradeList)
        {
            if (SO.generatorName != "")
            {
                upgClass = new UpgradeClass(SO.upgradeName, SO.upgradeDescription, (decimal)SO.cost, SO.multiplier, SO.dropDown.ToString(), GameObject.Find(SO.generatorName).GetComponent<GeneratorMaker>(), SO.UpgradeImage, SO.UpgradeEffectOnWhatImage, SO.ComputerUpgradeImage, SO.AutoClickerCooldown);
            }
            else
            {
                upgClass = new UpgradeClass(SO.upgradeName, SO.upgradeDescription, (decimal)SO.cost, SO.multiplier, SO.dropDown.ToString(), null, SO.UpgradeImage, SO.UpgradeEffectOnWhatImage, SO.ComputerUpgradeImage, SO.AutoClickerCooldown);
            }
            GameObject upgrade= (GameObject)Instantiate(UpgradePrefab);
            upgrade.transform.SetParent(UpgradeContainer.transform, worldPositionStays: false);

            UpgradeScript script = upgrade.GetComponent<UpgradeScript>();
            script.upgClass = upgClass;
            script.SetUp();

            UIHandler.RePosUpgradeUI();
        }
    }
}
