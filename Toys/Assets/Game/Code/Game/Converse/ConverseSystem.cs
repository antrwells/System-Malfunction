using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverseSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static ConverseSystem This = null;
    public ConverseItem Item = null;
    public Texture2D BackTex;
    int CurrentItem = -1;
    bool Done = false;
    float StartTime = 0.0f;
    

    public Converse Cur = null;

    public static void SetConverse(Converse converse)
    {
        This.Cur = converse;
        //This.Item = converse.Items[0];

    }

    void Start()
    {
        This = this;
    }


    GUIStyle textStyle = new GUIStyle();
    void OnGUI()
    {
        if (Item == null) return;
        if (Done) return;
        int sx = Screen.width;
        int sy = Screen.height;

        string name = "????";
        Texture2D ava = null;

        if (Item.Persona != null)
        {
            name = Item.Persona.FullName;
          
        }

        textStyle.fontSize = 23;
        textStyle.normal.textColor = new Color(1, 1, 1, 0.8f);

        //GUI.Box(new Rect(sx / 2 - 300, sy - 200, 600, 180),name);

        GUI.DrawTexture(new Rect(sx / 2 - 400, sy - 250, 800, 200), BackTex, ScaleMode.StretchToFill, true, 1.0f, new Color(1, 1, 1, 0.7f), 0, 0);
        GUI.Label(new Rect(sx / 2 - 140, sy - 240, 250, 50), name, textStyle);
        GUI.Label(new Rect(sx / 2 - 380, sy - 205, 650, 180), Item.Text, textStyle);

        if (ava != null)
        {

            //GUI.Box(new Rect(sx / 2 - 402 - 50, sy - 250 - 58, 100, 100), "",)

            GUI.DrawTexture(new Rect(sx / 2 - 400 - 50, sy - 250 - 56, 96, 96), ava, ScaleMode.ScaleToFit, true, 1.0f, new Color(1, 1, 1, 0.5f), 0, 0);

        }

        //GUI.Label(new Rect(sx / 2 - 280, sy - 190, 560, 170), Item.Text,textStyle);


    }

    void NextItem()
    {
  //      Debug.LogError("NextItem");
/*
        CurrentItem++;
        if(CurrentItem>=Cur.Items.Count)
        {
            Done = true;
            Item = null;
            return;
        }
        Item = Cur.Items[CurrentItem];
        StartTime = Time.time;
        if (Item.AddTag.Length>0)
        {
            EventSystem.NewEventTag(Item.AddTag);
        }
        if (Item.StopTag.Length > 0)
        {
            EventSystem.ClearTag(Item.StopTag);
        }
*/
        //Debug.Break();

    }

    // Update is called once per frame
    void Update()
    {
        if (Cur == null) return;
       if(Item == null)
        {
            NextItem();
        }
        if (Item != null)
        {
            if (Time.time > (StartTime + Item.WaitTime))
            {
                NextItem();
            }
            if(Input.GetMouseButtonDown(0))
            {
                NextItem();
            }
        }

    }
}
