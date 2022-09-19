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
        dropdown = GameObject.Find("Canvas");           // dropdown select menu is declared along with status which displays if you are placing object alert 
        StatusCode = false;                             // which is the status alert dropdown value which is the selected object and Dot which is a cursor.
        alert = GameObject.Find("Alert");
        dropdownvalue = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Dropdown>();
        Dot = GameObject.Find("Dot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))               // Toggles status code message when hitting esc
        {
            StatusCode = !StatusCode;
        }                                           

        CursorLoc = GameObject.Find("Dot").transform.position;

        if (StatusCode)                                     // If status code is on
        {
            alert.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Mouse0))           // if click
            {
                selection = GetSelection(dropdownvalue);    // Get select from dropdown
                Object.Instantiate(selection, CursorLoc, Rotate, Parent);   // create new object at cursor that is selection.
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Snap Cursor");                   // if right click
                if ((Dot.transform.position.x % 1) >= 0.5)  // This code blocks just snap a cursor into place. Auto align tool
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
    {                                                   // Index of each object on the dropdown that you want to create
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
