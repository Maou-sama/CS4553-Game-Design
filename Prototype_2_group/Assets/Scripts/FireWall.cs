using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FireWall: MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerNew>().loseHP(1);
            Camera.main.GetComponent<ShakeScreen>().Screenshake(0.8f, 1.6f);
        }
    }

}
