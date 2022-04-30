using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AmbientEditor : EditorWindow
{


    public Ambience Editing = null;

    [MenuItem("Edge/Editors/Ambience")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AmbientEditor));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {


        GUILayout.BeginHorizontal();

        GUILayout.Label("Current Ambience");
        Editing = EditorGUILayout.ObjectField(Editing, typeof(Ambience)) as Ambience;

        if (Editing == null)
        {

            GUILayout.EndHorizontal();
            return;

        }

        if (GUILayout.Button("Reset"))
        {
            Editing.Clear();
        }

        GUILayout.EndHorizontal();

        Editing.AudioSrc = EditorGUILayout.ObjectField(Editing.AudioSrc, typeof(AudioSource)) as AudioSource;


        Editing.Music = EditorGUILayout.ObjectField(Editing.Music, typeof(AudioClip)) as AudioClip;

        Editing.ActionMusic = EditorGUILayout.ObjectField(Editing.ActionMusic, typeof(AudioClip)) as AudioClip;

        Editing.FearMusic = EditorGUILayout.ObjectField(Editing.FearMusic, typeof(AudioClip)) as AudioClip;

        Editing.SuccessMusic = EditorGUILayout.ObjectField(Editing.SuccessMusic, typeof(AudioClip)) as AudioClip;


        Editing.SceneName = EditorGUILayout.TextField(Editing.SceneName);

        Editing.Font = EditorGUILayout.ObjectField(Editing.Font, typeof(Font)) as Font;

        Editing.ScreenPos = EditorGUILayout.Vector2Field("ScreenPos", Editing.ScreenPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
