using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PaintingsSets : MonoBehaviour
{
    public float TimePerSet = 90f;

    public static PaintingsSet CurrentSet = null;
    private List<PaintingsSet> _paintingsSets = new List<PaintingsSet>();
    private float prevDelta = 0;

    // Use this for initialization
    void Start()
    {
        var info = new DirectoryInfo(Application.dataPath + "\\Resources\\Paintings");
        var sets = new List<DirectoryInfo>(info.GetDirectories());

        foreach (var t in sets)
        {
            var files = t.GetFiles("*.jpg").ToList();
            files = files.Concat(t.GetFiles("*.png")).ToList();
            _paintingsSets.Add(new PaintingsSet
            {
                SetName = t.Name,
                Picture1 = Resources.Load("Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(0).Name)) as Texture2D,
                Picture1Name = files.ElementAt(0).Name,
                Picture2 = Resources.Load("Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(1).Name)) as Texture2D,
                Picture2Name = files.ElementAt(1).Name,
                Picture3 = Resources.Load("Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(2).Name)) as Texture2D,
                Picture3Name = files.ElementAt(2).Name
            });
        }
        CurrentSet = _paintingsSets.FirstOrDefault();
        SetPaintings(_paintingsSets.FirstOrDefault());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayNextSet(KeyCode.LeftArrow);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayNextSet(KeyCode.RightArrow);
        }
        prevDelta += Time.deltaTime;
        if (prevDelta >= TimePerSet)
        {
            DisplayNextSet(KeyCode.RightArrow);
            prevDelta = 0;
        }
    }

    private void DisplayNextSet(KeyCode key)
    {
        var index = _paintingsSets.FindIndex(x => x == CurrentSet);
        switch (key)
        {
            case KeyCode.LeftArrow:
                index -= 1;
                break;
            case KeyCode.RightArrow:
                index += 1;
                break;
        }
        if (index < 0)
            index = _paintingsSets.Count - 1;
        else if (index == _paintingsSets.Count)
            index = 0;
        SetPaintings(_paintingsSets[index]);
        CurrentSet = _paintingsSets[index];
        LoggerBehavior.DoLog();
    }

    private void SetPaintings(PaintingsSet set)
    {
        GameObject.Find("Paint1").GetComponent<Renderer>().material.mainTexture = set.Picture1;
        GameObject.Find("Paint2").GetComponent<Renderer>().material.mainTexture = set.Picture2;
        GameObject.Find("Paint3").GetComponent<Renderer>().material.mainTexture = set.Picture3;
    }
}

public class PaintingsSet
{
    public string SetName { get; set; }

    public Texture2D Picture1 { get; set; }
    public string Picture1Name { get; set; }
    public Texture2D Picture2 { get; set; }
    public string Picture2Name { get; set; }
    public Texture2D Picture3 { get; set; }
    public string Picture3Name { get; set; }
}