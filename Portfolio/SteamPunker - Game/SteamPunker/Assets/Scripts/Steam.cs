using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public int steamheld = 0; // number of steam held
    public GameObject Output; // input and output pipes
    public GameObject Input;
    public Steam Inputscript; // the scripts which handle input
    public SteamGenerator Inputscript2;
    // Start is called before the first frame update
    void Start()
    {
        Input = null; // Input is nothing to start
    }

    // Update is called once per frame
    void Update()
    {
    if (Input != null && Inputscript != null)
        {
                if (Inputscript.steamheld > 1) // If the input has more than 0 steam drain it quicker 
                {
                    Inputscript.steamheld--;
                    Inputscript.steamheld--;
                    steamheld++;
                    steamheld++;
                }
                else // Otherwise drain at 1 per second
                {
                    Inputscript.steamheld--;
                    steamheld++;
                }
                
            }
    else if ((Input != null && Inputscript2 != null))
        {
            if (Inputscript2.steamheld > 1) // If the input has more than 1 steam drain it quicker 
            {
                Inputscript2.steamheld--;
                Inputscript2.steamheld--;
                steamheld++;
                steamheld++;
            }
            else                        // otherwise base speed
            {
                Inputscript2.steamheld--;
                steamheld++;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Pipe_hidden(Clone)" && collision.gameObject.GetComponent<Steam>().steamheld > this.steamheld)) // If a pipe is found and is colliding
        {
            Debug.Log("Found pipe with more than 0 steam Assuming Input"); // if pipe has more than 0 safe to assume this is an input pipe
            Input = collision.gameObject;                                   // make the pipe input
            Inputscript = Input.GetComponent<Steam>();                     // input script is the objects script
        }
        else if ((collision.gameObject.name == "SteamGen_hidden(Clone)"))
        {
            Input = collision.gameObject;                               // If generator of steam
            Inputscript2 = Input.GetComponent<SteamGenerator>();        // set input 2 = to that script the steam gen has
        }
        if ((collision.gameObject.name == "Pipe_hidden(Clone)" && collision.gameObject.GetComponent<Steam>().steamheld == 0) || (collision.gameObject.name == "Miner_hidden(Clone)"))
        {
           // If pipe has 0 steam safe to assume output
           Debug.Log("Found pipe with 0 steam Assuming output");
           Output = collision.gameObject;
        }

    }
}
