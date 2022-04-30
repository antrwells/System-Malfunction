using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
public class ConvEditor : EditorWindow
{

    [SerializeField] TreeViewState m_TreeViewState;

    DialogTreeView m_TreeView;

    public static Converse Editing;
    public static ConverseItem ItemEdit;

    [MenuItem("Edge/Editors/Converse Editor")]
    public static void ShowWindow()
    {

        //EditorWindow.GetWindow(typeof(ConvEditor),true,"Conversation Editor");

        EditorWindow.GetWindowWithRect(typeof(ConvEditor), new Rect(40, 40, 700, 500), false, "Dialog Editor");


    }


    void OnEnable()
    {
        // Check whether there is already a serialized view state (state 
        // that survived assembly reloading)
        if (m_TreeViewState == null)
            m_TreeViewState = new TreeViewState();

        m_TreeView = new DialogTreeView(m_TreeViewState,Editing);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int editID = 0;

    public ConverseItem FindItem(ConverseItem root,int id)
    {
        if(root.treeID == id)
        {
            return root;
        }
        
        for(int i = 0; i < root.SubItems.Count; i++)
        {

            var rv = FindItem(root.SubItems[i], id);

            if (rv != null)
            {
                return rv;
            }

        }

        return null;


    }

    private void OnGUI()
    {
        if (Editing != null)
        {
            if (m_TreeView != null)
            {
                m_TreeView.OnGUI(new Rect(0, 0, 220, position.height));
            }
        }

     

        if (Editing != null)
        {
            if (m_TreeView != null)
            {
                if (m_TreeView.curID != editID)
                {

                    editID = m_TreeView.curID;
                    ItemEdit = FindItem(Editing.Root, editID);


                }
            }
        }


        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        GUILayout.Space(230);

        GUILayout.Label("Dialog", GUILayout.Width(100));;

        Converse prev = Editing;

        Editing = EditorGUILayout.ObjectField(Editing, typeof(Converse)) as Converse;

        if (Editing != prev)
        {
            if (Editing != null)
            {

                if(Editing.Root == null)
                {

                    var np = ScriptableObject.CreateInstance(typeof(ConverseItem)) as ConverseItem;

                    Editing.Root = np;

                }

                ItemEdit = Editing.Root;

            }
            m_TreeViewState = new TreeViewState();
            m_TreeView = new DialogTreeView(m_TreeViewState, Editing);

        }

        if (Editing == null)
        {

            GUILayout.EndHorizontal();
            //GUILayout.EndScrollView();
            return;

        }

        if (GUILayout.Button("Reset"))
        {
            Editing.Clear();
        }

        GUILayout.EndHorizontal();


        if (ItemEdit != null)
        {

       

            ConverseItem item = ItemEdit;

            // GUILayout.Space(230);
            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            GUILayout.Label("Persona", GUILayout.Width(60));
            item.Persona = EditorGUILayout.ObjectField(item.Persona, typeof(GamePersona)) as GamePersona;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            //GUILayout.Space(230);
            string prev_stext = item.ShortText;
            GUILayout.Label("Short", GUILayout.Width(60));
            item.ShortText = EditorGUILayout.TextField(item.ShortText);
            if(prev_stext != item.ShortText)
            {
                m_TreeView.Reload();
                int pid = m_TreeView.curID;
                //m_TreeViewState = new TreeViewState();
                //m_TreeView = new DialogTreeView(m_TreeViewState, Editing);
                //m_TreeView.curID = pid;
                //List<int> opid = new List<int>();
                //opid.Add(pid);
                //m_TreeView.SetSelection(opid);

            }
            GUILayout.EndHorizontal();

            //G//UILayout.Space(230);

            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            GUILayout.Label("Text");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(230);

            string prev_text = item.Text;
            item.Text = EditorGUILayout.TextArea(item.Text, GUILayout.Height(256));
            if(prev_text != item.Text)
            {
                m_TreeView.Reload();
                int pid = m_TreeView.curID;
                // Debug.Break();
            }
            
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.Space(230);            
            item.Speaker = EditorGUILayout.ObjectField(item.Speaker, typeof(GameObject)) as GameObject;

            item.TriggerAnim = EditorGUILayout.TextField("Trigger Anim", item.TriggerAnim);
            GUILayout.EndHorizontal();
            //GUILayout.Space(230);
            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            GUILayout.Label("Audio", GUILayout.Width(60));
            item.Audio = EditorGUILayout.ObjectField(item.Audio, typeof(AudioClip)) as AudioClip;
            GUILayout.EndHorizontal();
            //GUILayout.Space(230);
            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            GUILayout.Label("Time", GUILayout.Width(60));
            item.WaitTime = EditorGUILayout.FloatField(item.WaitTime);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(230);
            GUILayout.Label("Responses", GUILayout.Width(80));
            GUILayout.EndHorizontal();
            for (int i = 0; i < item.SubItems.Count; i++)
            {
                var sub = item.SubItems[i];
                GUILayout.BeginHorizontal();
                GUILayout.Space(230);
                if (GUILayout.Button("Edit Response:" + sub.ShortText))
                {

                    ItemEdit = sub;
                    List<int> pick = new List<int>();
                    pick.Add(sub.treeID);

                    m_TreeView.SetSelection(pick);

                }

                GUILayout.EndHorizontal();

            }

            GUILayout.BeginHorizontal();
            GUILayout.Space(230);

            if(GUILayout.Button("New Response"))
            {
                var np = ScriptableObject.CreateInstance(typeof(ConverseItem)) as ConverseItem;
                item.SubItems.Add(np);
                ItemEdit = np;
                m_TreeView.Reload();
                List<int> pick = new List<int>();
                pick.Add(np.treeID);

                m_TreeView.SetSelection(pick);
                int pid = m_TreeView.curID;

            }

            GUILayout.EndHorizontal();
            EditorUtility.SetDirty(ItemEdit);
        }

        GUILayout.EndVertical();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


class DialogTreeView : TreeView
{

    Converse m_dialog;
    public int curID = 0;

    public DialogTreeView(TreeViewState treeViewState,Converse dialog)
     : base(treeViewState)
    {
        m_dialog = dialog;
        Reload();
    }

    protected override void SelectionChanged(IList<int> selectedIds)
    {
        //base.SelectionChanged(selectedIds);
        curID = selectedIds[0];

    }

    protected override TreeViewItem BuildRoot()
    {

        var root = new TreeViewItem { id = 0, depth = -1, displayName = "Dialog" };


        if (m_dialog != null)
        {

            if (m_dialog != null)
            {

                AddItems(root, m_dialog.Root, 1);
            }


        }
        SetupDepthsFromParentsAndChildren(root);


        return root;
    }

    int AddItems(TreeViewItem item,ConverseItem ditem,int id)
    {

        if (ditem.ShortText == null) ditem.ShortText = "";
        if (ditem.Text == null) ditem.Text = "";

        string text = ditem.ShortText;



        if(text.Length<2)
        {
            text = ditem.Text;
        }

        if(text.Length<1)
        {
            text = "<response>";
        }

        var new_item = new TreeViewItem { id = id, displayName = text };

        ditem.treeID = id;

        item.AddChild(new_item);

        foreach(var sub in ditem.SubItems)
        {
            id++;
            id += AddItems(new_item, sub, id);

        }

        return id;

    }

}