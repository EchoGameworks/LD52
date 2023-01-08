using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int MaxRounds = 20;
    public List<GameObject> Tiles;
    public List<GameObject> Growables;
    public List<WorldAction> WorldActions;
}
