using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeClass
{
    public string name;
    public string description;

    public Sprite image;

    [Header("Block Coding Effects")]
    public List<decimal> blockCodingProductionEffects;
    public List<decimal> blockCodingSpeedEffects;
    [Header("Console Effects")]
    public List<decimal> consoleProductionEffects;
    public List<decimal> consoleSpeedEffects;
    [Header("GUIS Effects")]
    public List<decimal> GUISProductionEffects;
    public List<decimal> GUISSpeedEffects;
    [Header("Global Effects")]
    public List<decimal> globalProductionEffects;
    public List<decimal> globalSpeedEffects;
}
