using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkUpSystem : MonoBehaviour
{
    public static MarkUpSystem This = null;
    public List<MarkUp> MarkList = new List<MarkUp>();
    public Camera CurrentCam;

    // Start is called before the first frame update
    void Start()
    {
        This = this;
    }


    public static void RemoveMarkUp(MarkUp mark)
    {
        This.MarkList.Remove(mark);
    }
    public static void AddMarkUp(MarkUp mark)
    {
        This.MarkList.Add(mark);
    }
    GUIStyle guiS = new GUIStyle();

    void OnGUI()
    {

        if (MarkList.Count > 0)
        {
           // Debug.LogError("Showing Marks");
        }

        foreach(var mark in MarkList)
        {

            var pos = mark.Target.transform.position;

            var vec = CurrentCam.WorldToScreenPoint(pos);
            vec.y = Screen.height - vec.y;

            //vec.y += 20;

            guiS.fontSize = 23;
            guiS.fontStyle = FontStyle.Bold;
            //guiS.normal.

            guiS.normal.textColor = new Color(1, 1f, 1f, 0.9f);

            GUI.Label(new Rect(vec.x, vec.y, 200, 30), mark.Text,guiS);


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
