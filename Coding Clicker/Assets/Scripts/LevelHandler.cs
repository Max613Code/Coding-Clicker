using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHandler
{
    public static decimal money { get; private set; }

    public static void SetMoney(decimal Money)
    {
        money = Money;
    }

    public static void AddMoney(decimal Money)
    {
        money += Money;
    }

    public static void SubtractMoney(decimal Money)
    {
        money -= Money;
    }

}
