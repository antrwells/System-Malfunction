using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemEditor : EditorWindow
{ 


    [MenuItem("Edge/Editors/Item Editor")]
    public static void ShowWindow()
    {

        EditorWindow.GetWindow(typeof(ItemEditor));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static GameItem Editing=null;


    void OnGUI()
    {

        Editing = EditorGUILayout.ObjectField(Editing, typeof(GameItem)) as GameItem;

        if (Editing == null) return;

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Name", GUILayout.Width(70));
        Editing.ItemName = GUILayout.TextField(Editing.ItemName,GUILayout.Width(250));

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Avatar");

        Editing.ItemView = EditorGUILayout.ObjectField(Editing.ItemView, typeof(Texture2D)) as Texture2D;
        
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Info");

        Editing.ItemInfo = EditorGUILayout.TextArea(Editing.ItemInfo,GUILayout.Width(256),GUILayout.Height(256));

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Item Type", GUILayout.Width(80));

        Editing.IType = (GameItem.ItemType)EditorGUILayout.EnumPopup(Editing.IType);
          
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Use Sound", GUILayout.Width(70));

        Editing.UseSound = EditorGUILayout.ObjectField(Editing.UseSound, typeof(AudioSource)) as AudioSource;

      
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        GUILayout.Label("Iventory Width",GUILayout.Width(120));
        Editing.InventW = EditorGUILayout.IntField(Editing.InventW, GUILayout.Width(64));
        GUILayout.Label("Inventory Height", GUILayout.Width(120));
        Editing.InventH = EditorGUILayout.IntField(Editing.InventH, GUILayout.Width(64));

        //GUILayout.BeginHorizontal();




        GUILayout.EndHorizontal();

        switch (Editing.IType)
        {
            case GameItem.ItemType.Usables:
            case GameItem.ItemType.Key:
                GUILayout.BeginHorizontal();
                GUILayout.Label("Key ID", GUILayout.Width(80));
                Editing.KeyID = EditorGUILayout.TextField(Editing.KeyID, GUILayout.Width(180));
              //  Editing.Reusable = EditorGUILayout.Toggle("Reusable", Editing.Reusable);
                GUILayout.EndHorizontal();

                break;
            case GameItem.ItemType.Weapon:


                GUILayout.BeginHorizontal();

                GUILayout.Label("Strength", GUILayout.Width(70));

                Editing.Stength = EditorGUILayout.FloatField(Editing.Stength,GUILayout.Width(80));

                GUILayout.Label("Range", GUILayout.Width(70));

                Editing.MaxRange = EditorGUILayout.FloatField(Editing.MaxRange, GUILayout.Width(80));

                GUILayout.EndHorizontal();

                GUILayout.Space(15);

                GUILayout.BeginHorizontal();

                GUILayout.Label("Max Uses");

                Editing.MaxUses = EditorGUILayout.IntField(Editing.MaxUses, GUILayout.Width(80));

                GUILayout.Label("Initial uses", GUILayout.Width(70));

                Editing.CurUses = EditorGUILayout.IntField(Editing.CurUses, GUILayout.Width(80));

                GUILayout.Label("Initial refills", GUILayout.Width(70));

                Editing.CurRefills = EditorGUILayout.IntField(Editing.CurRefills, GUILayout.Width(80));

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("Exit Point", GUILayout.Width(100));

                Editing.ExitPoint = EditorGUILayout.ObjectField(Editing.ExitPoint, typeof(GameObject)) as GameObject;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.Label("VFX:", GUILayout.Width(70));

                //Editing.Use
                Editing.UseFX = EditorGUILayout.ObjectField(Editing.UseFX, typeof(UnityEngine.VFX.VisualEffect)) as UnityEngine.VFX.VisualEffect;

                GUILayout.Label("Use Time", GUILayout.Width(70));

                Editing.UseFXLength = EditorGUILayout.FloatField(Editing.UseFXLength);

                //Editing.
                GUILayout.EndHorizontal();

                break;
        }


        GUILayout.EndVertical();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
