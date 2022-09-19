using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Research_Screen : MonoBehaviour {
    public bool ResearchScreenToggle;
    public Button Toggle;
    public GameObject ResearchOverlay;

    // Start is called before the first frame update
    void Start()
    {
    ResearchScreenToggle = false;
    Toggle = this.GetComponent<Button>();
    Toggle.onClick.AddListener(SwapState);
    ResearchOverlay = GameObject.Find("Research").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwapState()
    {
        ResearchScreenToggle = !ResearchScreenToggle;
        if (ResearchScreenToggle == false)
        {
            ResearchOverlay.SetActive(false);
        }
        else
        {
            ResearchOverlay.SetActive(true);
        }
    }
}
