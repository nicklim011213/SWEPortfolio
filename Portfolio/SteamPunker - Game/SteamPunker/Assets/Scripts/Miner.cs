using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public GameObject Input;
    public Resource_Mgr ResourceView;
    public int CoalProgress;
    public int OtherProgress;
    public float Chance;
    public bool Eff;
    // Start is called before the first frame update
    void Start()
    {
        ResourceView = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<Resource_Mgr>(); // Opens a simple screen displaying what it gathered
    }

    // Update is called once per frame
    void Update()
    {

        if (Input != null)
        {
            if (Input.GetComponent<Steam>().steamheld > 2 || Input.GetComponent<Steam>().steamheld == 2) // If the input has > 2 steam do its efficency boost
            {
                Chance = Random.Range(0.0f, 1.0f);                                                      // Determines resouce type
                Input.GetComponent<Steam>().steamheld = Input.GetComponent<Steam>().steamheld - 2;      // remove 2 steam
                Eff = true;
                if (Chance >= .5 && CoalProgress < 100)
                {
                    CoalProgress++;
                    CoalProgress++;
                    CoalProgress++;
                    Debug.Log("Extra Steam Coal Progress X3");                                          // 3x coal progress
                }
                else if (CoalProgress >= 100)                                                           // reset and add 1 coal
                {
                    CoalProgress = 0;
                    ResourceView.CoalAmt++;
                }
                else if (OtherProgress >= 100)                                                          // reset and add other
                {
                    ResourceView.OtherMatAmt++;
                    OtherProgress = 0;
                }
                else
                {
                    OtherProgress++;                                                                    // 3x Other progress
                    OtherProgress++;
                    OtherProgress++;
                    Debug.Log("Extra Steam Other Progress X3");
                }
            }
            else if (Input.GetComponent<Steam>().steamheld < 2)                                         // If less than 2
            {
                Eff = false;                                                                            // no efficency boost
                Chance = Random.Range(0.0f, 1.0f);                                                      // determine gathered materail
                Input.GetComponent<Steam>().steamheld--;
    
                if (Chance >= .5 && CoalProgress < 100)                                                 // single progress coal
                {
                    CoalProgress++;
                }
                else if (Chance >= .5 && CoalProgress >= 100)                                           // reset coal add +1 to stockpile
                {
                    CoalProgress = 0;
                    ResourceView.CoalAmt++;
                }
                else if (Chance < .5 && OtherProgress >= 100)
                {
                    ResourceView.OtherMatAmt++;                                                         // reset other add +1
                    OtherProgress = 0;
                }
                else
                {
                    OtherProgress++;
                }
            }
            else if (Input.GetComponent<Steam>().steamheld < 1)
            {
                Debug.Log("Not Enough Steam");
            }
        }


    }
    void OnTriggerEnter2D(Collider2D collision)                                                       // Attachment logic to pipes
    {
        Debug.Log("Attached");
        if (collision.gameObject.name == "Pipe_hidden(Clone)")      
        {
            Debug.Log("Is Pipe");
            Input = collision.gameObject;
        }
        else
        {
        }

    }
}
