using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ghost : MonoBehaviour {
    public AudioClip ghostRangeClip, ghostFollowClip;
    public GameObject scream;
    public float jumpOutScale;
    public Transform target;
    public float speed = 0.5f;
    public float followRange;
    public GameObject footStep;
    public int attack=3;
    public int life = 1;
    public int acceptDamage = 1;

    Player player;
    private Vector3 initPosition;
    Rigidbody2D rigi;
    AudioSource    aud, scrAud;
    SpriteRenderer spr;
    bool isJumpOut;
    Sequence seq;

    private Vector3 cur_target;
    public bool isOpen;
    // Use this for initialization
    void Start () {
        aud = GetComponent<AudioSource>();
        scrAud = scream.GetComponent<AudioSource>();
        spr = GetComponent<SpriteRenderer>();
        rigi = GetComponent<Rigidbody2D>();
        player = target.GetComponent<Player>();
        isJumpOut = false;
        cur_target = transform.position;
        initPosition = transform.position;

        seq = DOTween.Sequence();
        seq.Append(spr.DOFade(1,0)).Append(transform.DOScale(jumpOutScale, 0.01f)).
            Insert(0.5f, spr.DOFade(0f, 2f));
        seq.OnStart(() => { isOpen = false; aud.volume = 1.0f; }).OnComplete(() =>
         {           
             life -= acceptDamage;
            // transform.position = initPosition;
             transform.localScale = new Vector3(0.3f, 0.3f, 0);
             isOpen = false;
             
             seq.Pause();
         }).SetAutoKill(false);
        aud.clip = ghostRangeClip;
        aud.Play();

        isOpen = false;
	}

    float t = 0;
	// Update is called once per frame
	void Update () {

        cur_target = target.position;
        float dis = Vector3.Distance(initPosition, cur_target);

        if (life <= 0)
            Destroy(gameObject);

        if (!isOpen)
        {
            if (dis <= followRange)
            {
                aud.clip = ghostFollowClip;
                if (!aud.isPlaying)
                    aud.Play();
                rigi.DOKill();
                rigi.DOMove(cur_target, 1f / speed).Play().OnPlay(()=> {
                    t += Time.deltaTime;
                    if(t>0.1/speed)
                    {
                        t = 0;
                        Instantiate(footStep).transform.position = transform.position;                       
                    }

                });
                if (dis <= 2f)
                    aud.enabled = false;
                else
                    aud.enabled = true;
            }
            else
            {
                rigi.DOKill();
                rigi.DOMove(initPosition, 1f / speed).Play();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "CloseEye")
        {
            Debug.Log(collision.name);
            isOpen = true;
            player.another_eye -= attack;

            
            scrAud.Play();
            seq.Restart();
            spr.maskInteraction = SpriteMaskInteraction.None;              
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);
        Gizmos.DrawWireSphere(initPosition, followRange);
    }
}
