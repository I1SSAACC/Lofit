using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "New Ore")]
public class OreData : ScriptableObject
{
    public Material Material;
    public int MaxHP,Amount;
    public OreType Type;
    public List<GameObject> DropCrystalls;
}
