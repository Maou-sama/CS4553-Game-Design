
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingWall : MonoBehaviour {

    [SerializeField] private MovingWall[] mvs;
    [SerializeField] private GameObject movingWallSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (MovingWall mv in mvs)
            {                         
                mv.Move();
            }
            movingWallSound.SetActive(true);
        }
    }
}
