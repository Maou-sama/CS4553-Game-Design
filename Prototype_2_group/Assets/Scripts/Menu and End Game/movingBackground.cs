using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBackground : MonoBehaviour {
	//private GameObject background;
	//private Transform transform;
	public float speed;
	// Use this for initialization
	void Start () {
		//background = gameObject.GetComponent<GameObject> ();
		//transform = background.transform;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
		if (gameObject.transform.position.x < -21.12f)
		{
			gameObject.transform.position += new Vector3(21.12f * 2, 0, 0);
		}
	}
}
