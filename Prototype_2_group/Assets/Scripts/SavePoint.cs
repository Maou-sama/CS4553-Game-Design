using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    public bool check;

    private void Start()
    {
        check = false;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && check == false)
        {
            if(gameObject.tag == "TurnInvisiblePoint")
            {
                GameManager.gm.invisible = true;
                Debug.Log("I'm Invisible Point");
            }

            PlayerNew player = collision.gameObject.GetComponent<PlayerNew>();
            player.setSavePointPos(transform.position);
            player.hp = player.MaxHP;
            check = true;
        }
    }
}
