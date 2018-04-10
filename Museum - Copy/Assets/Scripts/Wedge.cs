using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wedge : MonoBehaviour {

    private LineRenderer wedge1;
    private LineRenderer wedge2;

    public GameObject lastActive;
    public GameObject active;

    int counter = 2;

    void Start()
    {
        lastActive = GameObject.FindGameObjectWithTag("LastActive");
        active = GameObject.FindGameObjectWithTag("Active");
    }

    void Update()
    {
        lastActive = GameObject.FindGameObjectWithTag("LastActive");
        active = GameObject.FindGameObjectWithTag("Active");

        if (lastActive == null)
        {
            wedge1 = GetComponent<LineRenderer>();
            wedge2 = GetComponent<LineRenderer>();

            wedge1.SetPosition(0, GameObject.Find("Paint5").transform.position);
            wedge1.SetPosition(1, active.transform.position);

            //wedge2.SetPosition(0, GameObject.Find("Paint5").transform.position);
            //wedge2.SetPosition(1, active.transform.position);
        }
        else if (lastActive != null)
        {
            wedge1 = GetComponent<LineRenderer>();

            wedge1.SetPosition(0, lastActive.transform.position);
            wedge1.SetPosition(1, active.transform.position);
        }

        if (Input.GetKeyDown("space"))
        {
            if (lastActive != null)
                lastActive.tag = "Untagged";
            
            active.tag = "LastActive";
            GameObject.Find("Paint" + counter).tag="Active";

            if (counter < 6)
                counter++;
            else
                counter = 1;
        }
    }

}
