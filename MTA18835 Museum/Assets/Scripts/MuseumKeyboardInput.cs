using UnityEngine;

public class MuseumKeyboardInput : MonoBehaviour
{
    public static GameObject cam2; // in inspector under Musuem object 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetMuseumToCamera();
    }

    private void SetMuseumToCamera()
    {
        // Bianca changed this
        cam2 = GameObject.Find("Camera");
        cam2.SetActive(false);
        //LoggerBehavior.SceneStatus = "running scene";
        //
        transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
        transform.position = Camera.main.transform.position;
        transform.Translate(new Vector3(0, 0, 5));
    }
}
