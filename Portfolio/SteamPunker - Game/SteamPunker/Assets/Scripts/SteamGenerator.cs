using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamGenerator : MonoBehaviour
{
    public int steamheld;
    public bool Connection;
    public GameObject Output;
    public int Fuel;
    public Resource_Mgr ResourceView;
    // Start is called before the first frame update
    void Start()
    {
        steamheld = 0;
        Connection = false;
        Fuel = 250;
        ResourceView = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<Resource_Mgr>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Output = collision.gameObject;
        Connection = true;
        if (Output != null)
        {
            Debug.Log("Found Pipe");
        }
        else
        {
            Debug.Log("Detected Collision but found no pipe");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (steamheld < 0)
        {
            steamheld = 0;
        }
        if (Connection && Fuel > 0)
        {
            steamheld++;
            Fuel--;  
        }
        else if (Connection && Fuel == 0 && ResourceView.CoalAmt > 0)
        {
            ResourceView.CoalAmt--;
            steamheld++;
            Fuel = 250;
        }
        else
        {
        }
    }


}
