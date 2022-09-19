using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Draw : MonoBehaviour {
	public GameObject Effect;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("RigidBodyFPSController").GetComponent<Rigidbody>().velocity.y <= -30)
		{
			Effect.SetActive(true);
			Debug.Log("Do Image");
		}
		else
		{
			Effect.SetActive(false);
			Debug.Log("Dont do Image");
		}
		
	}

}
