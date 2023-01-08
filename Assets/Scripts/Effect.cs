using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Growable;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject
{
    public ImpactProperty ImpactPropery;
    public enum EffectAreas { Single, FullRowCol, ThreeRowColl, TwoX, ThreeX, FiveX }
    public EffectAreas EffectArea;
    public int EffectValue;
    public string Description;
}
