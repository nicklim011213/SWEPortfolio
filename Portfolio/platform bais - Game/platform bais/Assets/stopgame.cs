using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopgame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
         Application.Quit(); // Quits the game
	}
 }
	}
