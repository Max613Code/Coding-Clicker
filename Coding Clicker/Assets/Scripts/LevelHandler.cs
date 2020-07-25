using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHandler
{
    public static float money { get; private set; }

    public static void SetMoney(float Money)
    {
        money = Money;
    }

    public static void AddMoney(float Money)
    {
        money += Money;
    }

}
