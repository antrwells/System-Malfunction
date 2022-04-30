using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EventEditor : EditorWindow
{

    public static GameEvent Editing = null;

    [MenuItem("Edge/Editors/Event Editor")]
    public static void ShowWindow()
    {

        EditorWindow.GetWindow(typeof(EventEditor));

    }

    Vector2 sp;
    void OnGUI()
    {


        sp = GUILayout.BeginScrollView(sp);


        GUILayout.BeginHorizontal();

        GUILayout.Label("Current Event");
        Editing = EditorGUILayout.ObjectField(Editing, typeof(GameEvent)) as GameEvent;



        if (Editing == null)
        {

            GUILayout.EndScrollView();
            GUILayout.EndHorizontal();
            return;

        }

        GUILayout.Label("Name");
        Editing.EventName = GUILayout.TextField(Editing.EventName);

        if (GUILayout.Button("Reset"))
        {
            Editing.Clear();
        }

        GUILayout.EndHorizontal();


        foreach (var ev in Editing.SubEvents)
        {


            ev.EvType = (SubEvent.EventType)EditorGUILayout.EnumPopup(ev.EvType);

            GUILayout.BeginHorizontal();

            switch (ev.EvType)
            {
                case SubEvent.EventType.ShowText:

                    ev.Text = EditorGUILayout.TextField(ev.Text);
                    ev.Position2D = EditorGUILayout.Vector2Field("Position", ev.Position2D);
                    break;

                case SubEvent.EventType.StopEvent:

                    //   ev.EventName = EditorGUILayout.TextField(ev.EventName);

                    ev.Event = EditorGUILayout.ObjectField(ev.Event, typeof(GameEvent)) as GameEvent;


                    break;
                case SubEvent.EventType.StartEvent:

                    ev.Event = EditorGUILayout.ObjectField(ev.Event, typeof(GameEvent)) as GameEvent;

                    break;
                case SubEvent.EventType.StartConverse:

                    ev.Converse = EditorGUILayout.ObjectField(ev.Converse, typeof(Converse)) as Converse;
                    
                    break;
                case SubEvent.EventType.WaitForTagRemove:

                    ev.WaitTag = EditorGUILayout.TextField(ev.WaitTag);

                    break;
                case SubEvent.EventType.RemoveMark:

                    GUILayout.Label("MarkUp");
                    ev.Mark = EditorGUILayout.ObjectField(ev.Mark, typeof(MarkUp)) as MarkUp;

                    break;
                case SubEvent.EventType.StopSound:

                    GUILayout.Label("AudioSource");
                    ev.Sound = EditorGUILayout.ObjectField(ev.Sound, typeof(AudioSource)) as AudioSource;

                    break;
                case SubEvent.EventType.ActivateUsable:

                    ev.Target = EditorGUILayout.ObjectField(ev.Target, typeof(GameObject)) as GameObject;
                    ev.Usable = EditorGUILayout.ObjectField(ev.Usable, typeof(Usable)) as Usable;

                    break;
                case SubEvent.EventType.AddMarkUp:

                    GUILayout.Label("MarkUp");
                    ev.Mark = EditorGUILayout.ObjectField(ev.Mark,typeof(MarkUp)) as MarkUp;
                   

                    break;
                case SubEvent.EventType.PlaySound:


                    GUILayout.Label("AudioSource");
                    ev.Sound = EditorGUILayout.ObjectField(ev.Sound, typeof(AudioSource)) as AudioSource;
                    GUILayout.Label("Loop?");
                    ev.LoopSound = EditorGUILayout.Toggle(ev.LoopSound);

                    break;
                case SubEvent.EventType.Wait:

                    GUILayout.Label("Time");
                    ev.WaitTime = EditorGUILayout.FloatField(ev.WaitTime);

                    break;
                case SubEvent.EventType.WaitForTag:

                    GUILayout.Label("Tag");
                    ev.WaitTag = EditorGUILayout.TextField(ev.WaitTag);
                    GUILayout.Label("Clear Tag?");
                    ev.ClearTag = EditorGUILayout.Toggle(ev.ClearTag);


                    break;

            }
            GUILayout.EndHorizontal();

        }
           

            if(GUILayout.Button("New SubEvent"))
            {
                var newEv = ScriptableObject.CreateInstance<SubEvent>();
                Editing.SubEvents.Add(newEv);
            }


        

        GUILayout.EndScrollView();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
