using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OresSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public OreData OreData;
    public bool isSpawn,isView;
    private void OnDrawGizmos()
    {
        if (isSpawn)
        {    
            isSpawn = false;
            Spawn();
        }
    }
    public void Spawn()
    {
        GameObject oreG = Instantiate(Prefab);
        oreG.transform.SetParent(transform);
        oreG.name = OreData.Type.ToString();
        oreG.transform.localPosition = new Vector3(0, 0, 0);
        Ore ore = oreG.GetComponent<Ore>();
        ore.MaxHP = ore.HP = OreData.MaxHP;
        ore.OreType = OreData.Type;
        ore.GiveAmount = OreData.Amount;
        ore.spawn = this;
        ore.Material = OreData.Material;
        ore.DropCrystalls = OreData.DropCrystalls;
        foreach(Renderer cryst in ore.Crystals)
        {
            cryst.material = OreData.Material;
        }

    }
}
