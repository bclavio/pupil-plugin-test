using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeHit : MonoBehaviour
{

    float locTime = 2.0f;
    float runningTime = 0.0f;

    public Wedge otherScript;

    public GameObject lastActive;
    public GameObject active;


    int counter = 2;


    void myFunction()
    {
        var raycastHit = EyeRay.CurrentlyHit;
        var camera = Camera.main;
    }



    void Update()
    {
        myFunction();


        if (PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes) == Vector2.zero) return;



        //print("Hit: " + EyeRay.CurrentlyHit.transform.name);



        if (EyeRay.CurrentlyHit.collider.gameObject.tag == "Active")
        {
            //print("Destroy: " + EyeRay.CurrentlyHit.transform.gameObject.ToString());
            runningTime += Time.deltaTime * 1;

            if (runningTime >= locTime)
            {
                GameObject.FindGameObjectWithTag("Active").GetComponent<Renderer>().material.mainTexture = Resources.Load("Paintings\\Portrait1\\portrait 3") as Texture2D;
                runningTime = 0.0f;

                newTarget();
            }

        }
        else
        {
            runningTime = 0f;
        }


    }

    public void newTarget()
    {
        lastActive = GameObject.FindGameObjectWithTag("LastActive");
        active = GameObject.FindGameObjectWithTag("Active");

        if (lastActive != null)
            lastActive.tag = "Untagged";

        active.tag = "LastActive";
        GameObject.Find("Paint" + counter).tag = "Active";

        if (counter < 6)
            counter++;
        else
            counter = 1;
    }


    private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
    {
        return hit.transform.InverseTransformPoint(hit.point);
    }


}
