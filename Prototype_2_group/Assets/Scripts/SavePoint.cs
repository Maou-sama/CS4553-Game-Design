using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            obj.GetComponent<Player>().save_point = transform.position;

            int c =obj.GetComponent<Player>().another_eye;
            if(c<12)
                obj.GetComponent<Player>().another_eye = 12;
        }
    }
}
