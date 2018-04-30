using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : MonoBehaviour {
    private PlayerNew player;

    [Header("Door Properties")]
    [SerializeField] private int doorID;

    public int DoorID
    {
        get
        {
            return doorID;
        }
    }

    private void Update()
    {
        //Find the player in the scene
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>();
        }
    }
		

	private void OnCollisionEnter2D(Collision2D collision)	//#
    {
        if (collision.gameObject.tag == "Player")
        {
            bool hasKey = collision.gameObject.GetComponent<PlayerNew>().CheckKey(doorID);
            if(hasKey)
            {
                openDoor();
            }
           
        }
    }

    public void openDoor()
    {
        gameObject.SetActive(false);
    }
}