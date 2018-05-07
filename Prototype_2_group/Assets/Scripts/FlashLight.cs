using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(SpriteMask))]
public class FlashLight : MonoBehaviour
{
    private CircleCollider2D cc2d;
	private bool flashLightOn;
    
    [Header("Light's Properties")]
    [SerializeField] private Light flashlight;
    [SerializeField] private float intensity;
    [SerializeField] private SpriteMask lightSpriteMask;

    // Use this for initialization
    void Start()
    {
		flashLightOn = false;
        cc2d = GetComponent<CircleCollider2D>();
    }

    public void On()
    {
        cc2d.enabled = true;
        lightSpriteMask.enabled = true;
		flashLightOn = true;
        flashlight.intensity = intensity;
    }

    public void Off()
    {
        cc2d.enabled = false;
        lightSpriteMask.enabled = false;
        flashLightOn = false;
        flashlight.intensity = 0;
    }

	public bool getFlashLight(){
		return flashLightOn;
	}
}