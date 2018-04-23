using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour
{
    //Whether the wall move down or up
    [SerializeField] private bool moveDown;
    [SerializeField] private bool startMoving;

    private Animator anim;

    // Use this for initialization
    private void Start()
    {
        //Change the animator according to move direction of the wall
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("moveDown", moveDown);
        anim.SetBool("startMoving", startMoving);
    }

    public void Move()
    {
        startMoving = true;
    }
}