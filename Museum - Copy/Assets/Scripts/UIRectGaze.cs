using UnityEngine;

public class UIRectGaze : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes) == Vector2.zero) return;
        var eyeVec = new Vector3(PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).x, PupilData._2D.GetEyeGaze(PupilData.GazeSource.BothEyes).y, 2);

        var res = Camera.main.ViewportToWorldPoint(eyeVec);
        res = Camera.main.WorldToViewportPoint(res);
        this.transform.position = new Vector3(res.x * Screen.width, res.y * Screen.height, res.z);
    }
}
