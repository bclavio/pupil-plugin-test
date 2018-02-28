using UnityEngine;

public class MuseumKeyboardInput : MonoBehaviour
{
    private GameObject cam; 
    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetMuseumToCamera();
    }

    private void SetMuseumToCamera()
    {
        cam.SetActive(false);
        transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
        transform.position = Camera.main.transform.position;
        transform.Translate(new Vector3(0, 0, 5));
    }
}
