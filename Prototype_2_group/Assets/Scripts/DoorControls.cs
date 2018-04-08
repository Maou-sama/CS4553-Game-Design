using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour {
    public int door_id;
    public Transform open_pos;
    public Transform close_pos;
    public bool is_open;

    private void Update()
    {
        if (is_open)
            transform.position = open_pos.position;
        else
            transform.position = close_pos.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        Player player = obj.GetComponent<Player>();

        if (obj.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                is_open = !is_open;
            }
        }
    }
}
