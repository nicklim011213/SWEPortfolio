using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This code is unused

public class Research_Screen : MonoBehaviour {
    public bool ResearchScreenToggle;
    public Button Toggle;
    public GameObject ResearchOverlay;

    // Start is called before the first frame update
    void Start()
    {
    ResearchScreenToggle = false;
    Toggle = this.GetComponent<Button>();       // Create a toggle bool and attach it to button
    Toggle.onClick.AddListener(SwapState);      // If clicked 
    ResearchOverlay = GameObject.Find("Research").transform.GetChild(0).gameObject; // define the actual screen
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwapState()
    {
        ResearchScreenToggle = !ResearchScreenToggle;
        if (ResearchScreenToggle == false) // if bool is off
        {
            ResearchOverlay.SetActive(false); // screen is off
        }
        else
        {
            ResearchOverlay.SetActive(true); // otherwise on
        }
    }
}
