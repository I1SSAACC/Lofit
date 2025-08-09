using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHero : NetworkBehaviour
{
    
    private int selectedWeaponLocal = 0;
    public GameObject ShootPoint;
    public int ActiveWeapon;
//    public List<WeaponCharacteristics> WeaponsList;
//    public Weapon ActiveWeaponGb;
    public Player_Move Move;
    public PlayerStats Stats;
    public PlayerWallet Wallet;
    public PlayerUI UI;
    public Player_MouseMove Mouse;
    public GameObject PLauer;
    public TeamVariaty Team;
    private float Taimer = 3;
    public GameObject loadUI;
    public bool canShop;
    [SyncVar] public float HP,maxHP;
    [SyncVar] private float armor, maxArmor;
    [SyncVar] public bool isAlive; 
    [SyncVar]  public Statue StatueTeam;
    public int id;

    private void Start()
    {
        if (isLocalPlayer)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Mouse.enabled = true;
            ManagerMatch  Manager = FindObjectOfType<ManagerMatch>();
            Manager.ConnectMath();
        }

    }
    private void Update()
    {
        if (isAlive)
        {
            AliveMode();
        }
        else
        {

        }
    }

    private void AliveMode()
    {
        if (!Wallet.isShop)
        {
            /*
            if (WeaponsList.Count > 0)
            {
               Shoot();
            }
            */
           // ChangeWeapon();
            Move.Move();
            Move.Run();
            Move.Jupm(); // ����������: Jupm -> Jump
        }
        Wallet.TryShop();
    }

    public enum TeamVariaty //������� �������
    {
        Blue,
        Red,
        Yellow,
        White,
    }
} 

