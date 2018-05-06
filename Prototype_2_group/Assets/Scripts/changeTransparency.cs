using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTransparency : MonoBehaviour {
	private SpriteRenderer sr;
	private float time; //timer variable for tweening colors
	public float startTime;
	public Gradient colorGradient;
	private bool rise;
	public float changeTime = 100f;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		time = startTime;
		rise = false;
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (float)0.5);
	}


	// Update is called once per frame
	void FixedUpdate () {
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (float)(time / changeTime));
		//sr.color = colorGradient.Evaluate (time / changeTime); //as time increases, we move along the gradient
		if (rise) {
			time += Time.deltaTime;
			if (time >= changeTime) {
				rise = false;
			}
		} else {
			time -= Time.deltaTime;
			if (time <= 0) {
				rise = true;
			}
		}
		//Debug.Log(Time.deltaTime);
	}
}
