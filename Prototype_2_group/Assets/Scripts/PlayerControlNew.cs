using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/*
    This scripts is use for palyer control and player interation.
     */

[RequireComponent(typeof(PlayerNew))]
//[RequireComponent(typeof(CameraNew))]
public class PlayerControlNew : MonoBehaviour {

    PlayerNew player;
    public float speed;
    //public CameraNew cam;
    public FlashLight fl;
    private Vector3 direction;


    private Rigidbody2D rigi;
    private void Start()
    {
        player = GetComponent<PlayerNew>();
        //player.setSavePointPos(transform.position);
        rigi = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (player.hp <= 0)
        {
            player.hp = player.MaxHP;
            transform.position = player.SavePointPos;
        }

        rigi.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction = transform.up;
            rigi.velocity = direction * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = -transform.up;
            rigi.velocity = direction * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = transform.right;
            rigi.velocity = direction * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -transform.right;
            rigi.velocity = direction * speed;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            OpenCamera();
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            CloseCamera();
        }
    }

    void OpenCamera()
    {
        if (player.Battery <= 0)
            return;
        fl.On();
    }
    void CloseCamera()
    {
        if (player.Battery > 0)
            player.loseBatery(1);
        fl.Off();
    }
}
