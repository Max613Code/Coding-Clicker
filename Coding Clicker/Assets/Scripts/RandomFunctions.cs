using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomFunctions : MonoBehaviour
{
    public System.Random rand = new System.Random();

    public int RandomInt(int min, int max)
    {
        return rand.Next(min, max);
    }
}
