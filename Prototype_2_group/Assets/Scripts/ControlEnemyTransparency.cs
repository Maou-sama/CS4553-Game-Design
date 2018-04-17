using UnityEngine;
using System.Collections;

public class ControlEnemyTransparency : MonoBehaviour
{
    private SpriteRenderer sr;

    private float time; //timer variable for tweening colors

    [Range(0, 4)]
    public float changeTime = 1.0f;

    public Gradient colorGradient;

    public bool startChanging;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        time = changeTime;
        startChanging = true;
    }

    private void Update()
    {
        if (startChanging)
        {
            if (time < changeTime)
            {
                sr.color = colorGradient.Evaluate(time / changeTime); //as time increases, we move along the gradient
                time += Time.deltaTime;
            }
            else
            {
                sr.color = colorGradient.Evaluate(1); //get the color from the far right of the gradient
                startChanging = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlashLight")
        {
            Debug.Log("Enter");
            sr.color = colorGradient.Evaluate(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FlashLight")
        {
            Debug.Log("Exit");
            startChanging = true;
            time = 0;
        }
    }
}