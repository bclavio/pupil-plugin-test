using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggerBehavior : MonoBehaviour
{

    #region Public editor fields

    //private string PatientName = "Anonym";
    // TO DO: enter name with inputfield UI, fix outcommented code and enable the UI element
    // The user should enter the text/name before the calibration
    // To get this to work, perhaps perhebit all keycode events until the calibration is finished 
    //public InputField mainInputField;
    //public static string inputString;
    //public GameObject inputFieldObj;
    //public GameObject mainCam;
    //public static string SceneStatus = "starting";

    #endregion

    //private int SessionNumber = 1; // if the name is the same, increase the SessionNumber else SessionNumber = 1
    private static Logger _logger;
    private DateTime _starTime;
    private static List<object> _toLog;



    #region Unity Methods

    private void Start()
    {
        //mainInputField.text = "Enter name here...";
        _toLog = new List<object>();
        _starTime = DateTime.Now;
        //mainCam = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        DoLog();
        if (PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes) == Vector2.zero) return;
        AddToLog();
        /*
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            inputString = mainInputField.text.ToString();
            inputFieldObj.SetActive(false);
        }
        */


    }


    private void AddToLog()
    {
        var date = DateTime.Now;
        var raycastHit = EyeRay.CurrentlyHit;
        var camera = Camera.main;
        var tmp = new
        {
            a = date.ToString("dd/MM/yy HH:mm:ss"),
            abis = date.Millisecond,
            ater = (date - _starTime).TotalMilliseconds,
            //b = SceneStatus,
            //b = PatientName //inputString,
            //c = SessionNumber, // also removed in appconstant script
            cbis = PaintingsSets.CurrentSet.SetName,
            d = camera.transform.position.x,
            e = camera.transform.position.y,
            f = camera.transform.position.z,
            g = camera.transform.rotation.x,
            h = camera.transform.rotation.y,
            i = camera.transform.rotation.z,
            j = PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).x,
            k = PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).y,
            //////////// A VOIR ////////////
            //l = PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).x,
            //m = PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).y,
            ///////////// ///// ////////////
            n = raycastHit.transform != null ? raycastHit.transform.name : "none",
            o = raycastHit.transform != null ? raycastHit.transform.position.x.ToString() : "none",
            p = raycastHit.transform != null ? raycastHit.transform.position.y.ToString() : "none",
            q = raycastHit.transform != null ? raycastHit.transform.position.z.ToString() : "none",
            r = raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).x.ToString() : "none",
            s = raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).y.ToString() : "none",
            t = raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).z.ToString() : "none"
        };
        _toLog.Add(tmp);
    }

    private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
    {
        return hit.transform.InverseTransformPoint(hit.point);
    }

    public static void DoLog()
    {
        _logger = Logger.Instance;
        if (File.ReadAllText(_logger.FullPathLogFile).Equals(""))
        {
            var firstRow = new { a = AppConstants.CsvFirstRow };
            _toLog.Add(firstRow);
        }
        _logger.Log(_toLog.ToArray());
        _toLog.Clear();
    }

    #endregion
}
