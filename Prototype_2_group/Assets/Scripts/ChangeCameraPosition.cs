using UnityEngine;
using System.Collections;

public class ChangeCameraPosition : MonoBehaviour
{
    [SerializeField] private Vector3 cameraPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<ShakeScreen>().cameraCenterPos = cameraPosition;
        }
    }
}