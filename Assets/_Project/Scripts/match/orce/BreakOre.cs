using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOre : MonoBehaviour
{
    public OreType Ore;
    public void Init(OreType ore)
    {
        Ore = ore;
    }
    public void Destroy()
    {
        Destroy(gameObject);    
    }
}
