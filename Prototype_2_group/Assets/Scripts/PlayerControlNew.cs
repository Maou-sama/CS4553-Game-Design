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

    private GameObject movingWall;

    [Header("Player Properties")]
    [SerializeField] private int wallDamage;
    [SerializeField] private float speed;
    [SerializeField] private float respawnTime;

    [SerializeField] private SprintBar sprint;
    public float sprintBarLength=1;

    [Header("Player's Object")]
    [SerializeField] private FlashLight fl;
    [SerializeField] private GameObject mark;
    [SerializeField] private GameObject bloodParticleSystem;
    private Vector3 direction;

    private void Start()
    {
        movingWall = GameObject.FindGameObjectWithTag("CrushingWall");
        player = GetComponent<PlayerNew>();
        rigi = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<Collider2D>();
        
    }

    bool isrun;

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
            if (Input.GetKey(KeyCode.W))
            {
                direction += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += new Vector3(-1, 0, 0);
            }
            if (direction != Vector3.zero)
                direction = direction.normalized;

            rigi.velocity = direction * speed* sprint.PlayerSpeedScale();
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
        }
    

    private void Die()
    {
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
