using UnityEngine;
using System.Collections;

public class ShakeScreen : MonoBehaviour {

	public Vector3 cameraCenterPos;

	float shakeDuration = 0;
	float shakeTimer = 0;
	float shakeMagnitude = 0;
	Vector3 shakeDirection = Vector3.zero;

	// an animation curve that we'll use to taper off the screen shake magnitude towards the end of the duration
	public AnimationCurve curve;

	void Start () {
        cameraCenterPos = transform.position;
    }

	void Update () {
        transform.localPosition = cameraCenterPos + AddScreenshake();
	}

	public void Screenshake (float duration, float magnitude, Vector3 weightedDirection) {
		shakeDuration = duration;
		shakeTimer = shakeDuration;
		shakeMagnitude = magnitude;
		shakeDirection = weightedDirection.normalized;
	}	

	public void Screenshake (float duration, float magnitude) {
		Screenshake(duration, magnitude, Vector3.zero);
	}

	// this method returns a vector3 that will be used to offset the camera from its center position
	Vector3 AddScreenshake () {
	
		if (shakeTimer > 0) {
			// this ease number will be used to modify the magnitude of the final vector.
			float ease = curve.Evaluate(shakeTimer / shakeDuration);
			
			// initializing our result vector with a random magnitude between -1 and 1 along the camera's right and up directions
			Vector3 v = (transform.right * Random.Range(-1f, 1f)) + (transform.up * Random.Range(-1f, 1f));

			// here we add to our result vector a weighted direction
			v += shakeDirection;

			// now we multiply the vector by our shake magnitude and then by our ease amount;
			v *= shakeMagnitude * ease;

			// subtract from the shake timer
			shakeTimer -= Time.deltaTime;

			return v;
		}

		// if we got here, shakeTimer is less than 0 so we return vector3.zero for no offset 
		return Vector3.zero;
	}
}
