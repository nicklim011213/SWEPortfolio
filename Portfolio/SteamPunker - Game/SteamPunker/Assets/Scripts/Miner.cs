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
        ResourceView = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<Resource_Mgr>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input != null)
        {
            if (Input.GetComponent<Steam>().steamheld > 2 || Input.GetComponent<Steam>().steamheld == 2)
            {
                Chance = Random.Range(0.0f, 1.0f);
                Input.GetComponent<Steam>().steamheld = Input.GetComponent<Steam>().steamheld - 2;
                Eff = true;
                if (Chance >= .5 && CoalProgress < 100)
                {
                    CoalProgress++;
                    CoalProgress++;
                    CoalProgress++;
                    Debug.Log("Extra Steam Coal Progress X3");
                }
                else if (CoalProgress >= 100)
                {
                    CoalProgress = 0;
                    ResourceView.CoalAmt++;
                }
                else if (OtherProgress >= 100)
                {
                    ResourceView.OtherMatAmt++;
                    OtherProgress = 0;
                }
                else
                {
                    OtherProgress++;
                    OtherProgress++;
                    OtherProgress++;
                    Debug.Log("Extra Steam Other Progress X3");
                }
            }
            else if (Input.GetComponent<Steam>().steamheld < 2)
            {
                Eff = false;
                Chance = Random.Range(0.0f, 1.0f);
                Input.GetComponent<Steam>().steamheld--;

                if (Chance >= .5 && CoalProgress < 100)
                {
                    CoalProgress++;
                }
                else if (Chance >= .5 && CoalProgress >= 100)
                {
                    CoalProgress = 0;
                    ResourceView.CoalAmt++;
                }
                else if (Chance < .5 && OtherProgress >= 100)
                {
                    ResourceView.OtherMatAmt++;
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
    void OnTriggerEnter2D(Collider2D collision)
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
