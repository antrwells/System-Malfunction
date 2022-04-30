using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{

    public static EventSystem ES;

    public static List<string> EventTags = new List<string>();

    //Start Events
    public List<GameEvent> StartEvents = new List<GameEvent>();

    //Current Events
    public List<GameEvent> CurrentEvents = new List<GameEvent>();


    public static void NewEventTag(string tag)
    {
        EventTags.Add(tag);
    }

    public static void ClearTag(string tag)
    {
        EventTags.Remove(tag);
    }

    public static bool HasTag(string tag)
    {
        foreach(var t in EventTags)
        {
            if (t == tag) return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        ES = this;
        foreach(var ev in StartEvents)
        {
            StartEvent(ev);
        }
        StartEvents.Clear();
    }

    public static void AddEvent(GameEvent ev)
    {
        if (!ES.CurrentEvents.Contains(ev))
        {
            ES.CurrentEvents.Add(ev);
        }
    }

    public static void StopEvent(GameEvent ev)
    {
        if (ES.CurrentEvents.Contains(ev))
        {
            ES.CurrentEvents.Remove(ev);
        }
    }

    void StartEvent(GameEvent ev)
    {
        CurrentEvents.Add(ev);
       // Debug.Log("Started Event:" + ev.EventName);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(var ev in CurrentEvents)
        {
            ev.UpdateEvent();
      
        }

    }

    private void OnGUI()
    {
        foreach (var ev in CurrentEvents)
        {
            ev.renderGUI();
        }
    }
}
