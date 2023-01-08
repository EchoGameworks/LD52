using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Growable;

[CreateAssetMenu(fileName = "New World Action", menuName = "World Action")]
public class WorldAction : ScriptableObject
{
    public enum ActionTypes { Rest, Spawn, Modify, Reap }
    public ActionTypes ActionType;
    public string Description;
    public GameObject spawnObject;
    public Impact Impact;
    public int Value;
}
