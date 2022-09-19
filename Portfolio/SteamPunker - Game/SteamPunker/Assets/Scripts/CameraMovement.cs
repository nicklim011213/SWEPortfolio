using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find(("Dot")); //Find object called dot make it camera
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))        // Move Camera Up down left or right based on wasd layout
        {
            Camera.transform.position += Vector3.up * 10.0f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Camera.transform.position += Vector3.up * -10.0f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Camera.transform.position += Vector3.left * 10.0f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Camera.transform.position += Vector3.left * -10.0f * Time.deltaTime;
        }
    }
}

