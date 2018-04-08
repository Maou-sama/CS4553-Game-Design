using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    Rigidbody2D rigi;
    public Transform[] wayPoints;
    public int cur_pos;
    public float speed;
    public int att = 8;
    GameObject cam; 
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        if(wayPoints.Length > 0)
            rigi.MovePosition((Vector2)wayPoints[cur_pos].position);
    }
    // Update is called once per frame
    void Update () {

        if(wayPoints.Length > 0)
        {
            Vector2 des = Vector2.MoveTowards(transform.position,wayPoints[cur_pos].position, Time.deltaTime*speed);
            rigi.MovePosition(des);
            if (Vector2.Distance(transform.position, wayPoints[cur_pos].position) <= 0.1)
            {
                cur_pos = (cur_pos + 1) % wayPoints.Length;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.collider.gameObject;
        if(obj.name == "Player")
        {
            obj.GetComponent<Player>().another_eye -= att;           
            //obj.transform.position = obj.GetComponent<Player>().save_point;
        }
    }
}
