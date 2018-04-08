using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour {

    public Transform eye;
    public GameObject bomp;
    public int bomp_num;
    public int another_eye;
    public float speed;
    public GameObject eyeMask;
    public Vector3 save_point;
    public Text text;
    public bool is_eyemaskopen;
    Sequence seq;
    Rigidbody2D rigi;
    TileData tiledata;

    private Vector3 forward;
    public Camera cam;

     Hashtable ht = new Hashtable();

    public bool Is_eyemaskopen
    {
        get
        {
            return is_eyemaskopen;
        }

        set
        {
            is_eyemaskopen = value;
        }
    }

    // Use this for initialization
    void Start () {

        rigi = gameObject.GetComponent<Rigidbody2D>();
        eyeMask.SetActive(false);
        save_point = transform.position;
        seq = DOTween.Sequence();
        seq.Append(eyeMask.transform.DOScale(3f, 0.1f)).
            Append(eyeMask.transform.DOScale(8f, 3f)).Pause().SetAutoKill(false);
    }


    // Update is called once per frame
    void Update() {

        //rigi.MovePosition(Vector2.MoveTowards(transform.position, pos.position, Time.deltaTime));
        Time.timeScale = 1;
        rigi.velocity = Vector2.zero;

        if (another_eye < 0)
        {
            transform.position = save_point;
            another_eye = 10;
        }

            

        if (Input.GetKey(KeyCode.W))
        {
            forward = transform.up;
            rigi.velocity = transform.up * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -transform.up;
            rigi.velocity = transform.up * -speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            forward = transform.right;
            rigi.velocity = transform.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            forward = -transform.right;
            rigi.velocity = transform.right * -speed;
        }

        if(Input.GetKeyDown(KeyCode.E))
        { 
                GameObject b;
                b = GameObject.Instantiate(bomp);
                b.transform.position = transform.position + forward*0.1f;
        }
        if(Input.GetKeyDown(KeyCode.Q)&&another_eye>0)
        {
            another_eye--;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if(another_eye > 0)
            { 
                //Time.timeScale = 0.5f;
                rigi.velocity = Vector2.zero;
                //eyeMask.SetActive(true);
                OpenMask();
                
            }
        }
        else
        {
            CloseMask();
        }
       
    }

    private void OpenMask()
    {
        seq.Play();
        eyeMask.SetActive(true);        
        Is_eyemaskopen = true;
    }

    private void CloseMask()
    {
        seq.Restart();
        seq.Pause();
        eyeMask.SetActive(false);
        Is_eyemaskopen = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "StaticWall")
            cam.DOShakePosition(0.5f, 0.5f, 10).Play();
            
    }

    private void OnGUI()
    {
        text.text = "Score: " + another_eye.ToString();
    }
}
