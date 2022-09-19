using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class endlevel : MonoBehaviour {



    void OnTriggerEnter(Collider other) {
	if (other.gameObject.tag == "Player")
		{ 
		SceneManager.LoadScene("win"); 
		}
	else
		{

		}
    }

	// Update is called once per frame
	void Update () {
		
	}
}
