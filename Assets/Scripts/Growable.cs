using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Growable", menuName = "Growable")]
public class Growable : ScriptableObject
{
    public enum Impact { Negative, Neutral, Positive }
    public enum ImpactProperty { Temperature, Sun, Water }

    public string Name;
    public string Description;

    public List<Impact> TempImpact;
    public List<Impact> SunImpact;
    public List<Impact> WaterImpact;

    public int ImpactMultiplier;

    public Sprite Sprite;
}
