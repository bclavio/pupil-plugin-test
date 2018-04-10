using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesWallManager : MonoBehaviour {

    public Material DefaultCubeMaterial;

    public float BasicCubeScale = 0.3f;
    public int WallHeight = 10;
    public int WallWidth = 10;
    public float WallCenterX = 0;
    public float WallCenterY = 0;
    public float WallDistance = 5f;

    private static Vector3 posFirstCube;
    private GameObject[,] _wall;

    public GameObject[,] Wall
    {
        get { return _wall; }
        private set { }
    }


    // Use this for initialization
    void Start () {
        _wall = new GameObject[WallHeight, WallWidth];

        //posFirstCube = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.x, Camera.main.transform.position.y + 10);
        //_mainCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //_mainCube.transform.position = posFirstCube;
        //_mainCube.transform.localScale = BasicCubeScale;

        BuildWall();
    }

    void Update () {

        
    }

    public GameObject GetCube(int y, int x)
    {
        GameObject tmp = _wall[y, x];
        return tmp;
    }

    private void BuildWall()
    {
        for(int y = 0; y < WallHeight; y++)
        {
            for (int x = 0; x < WallWidth; x++)
            {
                var tmp = GameObject.CreatePrimitive(PrimitiveType.Cube);

                tmp.transform.localScale = new Vector3(BasicCubeScale, BasicCubeScale, BasicCubeScale);
                tmp.name = x + "," + y;
                tmp.AddComponent<CubeBehavior>();
                tmp.transform.position = new Vector3
                {
                    x = WallCenterX - (BasicCubeScale * (WallWidth / 2)) + (x * BasicCubeScale),
                    y = WallCenterY - (BasicCubeScale * (WallHeight / 2)) + (y * BasicCubeScale),
                    z = WallDistance
                };
                tmp.GetComponent<Renderer>().material = DefaultCubeMaterial;
                _wall[y, x] = tmp;
            }
        }
    }

    private void BeginnerActivity()
    {
        var rand = new System.Random();

        var x = rand.Next(WallWidth);
        var y = rand.Next(WallHeight);
    }
}
