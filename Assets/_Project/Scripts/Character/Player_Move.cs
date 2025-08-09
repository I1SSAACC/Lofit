using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player_Move : NetworkBehaviour
{
    public PlayerHero Player;
    private float y_Move;
    private float x_Move;
    private float z_Move;
    public float Gviry;
    private float timer = 5;

    //характиристики игрока
    public float Moves;
    public float jump;
    public float Spead;
    public float stamina;
    public float vlumestamina;

  [SerializeField] private CharacterController playerMoveController;
    private Vector3 lastPos;
    Vector3 move_Direction;
    public Animator Animations;

    void Start()
    {
        playerMoveController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        playerMoveController = GetComponent<CharacterController>();

    }

    public void Move()
    {
        x_Move = Input.GetAxis("Horizontal");
        z_Move = Input.GetAxis("Vertical");
        Animations.SetFloat("Z", Input.GetAxis("Horizontal") + z_Move);
        Animations.SetFloat("Y", Input.GetAxis("Vertical") + y_Move);

        if (playerMoveController.isGrounded)
        {
            move_Direction = new Vector3(x_Move, y_Move, z_Move);
            move_Direction = transform.TransformDirection(move_Direction);
        }
        move_Direction.y -= Gviry;
        playerMoveController.Move(move_Direction * Moves * Time.deltaTime);
        /*
        if (transform.position.y < -10)
        {
            Player.Kill();
        }
        */
    }
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 0f&& lastPos != transform.position)
        {
            Animations.SetBool(("Run"), true);
            playerMoveController.Move(move_Direction * Spead * Time.deltaTime);
            stamina -= vlumestamina * Time.deltaTime * 2;
            timer = 5;
        }
        else
        {
            Animations.SetBool(("Run"), false);
            //Move();
        }

     //   Player.UI.Stamina.Value = stamina;
        if (timer <= 0) stamina += vlumestamina * Time.deltaTime;
        timer -= Time.deltaTime;
        if (stamina >= 1) stamina = 1f;

    }


    public void Jupm()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            y_Move = jump;
            Animations.SetBool(("Jump"), true);
        }
        else
        {
            y_Move = 0;
            Animations.SetBool(("Jump"), false);
        }
        lastPos = transform.position;
    }


}