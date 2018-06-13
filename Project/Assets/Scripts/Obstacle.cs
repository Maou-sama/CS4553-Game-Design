using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle: MonoBehaviour {

    [Header("Properties")]
    [SerializeField] private int damage;
    [SerializeField] private float screenShakeDuration;
    [SerializeField] private float screenShakeMagnitude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerNew>().loseHP(damage);
            Camera.main.GetComponent<ShakeScreen>().Screenshake(screenShakeDuration, screenShakeMagnitude);
        }
    }
}