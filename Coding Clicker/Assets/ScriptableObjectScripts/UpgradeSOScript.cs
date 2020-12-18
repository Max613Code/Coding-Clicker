using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeSO", order = 1)]
public class UpgradeSOScript : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;

    public float cost;
    
    public Sprite UpgradeImage;
    public Sprite UpgradeEffectOnWhatImage;

    public UpgradeClass myUpgradeClass;
    public enum SelectionMenu
    {
        ComputerClickPower,
        ComputerAutoClicker,
        UpgradeComputerType,
        GeneratorProductionBase,
        GeneratorSpeed,
        GeneratorAutoClicker,
        GlobalProduction,
        GlobalSpeed
    };
    
    public SelectionMenu dropDown = SelectionMenu.ComputerClickPower;

    

    public float multiplier;

    public string generatorName;

    
    
    public Sprite ComputerUpgradeImage;


    public float AutoClickerCooldown;

    public int tier;
}