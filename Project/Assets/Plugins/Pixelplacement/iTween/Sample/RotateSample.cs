using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform target;
    private Vector3 zAxis = new Vector3(0, 0, 1);
    void Update()
    {
        transform.RotateAround(target.position, zAxis, speed);
    }
}