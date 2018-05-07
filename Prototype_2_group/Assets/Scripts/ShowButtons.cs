using UnityEngine;
using System.Collections;

public class ShowButtons : MonoBehaviour
{
    [SerializeField] private GameObject button;

    private void Update()
    {
        if(button == null)
        {
            Debug.Log("No Button");
            button = GameObject.FindGameObjectWithTag("LightButton");
            StartCoroutine(DisableButton());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(true);
        }
    }

    IEnumerator DisableButton()
    {
        yield return new WaitForSeconds(0.5f);
        button.SetActive(false);
    }
}
