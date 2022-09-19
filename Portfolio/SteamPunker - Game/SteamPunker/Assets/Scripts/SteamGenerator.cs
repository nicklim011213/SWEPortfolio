using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamGenerator : MonoBehaviour
{
    public int steamheld; // steam held before pipe output
    public bool Connection; // is it connected
    public GameObject Output; // the poutput object
    public int Fuel;           // the fuel (coal)
    public Resource_Mgr ResourceView; // the resource view (so it can reduce coal in view)
    // Start is called before the first frame update
    void Start()
    {
        steamheld = 0;
        Connection = false;
        Fuel = 250;
        ResourceView = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<Resource_Mgr>(); // This is the resource screen
    }

    void OnTriggerEnter2D(Collider2D collision) // When a collision with another object is detected
    {
        Output = collision.gameObject;          // its an output
        Connection = true;
        if (Output != null)                     // if nothing there then no pipe otherwise there is a pipe connection
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
        if (steamheld < 0)      // Fixes a small bug
        {
            steamheld = 0;
        }
        if (Connection && Fuel > 0) // if it is running with a connection
        {
            steamheld++; // make steam
            Fuel--;  // reduce fuel
        }
        else if (Connection && Fuel == 0 && ResourceView.CoalAmt > 0) // if no fuel and you have coal in your resources
        {
            ResourceView.CoalAmt--; // reduce coal amount in resources view
            steamheld++;            // make a steam
            Fuel = 250;             // add 250 fuel
        }
        else
        {
        }
    }


}
