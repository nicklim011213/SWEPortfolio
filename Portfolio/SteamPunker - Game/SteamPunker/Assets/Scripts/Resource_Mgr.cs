using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_Mgr : MonoBehaviour
{
    public int CoalAmt;         // Int stores amount of coal
    public int OtherMatAmt;     // Int stores amount of other
    // Start is called before the first frame update
    void Start()
    {
        CoalAmt = 0;           // Set to 0 when start
        OtherMatAmt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "Resources:\nCoal: " + CoalAmt  +"\nOther: " + OtherMatAmt;    // Display it on screen. Resource gathers update this value
    }
}
