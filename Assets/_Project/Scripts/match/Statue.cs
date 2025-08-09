using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using static PlayerHero;

public class Statue : NetworkBehaviour
{
    [SerializeField] private Statue Staty;
    public List<Transform> RespawnPoints;
    public List<NetworkConnectionToClient> listTeam = new List<NetworkConnectionToClient>();
    public Dictionary<TeamVariaty, GameObject> objectDictionary;
  [SyncVar]  public bool isAlive = true;
    [SyncVar] public float HP, MaxHP = 1002, Armor, maxArmor;
    public Transform PlayerList;
    public TeamVariaty Team;
    public int TeamNuber;


    public GameObject AliveStatue, DeadStatue;

  

    [ServerCallback]
    public void ServeRPlayerDistribution()
    {
        Debug.Log(listTeam.Count);
        foreach (var item in listTeam)
        {
           // item.identity.gameObject.GetComponent<PlayerHero>().StatueTeam = Staty;
          //  item.identity.gameObject.GetComponent<PlayerHero>().SpanPosition();
            Debug.Log(item);
        }

       
    }

   
    public Transform GetRespawnPoint()
    {
        return RespawnPoints[Random.Range(0, RespawnPoints.Count)];
    }

    [Command(requiresAuthority = false)]
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy();
            isAlive = false;
        }
    }
    public void HealUpgrade(float value)
    {
        MaxHP+=value;
        HP = MaxHP;
    }
    public void HealConsumables(float value)
    {
        HP = Mathf.Min(HP + value, MaxHP);
    }
    [ClientRpc]
    public void Destroy()
    {
        AliveStatue.SetActive(false);
        DeadStatue.SetActive(true);
    }
}
