using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
    public int stock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.name == "Player")
        {
            Player player = obj.GetComponent<Player>();
            player.another_eye += stock;
            GameObject.Destroy(gameObject);
        }
    }
}
