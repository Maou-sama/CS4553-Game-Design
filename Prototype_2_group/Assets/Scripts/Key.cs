using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Key : MonoBehaviour {

    private PlayerNew player;

    [Header("Key Properties")]
    [SerializeField] private Door door;
    [SerializeField] private int keyID;

    private void Start()
    {
        //Assign the door ID to key ID and match the color
        keyID = door.DoorID;
        GetComponent<SpriteRenderer>().color = door.GetComponent<SpriteRenderer>().color;

        //Find the player in the scene
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.AcquireKey(keyID);
            Destroy(gameObject);
        }
    }
}