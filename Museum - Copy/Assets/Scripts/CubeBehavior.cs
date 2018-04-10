using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour, IGazeContact
{
    private bool isOvered;

    public bool IsOvered
    {
        get { return isOvered; }
        private set { }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGazeOver()
    {
        
    }

    public void OnGazeEnter()
    {
        isOvered = true;
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    public void OnGazeExit()
    {
        isOvered = false;
        this.GetComponent<MeshRenderer>().enabled = true;
    }
}
