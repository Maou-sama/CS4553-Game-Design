using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class FlashLight : MonoBehaviour
{
    private CircleCollider2D cc2d;

    [Header("Light's Properties")]
    [SerializeField] private Light flashlight;
    [SerializeField] private float intensity;

    // Use this for initialization
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
    }

    public void On()
    {
        cc2d.enabled = true;
        flashlight.intensity = intensity;
    }

    public void Off()
    {
        cc2d.enabled = false;
        flashlight.intensity = 0;
    }
}