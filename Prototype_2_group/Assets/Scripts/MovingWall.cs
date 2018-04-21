using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour
{
    //Whether the wall move down or up
    [SerializeField] private bool moveDown;

    // Use this for initialization
    void Start()
    {
        //Change the animator according to move direction of the wall
        GetComponent<Animator>().SetBool("moveDown", moveDown);
    }
}