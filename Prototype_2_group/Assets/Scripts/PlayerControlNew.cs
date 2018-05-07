    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerNew))]
public class PlayerControlNew : MonoBehaviour {

    private PlayerNew player;
    private Rigidbody2D rigi;
    private SpriteRenderer sr;
    private Collider2D c2d;
    private Animator anim;

    private GameObject movingWall;

    [Header("Player Properties")]
    [SerializeField] private int wallDamage;
    [SerializeField] private float speed;
    [SerializeField] private float respawnTime;
    [SerializeField] private int reduceHpPerDeath;

    [Header("Player Sprint Properties")]
    [SerializeField] private float speedUpScale;
    [SerializeField] private float speedUpTime;
    [SerializeField] private float recoverTime;
    [SerializeField] private float coolingTime;

    //[SerializeField] private SprintBar sprint;
    //public float sprintBarLength=1;

    [Header("Player's Object")]
    [SerializeField] private FlashLight fl;
    [SerializeField] private GameObject mark;
    [SerializeField] private GameObject bloodParticleSystem;

    private bool isCooling = false;
    private float playerSpeedScale = 1;

    private Vector3 direction;

    private void Start()
    {
        movingWall = GameObject.FindGameObjectWithTag("CrushingWall");
        player = GetComponent<PlayerNew>();
        rigi = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        player.stamina = player.MaxStamina;
    }

    private void Update()
    {

        if (player.hp <= 0)
        {
            Die();
        }
        rigi.velocity = Vector2.zero;
        direction = Vector2.zero;


        //Movement Control
        if (!fl.getFlashLight())
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            anim.SetFloat("x", Input.GetAxis("Horizontal"));
            anim.SetFloat("y", Input.GetAxis("Vertical"));

            rigi.velocity = direction * speed * playerSpeedScale;
        }

        //Turn on off the flash light
        if (Input.GetKey(KeyCode.J))
            {
                TurnOnFlashLight();
            }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            TurnOffFlashLight();
        }

        //Leave blood mark
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (player.hp > 1)
            {
                Instantiate(mark, transform.position, Quaternion.identity);
                player.loseHP(1);
            }
        }

        if (!isCooling)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (player.stamina > 0)
                {
                    playerSpeedScale = speedUpScale;
                    consumeStamina();
                }
                else
                {
                    playerSpeedScale = 1;
                    isCooling = true;
                    StartCoroutine(CoolingTime());
                }
            }
            else
            {
                if (player.stamina < 1)
                {
                    playerSpeedScale = 1;
                    recoverStamina();
                }
                else
                    playerSpeedScale = 1;
            }
        }
        else
        {
            playerSpeedScale = 1;
        }
    }

    

    private void Die()
    {
        player.reduceMaxHP(reduceHpPerDeath);
        player.hp = player.MaxHP;
        Instantiate(bloodParticleSystem, transform.position, Quaternion.Euler(180, 0, 0));
        sr.enabled = false;
        c2d.enabled = false;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        movingWall.GetComponent<CrushingWall>().ResetWall();
        GameManager.gm.ResetDoor();
        GameManager.gm.ResetKey();
        sr.enabled = true;
        c2d.enabled = true;
        Camera.main.GetComponent<ShakeScreen>().cameraCenterPos = new Vector3(player.SavePointPos.x, player.SavePointPos.y, -10);
        transform.position = new Vector3(player.SavePointPos.x, player.SavePointPos.y, -1);
    }

    void recoverStamina()
    {
        if (player.stamina >= 1)
            return;
        player.stamina += Time.deltaTime / recoverTime;
    }

    IEnumerator CoolingTime()
    {
        yield return new WaitForSeconds(coolingTime);
        isCooling = false;
    }

    void consumeStamina()
    {
        if (player.stamina <= 0)
        { 
            return;
        }
        player.stamina -= Time.deltaTime / speedUpTime;
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
