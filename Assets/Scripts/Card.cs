using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public List<Effect> Effects;
}
