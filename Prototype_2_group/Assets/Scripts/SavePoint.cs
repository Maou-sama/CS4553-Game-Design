using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    //a check to see if player already activate this point
    private bool check;

    private void Start()
    {
        check = false;
    }

    //Set player hp back to the max, record the position of the save point only if this save point isn't activated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            PlayerNew player = collision.gameObject.GetComponent<PlayerNew>();
            player.setSavePointPos(transform.position);
            if (!check)
            {
                player.hp = player.MaxHP;
                check = true;
            }
            player.SetBattery(10);
        }
    }
}