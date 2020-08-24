using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class RandomFunctions
{
    public static System.Random rand = new System.Random();

    public static int RandomInt(int min, int max)
    {
        return rand.Next(min, max);
    }

    public static void Randomize()
    {
        for (int _ = 0; _ < rand.Next(1,10); _++)
        {
            rand.Next();
        }
    }
}
