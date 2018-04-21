using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
/*
    The ghost would chase the player within the chaseRange. If the player step into the 
  chase range, the ghost walk toward the player but do no hurt. 
    Only when player open their camera, should the ghost do hurt

    About what kind of hurt, it need to be decided. Maybe lose both HP and Battery?

    If you have any better idea, just share and modify
 */
public class GhostNew : MonoBehaviour {

    [Header("Ghost Properties")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float chaseRange; //The range that the ghost would chase and see the player
    [SerializeField] private float jumpOutScale; //How big should the ghost change when jump out

    [Header("Effect")]
    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private GameObject footStep;
    [SerializeField] private float footStepGenerateInterval;
    [SerializeField] private AudioSource screamAudioControl;
    
    private PlayerNew player;
    private AudioSource audioControl;
    private Rigidbody2D rigi;
    private SpriteRenderer spr;
    //private Sequence seq;

    private Vector2 originalPosition;
    private float distanceToPlayer;
    private Vector3 oldScale;
    void Start()
    {
        ////Init here////
        audioControl = GetComponent<AudioSource>();
        rigi = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        originalPosition = transform.position;
        oldScale = transform.localScale;
        ////Edit special effect/ animation here////
        //Ani: ghost scaling in a very fast speed then gradually fading(jumpout effect)
        //Can change

        /*
        seq = DOTween.Sequence();
        seq.Append(spr.DOFade(1, 0)).Append(transform.DOScale(jumpOutScale, 0.01f)).
            Insert(0.5f, spr.DOFade(0f, 2f));
        seq.OnComplete(() =>{
            spr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            transform.position = chaseRangeCenter;
            spr.color = Color.white;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        });
        seq.OnPlay(() => { spr.maskInteraction = SpriteMaskInteraction.None; });
        seq.SetAutoKill(false);
        */
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerNew>();
        }

        //Find the distance from the original position to the player 
        //and only chase player when player is in that range
        distanceToPlayer = Vector2.Distance(player.transform.position, originalPosition);

        if (distanceToPlayer < chaseRange)
        {         
            ChasePlayer();
        }
        else
        {       
            GoBack();
        }
    }

    void ChasePlayer()
    {
        //Get the distance and direction from ghost to player
        float distance = Vector2.Distance(player.transform.position, transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        //Leave footstep and move to player if distance is > 0.2
        if (distance >= 0.2f)
        {
            if (!audioControl.isPlaying)
                audioControl.PlayOneShot(footStepClip);
            rigi.velocity = speed * direction;
            Invoke("LeaveFootStep", footStepGenerateInterval);
        }
        else
        {
            rigi.velocity = Vector2.zero;
            CancelInvoke();
        }
  

    }

    void GoBack()
    {
        //Get the distance and direction to the original position
        float distance = Vector2.Distance(originalPosition, transform.position);
        Vector2 direction = (originalPosition - (Vector2)transform.position).normalized;

        //Move back until the original position and leave foot step
        if (distance >= 0.1f)
        {
            if (!audioControl.isPlaying)
                audioControl.PlayOneShot(footStepClip);
            rigi.velocity = speed * direction;
            Invoke("LeaveFootStep", footStepGenerateInterval);
        }
        else
        {
            transform.position = originalPosition;
            rigi.velocity = Vector2.zero;
            CancelInvoke();
        }
    }

    void LeaveFootStep()
    {
        Instantiate(footStep, transform.position, Quaternion.identity);
        //Invoke("LeaveFootStep", footStepGenerateInterval);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage player when hit, turn on the scream sound, turn big and opague for 1s
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            screamAudioControl.Play();
            player.loseHP(damage);
            transform.localScale *= jumpOutScale;
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1.0f);
            transform.DOScale(oldScale, 1.0f).Play();
            spr.DOFade(0f, 1.0f).Play();
            //seq.Restart();
        }
    }

    //Draw on scene window the effective range of ghost chasing and has no effect on game window
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(originalPosition, chaseRange);
    }
}
