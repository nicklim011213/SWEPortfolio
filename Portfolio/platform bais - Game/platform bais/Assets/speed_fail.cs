using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class speed_fail : MonoBehaviour {

     public float speed;
	 
	// Use this for initialization
	void Start () {
	}

	
	
	// Update is called once per frame
	void Update () {
	fail();	
	}
	
	void fail(){
	speed = GetComponent<Rigidbody>().velocity.y;
	if(speed <= -40.0f)
		{
		fail2();
		}
	}
	
	void fail2(){
	SceneManager.LoadScene("failed");
		
		
	}
	}

