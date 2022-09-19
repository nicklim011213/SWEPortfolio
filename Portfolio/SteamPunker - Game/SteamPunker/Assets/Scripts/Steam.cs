using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public int steamheld = 0;
    public GameObject Output;
    public GameObject Input;
    public Steam Inputscript;
    public SteamGenerator Inputscript2;
    // Start is called before the first frame update
    void Start()
    {
        Input = null;
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
                else
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
            else
            {
                Inputscript2.steamheld--;
                steamheld++;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Pipe_hidden(Clone)" && collision.gameObject.GetComponent<Steam>().steamheld > this.steamheld))
        {
            Debug.Log("Found pipe with more than 0 steam Assuming Input");
            Input = collision.gameObject;
            Inputscript = Input.GetComponent<Steam>();
        }
        else if ((collision.gameObject.name == "SteamGen_hidden(Clone)"))
        {
            Input = collision.gameObject;
            Inputscript2 = Input.GetComponent<SteamGenerator>();
        }
        if ((collision.gameObject.name == "Pipe_hidden(Clone)" && collision.gameObject.GetComponent<Steam>().steamheld == 0) || (collision.gameObject.name == "Miner_hidden(Clone)"))
        {
           Debug.Log("Found pipe with 0 steam Assuming output");
           Output = collision.gameObject;
        }

    }
}
