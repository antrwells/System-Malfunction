using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEvent : ScriptableObject
{ 
    public enum EventType
    {
        PlaySound,ShowText,ClearText,StartEvent,StopEvent,PauseEvent,ResumeEvent,Wait,WaitForTag,AddMarkUp,ActivateUsable,StopSound,RemoveMark,
        WaitForTagRemove,StartConverse,Restart
    }

    public EventType EvType;

    public AudioSource Sound;
    public bool LoopSound = false;
    public string WaitTag = "";
    public float WaitTime = 0.0f;
    public bool ClearTag;
    public MarkUp Mark;
    public GameObject Target;
    public Usable Usable;
    public Converse Converse;
    public GameEvent Event;
    public string Text = "";
    public Vector2 Position2D;
    
    public void Clear()
    {

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
