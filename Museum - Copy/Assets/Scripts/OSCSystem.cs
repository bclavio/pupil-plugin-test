using System.Collections.Generic;
using UnityEngine;

public class OSCSystem : MonoBehaviour
{
    #region Public editor fields
    [InspectorButton("Init")]
    public bool Launch;
    [InspectorButton("BeforeDoCalibration")]
    public bool StartCalibration;

    public Material DefaultOSCCubeMaterial;
    public int IterationPerMarkers = 8;
    #endregion

    #region Privates fields

    private Dictionary<string, Vector3[,]> _osData;
    private readonly GameObject[,] _grid = new GameObject[3, 3];

    private bool _isCalibRunning = false;
    private bool _isCalibDone = false;
    private int _num = 0;
    private int _step = 0;
    private GameObject _currentCube = null;

    #endregion

    #region Unity methods
    private void Start()
    {

    }

    private void Update()
    {
    }
    #endregion

    #region OSCSytem general methods
    private void Init()
    {
        Debug.Log("STARTING OSCSystem init ...");
        var cCubes = GameObject.FindGameObjectsWithTag(AppConstants.CalibrationCubeTag);

        foreach (var cube in cCubes)
        {
            var coordCube = GetCubeCoordFromName(cube);
            cube.GetComponent<Renderer>().material = DefaultOSCCubeMaterial;
            cube.transform.localPosition = new Vector3(coordCube[0] * AppConstants.CubeScaleRate - AppConstants.CubeScaleRate, coordCube[1] * AppConstants.CubeScaleRate - AppConstants.CubeScaleRate, AppConstants.CubeDistance);
            cube.transform.localRotation = new Quaternion { x = 0, y = 0, z = 0 };
            cube.transform.localScale = new Vector3(AppConstants.CubeScaleRate, AppConstants.CubeScaleRate, AppConstants.CubeDepth);
            _grid[coordCube[0], coordCube[1]] = cube;
        }
        Debug.Log("DONE: OSCSystem init ...");
    }
    #endregion

    #region OSCSystem calibration methods
    private void BeforeDoCalibration()
    {
        Debug.Log("OSCSystem calibration started !");
        DisplayGrid(false);
        SetGridColliders(false);
        _isCalibRunning = true;
        _currentCube = GetNextCube();
        SetCubeCollider(_currentCube, true);
        DisplayCube(_currentCube, true);
        InvokeRepeating("UpdateCalibration", 5, 0.3f);
    }

    private void AfterDoCalibration()
    {
        Debug.Log("OSCSystem calibration ended !");
        SetGridColliders(true);
        _isCalibRunning = false;
        _isCalibDone = true;
        CancelInvoke("UpdateCalibration");
    }

    private void UpdateCalibration()
    {
        var gaze = new Vector3(PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).x, PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).y, 2.0f);
        var dir = (Camera.main.ViewportToWorldPoint(gaze) - Camera.main.transform.localPosition).normalized;
        var ray = new Ray(Camera.main.transform.localPosition, dir);
        RaycastHit hit;

        var action = Physics.Raycast(ray, out hit, Mathf.Infinity) ? hit.transform.tag == AppConstants.CalibrationCubeTag ? RescaleAction.SizeUp : RescaleAction.SizeDown : RescaleAction.SizeDown;
        Debug.Log("HIT :" + hit.transform);
        RescaleCube(_currentCube, _step, action);
        _step++;

        if (_step != IterationPerMarkers) return;
        if (GetCubeCoordFromName(_currentCube) == new[] { AppConstants.GridHeight - 1, AppConstants.GridHeight - 1 })
        {
            AfterDoCalibration();
        }
        _step = 0;
        _num = 0;
        SetCubeCollider(_currentCube, false);
        DisplayCube(_currentCube, false);
        _currentCube = GetNextCube();
        SetCubeCollider(_currentCube, true);
        DisplayCube(_currentCube, true);
    }
    #endregion

    #region OSCSystem grid methdos
    private void DisplayGrid(bool value)
    {
        foreach (var c in _grid)
        {
            c.GetComponent<MeshRenderer>().enabled = value;
        }
    }

    private void SetGridColliders(bool value)
    {
        foreach (var c in _grid)
        {
            c.GetComponent<BoxCollider>().enabled = value;
        }
    }
    #endregion

    #region OSCSystem cube methods

    private void DisplayCube(GameObject cube, bool value)
    {
        cube.GetComponent<MeshRenderer>().enabled = value;
    }

    private void SetCubeCollider(GameObject cube, bool value)
    {
        cube.GetComponent<BoxCollider>().enabled = value;
    }

    private void RescaleCube(GameObject cube, int step, RescaleAction action)
    {
        var a = action == RescaleAction.SizeDown ? 1 : -1;

        if (step == 0)
        {
            cube.transform.localScale = new Vector3(
                cube.transform.localScale.x / 2,
                cube.transform.localScale.y / 2,
                cube.transform.localScale.z / 2
            );
        }
        else
        {
            var newScale = AppConstants.CubeScaleRate * (_num * 2 + a) / Mathf.Pow(2, step);
            cube.transform.localScale = new Vector3(newScale, newScale, AppConstants.CubeDepth);
            _num = (_num * 2 + a);
        }
    }

    private int[] GetCubeCoordFromName(GameObject cube)
    {
        var args = cube.name.Split(':');
        var coor = args[1].Split(';');
        var x = int.Parse(coor[0]);
        var y = int.Parse(coor[1]);
        return new[] { x, y };
    }

    private GameObject GetNextCube()
    {
        if (_currentCube == null)
        {
            return _grid[0, 0];
        }
        var prevCoor = GetCubeCoordFromName(_currentCube);
        var x = prevCoor[1];
        var y = prevCoor[0];
        if (y < AppConstants.GridHeight - 1 && x == AppConstants.GridWidth - 1)
        {
            x = 0;
            y++;
        }
        else if (y == AppConstants.GridHeight - 1 && x == AppConstants.GridWidth - 1)
        {
            x = 0;
            y = 0;
        }
        else
        {
            x++;
        }
        return _grid[y, x];
    }

    #endregion

}

public enum RescaleAction
{
    SizeUp = 10,
    SizeDown = 20
}
