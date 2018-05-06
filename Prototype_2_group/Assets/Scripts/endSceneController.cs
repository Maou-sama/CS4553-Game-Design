using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gameStart(){
		SceneManager.LoadScene ("Main 1");
	}

	public void exitGame(){
		Application.Quit();
	}
}
