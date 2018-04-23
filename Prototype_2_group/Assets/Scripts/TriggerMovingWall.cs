using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingWall : MonoBehaviour {

    public PathFinding[] mvs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (PathFinding mv in mvs)
            {
                mv.enabled = true;
            }
        }
    }
}
