using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConverseEditor : EditorWindow
{

    public static Converse Editing;

    [MenuItem("Edge/Editors/(Deprecated)Converse Editor")]
    public static void ShowWindow()
    {

        EditorWindow.GetWindow(typeof(ConverseEditor));

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 sp;

    void OnGUI()
    {

        sp = GUILayout.BeginScrollView(sp);


        GUILayout.BeginHorizontal();

        GUILayout.Label("Current Converse");
        Editing = EditorGUILayout.ObjectField(Editing, typeof(Converse)) as Converse;

        if (Editing == null)
        {

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            return;

        }

        if (GUILayout.Button("Reset"))
        {
            Editing.Clear();
        }

        GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            if (Editing.Root != null)
            {
                EditItem(Editing.Root);
            }
            else
            {
                if (GUILayout.Button(("Create Root")))
                {

                    Editing.Root = ScriptableObject.CreateInstance<ConverseItem>();

                }

                GUILayout.EndHorizontal();




            GUILayout.BeginHorizontal();


            //if (GUILayout.Button("Delete", GUILayout.Width(70)))
            //{
             //   Editing.Items.Remove(item);
                return;
            //}

            //GUILayout.EndHorizontal();
            

        }


        GUILayout.EndScrollView();

    }

    private static void EditItem(ConverseItem item, int tab = 0)
    {

        //GUILayout.BeginVertical();

        using (new GUILayout.HorizontalScope())
        {

            GUILayout.Space(15 * tab);

            using (new GUILayout.VerticalScope())
            {

                GUILayout.Label("Text");

                item.Persona = EditorGUILayout.ObjectField(item.Persona, typeof(GamePersona)) as GamePersona;
                item.ShortText = EditorGUILayout.TextField(item.ShortText);
                item.Text = EditorGUILayout.TextArea(item.Text, GUILayout.Height(256));

                item.Audio = EditorGUILayout.ObjectField(item.Audio, typeof(AudioClip)) as AudioClip;
                item.WaitTime = EditorGUILayout.FloatField(item.WaitTime);


                GUILayout.BeginHorizontal();
                GUILayout.Label("Stop Tag", GUILayout.Width(80));
                item.StopTag = EditorGUILayout.TextField(item.StopTag);
                GUILayout.Label("Add Tag",GUILayout.Width(80));
                item.AddTag = EditorGUILayout.TextField(item.AddTag);
                GUILayout.EndHorizontal();



                if (item.SubItems.Count > 0)
                {

                    foreach (var si in item.SubItems)
                    {




                        EditItem(si,tab+1);
                        if(GUILayout.Button("Delete"))
                        {
                            item.SubItems.Remove(si);
                            return;
                        }



                        //   GUILayout.EndHorizontal();
                    }
                }


                if (GUILayout.Button("New Response"))
                {

                    var np = ScriptableObject.CreateInstance(typeof(ConverseItem)) as ConverseItem;
                    np.Text = "";
                    item.SubItems.Add(np);

                }


            }
        }

        //GUILayout.EndVertical();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
