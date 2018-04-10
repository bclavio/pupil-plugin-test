using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookingAt : MonoBehaviour
{

    string lookingAtName;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var raycastHit = EyeRay.CurrentlyHit;

        lookingAtName = raycastHit.transform != null ? raycastHit.transform.name : "none";
        print("Looking at: " + lookingAtName);
    }
}
