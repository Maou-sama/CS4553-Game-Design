using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerNew))]
public class PlayerControlNew : MonoBehaviour {

    private PlayerNew player;
    private Rigidbody2D rigi;

    [Header("Player Properties")]
    [SerializeField] private int wallDamage;
    [SerializeField] private float speed;

    [Header("Player's Object")]
    [SerializeField] private FlashLight fl;
    [SerializeField] private GameObject mark;

    private Vector3 direction;
    
    private void Start()
    {
        player = GetComponent<PlayerNew>();
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

        //Movement Control
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

        //Turn on off the flash light
        if (Input.GetKey(KeyCode.J))
        {
            TurnOnFlashLight();
        }
        else if(Input.GetKeyUp(KeyCode.J))
        {
            TurnOffFlashLight();
        }

        //Leave blood mark
        if (Input.GetKeyDown(KeyCode.M))
        {
            Instantiate(mark, transform.position, Quaternion.identity);
        }
    }

    void TurnOnFlashLight()
    {
        if (player.Battery <= 0)
            return;
        fl.On();
    }
    void TurnOffFlashLight()
    {
        if (player.Battery > 0)
            player.loseBatery(1);
        fl.Off();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingWall")
        {
            Debug.Log("Touching Moving Wall");
            player.loseHP(wallDamage);
            Camera.main.GetComponent<ShakeScreen>().Screenshake(0.8f, 1.6f);
        }
    }
}
