using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesRoomManager : MonoBehaviour {

    public Material DefaultCubeMaterial;
    public int RateSpawnCube = 15;
    public float BasicCubeScale = 0.3f;
    public int RoomHeight = 10;
    public int RoomWidth = 10;
    public int RoomDepth = 10;
    public float RoomCenterX = 0;
    public float RoomCenterY = 0;
    public float RoomDistance = 5f;

    private static Vector3 posFirstCube;
    private GameObject[,,] _room;

    public GameObject[,,] Room
    {
        get { return _room; }
        private set { }
    }

    // Use this for initialization
    void Start () {
        _room = new GameObject[RoomHeight, RoomWidth, RoomDepth];

        BuildRoom();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BuildRoom()
    {
        var rand = new System.Random();
        //PARCOURS TOUTES LES CASES DE LA ROOM, PIS DECIDE VIA UN RAND SI ON PLACE UN CUBE OU NON
        for (var y = 0; y < RoomHeight; y++)
        {
            for (var x = 0; x < RoomWidth; x++)
            {
                for (var z = 0; z < RoomDepth; z++)
                {
                    if (rand.Next(0, 100) >= RateSpawnCube) continue;
                    var tmp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tmp.transform.localScale = new Vector3(BasicCubeScale, BasicCubeScale, BasicCubeScale);
                    tmp.AddComponent<CubeBehavior>();
                    var cubeZ = RoomDistance + rand.Next(RoomDepth);
                    tmp.transform.position = new Vector3
                    {
                        x = RoomCenterX - (BasicCubeScale * (RoomWidth / 2)) + (x * BasicCubeScale),
                        y = RoomCenterY - (BasicCubeScale * (RoomHeight / 2)) + (y * BasicCubeScale),
                        z = BasicCubeScale * z
                    };
                    tmp.GetComponent<Renderer>().material = DefaultCubeMaterial;
                    _room[y, x, z] = tmp;
                }
            }
        }
    }
}
