using UnityEngine;
using System.Collections;

public enum Direction { Left, Right }

public class CrushingWall : MonoBehaviour
{
    [Header("Crushing Wall Properties")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Direction direction;

    private Vector2 startingPosition;
    private bool move = false;
    private bool trigger = false;

    private void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (direction == Direction.Left)
            {
                transform.Translate(-transform.right * speed);
            }

            else if (direction == Direction.Right)
            {
                transform.Translate(transform.right * speed);
            }
        }
    }

    void ResetWall()
    {
        trigger = false;
        move = false;
        transform.position = startingPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerNew>().loseHP(damage);
        }

        //Stop moving if collided with room's wall
        if(collision.gameObject.tag == "StaticWall")
        {
            move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Make sure the it only trigger once
            if (!trigger)
            {
                trigger = true;
                move = true;
            }
        }
    }
}