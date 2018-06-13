using UnityEngine;
using System.Collections;

public class RemoveCrushingWall : MonoBehaviour
{
    [SerializeField] private GameObject crushingWall;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(crushingWall);
        }
    }
}
