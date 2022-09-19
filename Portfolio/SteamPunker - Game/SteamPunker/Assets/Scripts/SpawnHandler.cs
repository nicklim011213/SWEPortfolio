using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHandler : MonoBehaviour
{
    public Vector3 CursorLoc;
    public Quaternion Rotate;
    public Transform Parent;
    public bool StatusCode;
    public GameObject alert;
    public GameObject dropdown;
    public Dropdown dropdownvalue;
    public GameObject selection;
    public GameObject Dot;
    public GameObject SteamGenerator;
    public GameObject SteamPipe;
    public GameObject SteamMiner;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GameObject.Find("Canvas");
        StatusCode = false;
        alert = GameObject.Find("Alert");
        dropdownvalue = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Dropdown>();
        Dot = GameObject.Find("Dot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StatusCode = !StatusCode;
        }

        CursorLoc = GameObject.Find("Dot").transform.position;

        if (StatusCode)
        {
            alert.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                selection = GetSelection(dropdownvalue);
                Object.Instantiate(selection, CursorLoc, Rotate, Parent);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Snap Cursor");
                if ((Dot.transform.position.x % 1) >= 0.5)
                {
                    if ((Dot.transform.position.y % 1) >= 0.5)
                        {
                        Debug.Log("Snap X High Y High");
                        CursorLoc = Dot.transform.position = new Vector2(Mathf.Ceil(Dot.transform.position.x), Mathf.Ceil(Dot.transform.position.y));
                        }
                    if ((Dot.transform.position.y % 1) <= 0.5)
                        {
                        Debug.Log("Snap X High Y Low");
                        CursorLoc = Dot.transform.position = new Vector2(Mathf.Ceil(Dot.transform.position.x), Mathf.Floor(Dot.transform.position.y));
                        }
                }
                else
                {
                    if ((Dot.transform.position.y % 1) >= 0.5)
                    {
                        Debug.Log("Snap X Low Y High");
                        CursorLoc = Dot.transform.position = new Vector2(Mathf.Ceil(Dot.transform.position.x), Mathf.Ceil(Dot.transform.position.y));
                    }
                    if ((Dot.transform.position.y % 1) <= 0.5)
                    {
                        Debug.Log("Snap X Low Y Low");
                        CursorLoc = Dot.transform.position = new Vector2(Mathf.Ceil(Dot.transform.position.x), Mathf.Floor(Dot.transform.position.y));
                    }
                }
            }
        }
        else
        {
            alert.SetActive(true);
        }
    }


    GameObject GetSelection(Dropdown dropdown)
    {
        GameObject selection;
        if (dropdown.value == 0)
        {
            selection = SteamGenerator;
        }
        else if (dropdown.value == 1)
        {
            selection = SteamPipe;
        }
        else
        {
            selection = SteamMiner;
        }
        return selection;
    }
}
