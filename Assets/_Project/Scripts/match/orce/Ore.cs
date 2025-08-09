using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ore : NetworkBehaviour
{
    [SyncVar] public float HP;
    [HideInInspector] public float MaxHP;
    public OreType OreType;
    public int GiveAmount;
    public List<Renderer> Crystals;
    public Vector3 Size=new Vector3(1,.5f,1);
    public Vector3 Center = new Vector3(0, .1f, 1);
    [HideInInspector] public OresSpawner spawn;
    [HideInInspector] public Material Material;
    [HideInInspector] public List<GameObject> DropCrystalls;
    private void Update()
    {
        if (!isLocalPlayer) return;
    }

    public bool isBreak;
    public void OnDrawGizmos()
    {
        if (isBreak)
        {
            isBreak = false;
            DropCrystall();
        }
        if (spawn!=null&&spawn.isView)
        {
            Vector3 center = transform.position + Center;
            Vector3
                p1 = new Vector3(center.x + Size.x / 2, center.y - Size.y / 2, center.z + Size.z / 2),
                p2 = new Vector3(center.x + Size.x / 2, center.y - Size.y / 2, center.z - Size.z / 2),
                p3 = new Vector3(center.x - Size.x / 2, center.y - Size.y / 2, center.z - Size.z / 2),
                p4 = new Vector3(center.x - Size.x / 2, center.y - Size.y / 2, center.z + Size.z / 2),
                p5 = new Vector3(center.x + Size.x / 2, center.y + Size.y / 2, center.z + Size.z / 2),
                p6 = new Vector3(center.x + Size.x / 2, center.y + Size.y / 2, center.z - Size.z / 2),
                p7 = new Vector3(center.x - Size.x / 2, center.y + Size.y / 2, center.z - Size.z / 2),
                p8 = new Vector3(center.x - Size.x / 2, center.y + Size.y / 2, center.z + Size.z / 2);
            Debug.DrawLine(p1, p2, Color.green);
            Debug.DrawLine(p2, p3, Color.green);
            Debug.DrawLine(p3, p4, Color.green);
            Debug.DrawLine(p4, p1, Color.green);

            Debug.DrawLine(p5, p6, Color.green);
            Debug.DrawLine(p6, p7, Color.green);
            Debug.DrawLine(p7, p8, Color.green);
            Debug.DrawLine(p8, p5, Color.green);

            Debug.DrawLine(p1, p5, Color.green);
            Debug.DrawLine(p2, p6, Color.green);
            Debug.DrawLine(p3, p7, Color.green);
            Debug.DrawLine(p4, p8, Color.green);
        }
    }

    [Command(requiresAuthority = false)]
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            DropCrystall();
            HP = MaxHP;
          //  return GiveAmount;
            // Выдаем игроку OreType в количестве GiveAmount
        }
      //  return 0;
    }

    [ClientRpc]
    public void DropCrystall()
    {
        for(int i = 0; i < GiveAmount; i++)
        {
            Vector3 center = transform.position + Center;
            GameObject cryst = Instantiate(DropCrystalls[Random.Range(1, DropCrystalls.Count-1)]);
            cryst.GetComponent<Renderer>().material = Material;
            cryst.transform.position = new Vector3(
                Random.Range(center.x - Size.x / 2, center.x + Size.x / 2),
                Random.Range(center.y - Size.y / 2, center.y + Size.y / 2),
                Random.Range(center.z - Size.z / 2, center.z + Size.z / 2));
            cryst.GetComponent<BreakOre>().Init(OreType);
        }       
    }
}

public enum OreType
{
    None,
    Ruby,  
    Saphire,
    Topaz
}