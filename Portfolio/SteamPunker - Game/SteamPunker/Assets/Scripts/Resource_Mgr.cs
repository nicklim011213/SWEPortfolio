using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_Mgr : MonoBehaviour
{
    public int CoalAmt;
    public int OtherMatAmt;
    // Start is called before the first frame update
    void Start()
    {
        CoalAmt = 0;
        OtherMatAmt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "Resources:\nCoal: " + CoalAmt  +"\nOther: " + OtherMatAmt;
    }
}
