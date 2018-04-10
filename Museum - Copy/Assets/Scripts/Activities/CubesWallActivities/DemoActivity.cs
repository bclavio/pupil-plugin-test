using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoActivity : MonoBehaviour {


    [InspectorButton("Init")]
    public bool LaunchActivity;

    public CubesWallManager Manager;
    public int NbCubes = 10;
    public Material CubeHighLight;

    private List<Dictionary<int, GameObject>> cubes;
    private int indexCube = 0;
    private bool cubeMaterialChanged = false;
    private bool isStarted = false;
    private int idActivity = -1;
    private float cubeTimeCounter = 0.0f;

	void Start () {
        cubes = new List<Dictionary<int, GameObject>>();
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(50, 300, 200, 50), cubeTimeCounter.ToString());
    }

    void Update () {

        if (indexCube == NbCubes || !isStarted)
        {
            isStarted = false;
            return;
        }

        var cube = cubes[idActivity][indexCube];

        if (!cubeMaterialChanged)
        {
            cube.GetComponent<Renderer>().material = CubeHighLight;
            cubeMaterialChanged = true;
            cubeTimeCounter = 0.0f;
        }

        cubeTimeCounter += Time.deltaTime;
        

        if (cube.GetComponent<CubeBehavior>().IsOvered)
        {
            cube.GetComponent<Renderer>().material = Manager.DefaultCubeMaterial;
            indexCube++;
            cubeMaterialChanged = false;
        }
        
    }

    private void Init()
    {
        var rand = new System.Random();

        idActivity++;
        indexCube = 0;
        
        cubes.Add(new Dictionary<int, GameObject>());

        for (int i = 0; i < NbCubes; i++)
        {
            cubes[idActivity].Add(i, Manager.GetCube(rand.Next(Manager.WallHeight), rand.Next(Manager.WallWidth)));
        }

        isStarted = true;
    }
}
