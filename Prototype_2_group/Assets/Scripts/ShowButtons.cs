using UnityEngine;
using System.Collections;

public class ShowButtons : MonoBehaviour
{
    [SerializeField] private GameObject button;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(true);
        }
    }
}
