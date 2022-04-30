using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverseItem : ScriptableObject
{
    // Start is called before the first frame update
    public string Text;
    public string ShortText;
    public AudioClip Audio;
    public GamePersona Persona;
    public float WaitTime = 3.0f;
    public string AddTag="", StopTag="";
    public int treeID = 0;
    public GameObject Speaker = null;
    public string TriggerAnim = "";
    public List<ConverseItem> SubItems = new List<ConverseItem>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
