using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using static PlayerHero;

public class ManagerMatch : NetworkBehaviour
{
    public List<NetworkConnectionToClient> HerosRed = new List<NetworkConnectionToClient>();
    public List<NetworkConnectionToClient> HerosBlue = new List<NetworkConnectionToClient>();
    public List<GameObject> GHerosRed = new List<GameObject>();
    public List<GameObject> GHerosBlue = new List<GameObject>();
    public GameObject CameraMain;
    public Statue StatueRed, StatueBlue;
    private TeamVariaty Team;
    public GameObject Menu;
    public GameObject WinBlue, WinRed;

    [Scene] public string IdScene;


    public void Start()
    {
        IdScene = gameObject.scene.name;
    }

    [Command(requiresAuthority = false)]
    public void ConnectMath(NetworkConnectionToClient sender = null)
    {
        GameObject Hero = sender.identity.connectionToClient.identity.gameObject;
        StartCoroutine(Readyload(Hero));

        //распределение игроков по командам
        if (Hero.GetComponent<PlayerHero>().Team == TeamVariaty.Blue)
        {
            HerosBlue.Add(sender.identity.connectionToClient);
            GHerosBlue.Add(Hero.gameObject);
        }

        if (Hero.GetComponent<PlayerHero>().Team == TeamVariaty.Red)
        {
            HerosRed.Add(sender.identity.connectionToClient);
            GHerosRed.Add(Hero.gameObject);

        }

        Debug.Log(HerosRed.Count);
        Debug.Log(HerosBlue.Count);

        StatueRed.listTeam = HerosRed;
        StatueBlue.listTeam = HerosBlue;

        StatueRed.ServeRPlayerDistribution();
        StatueBlue.ServeRPlayerDistribution();

    }

    public IEnumerator Readyload(GameObject LocalHero)
    {
        LocalHero.GetComponent<Player_Move>().enabled = false;


        yield return new WaitForSeconds(10);


        CameraMain.SetActive(false);
        LocalHero.GetComponent<Player_MouseMove>().Camera.SetActive(true);
        LocalHero.GetComponent<Player_Move>().enabled = true;
        yield break;
    }

   
}
