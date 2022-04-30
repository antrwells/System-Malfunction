using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameEvent : MonoBehaviour
{

    public string EventName = "NewEvent";
    public List<SubEvent> SubEvents = new List<SubEvent>();
    SubEvent Cur;
    int CurEvent = -1;
    public bool EventsDone = false;
    float EventStart = 0.0f;
    public string ShowText = "";
    public Vector2 Position2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Clear()
    {

    }

    void Restart()
    {

        EventsDone = false;
        CurEvent = 0;
        Cur = SubEvents[CurEvent];
        EventStart = Time.time;

    }
    void NextEvent()
    {
        if (EventsDone) return;
        CurEvent++;
      
        if (CurEvent < SubEvents.Count)
        {
            Cur = SubEvents[CurEvent];
            EventStart = Time.time;
        }
        else
        {
            EventSystem.NewEventTag(EventName + "Done");
            EventsDone = true;
        }
    }

    public void UpdateEvent()
    {
        if (EventsDone) return;
     //   Debug.Log("Updating Event:" + EventName);
        if(CurEvent == -1)
        {
            NextEvent();
        }

        if (Cur != null)
        {

            switch (Cur.EvType)
            {
                case SubEvent.EventType.ClearText:

                    ShowText = "";
                    NextEvent();
                    break;                 
                        
                case SubEvent.EventType.ShowText:

                    ShowText = Cur.Text;
                    Position2D = Cur.Position2D;
                    NextEvent();

                    break;
                case SubEvent.EventType.StopEvent:

                    EventSystem.StopEvent(Cur.Event);
                    NextEvent();

                    break;
                case SubEvent.EventType.StartEvent:

                    EventSystem.AddEvent(Cur.Event);
                    NextEvent();
                    
                    break;
                case SubEvent.EventType.Restart:

                    Restart();

                    break;
                case SubEvent.EventType.StartConverse:

                    ConverseSys.CurrentConverse = Cur.Converse;
                    NextEvent();

                    break;
                case SubEvent.EventType.WaitForTagRemove:

                    if (EventSystem.HasTag(Cur.WaitTag) == false)
                    {

                        NextEvent();

                    }

                    break;

                case SubEvent.EventType.RemoveMark:


                    MarkUpSystem.RemoveMarkUp(Cur.Mark);
                    NextEvent();

                    break;

                case SubEvent.EventType.StopSound:

                    Cur.Sound.Stop();
                    NextEvent();

                    break;
                case SubEvent.EventType.ActivateUsable:

                    Cur.Usable.Active = true;
                    //                    Debug.Break();
                    NextEvent();
                    Debug.Log("ACtivated:" + Cur.Usable.name);


                    break;
                case SubEvent.EventType.AddMarkUp:

                    MarkUpSystem.AddMarkUp(Cur.Mark);
                    NextEvent();

                    break;
                case SubEvent.EventType.PlaySound:

                    var s = Cur.Sound;

                    s.loop = Cur.LoopSound;

                    s.Play();
                    NextEvent();

                    break;
                case SubEvent.EventType.WaitForTag:

                    if (EventSystem.HasTag(Cur.WaitTag))
                    {
                        if (Cur.ClearTag)
                        {
                            EventSystem.ClearTag(Cur.WaitTag);
                        }
                        NextEvent();
                    }

                    break;
                case SubEvent.EventType.Wait:

                    //Debug.LogError("Waiting for time.");
                    if(Time.time>(EventStart+Cur.WaitTime))
                    {

                        NextEvent();

                    }

                    break;
            }

        }

    }

    public void renderGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;

        if(ShowText!="")
        {
          
            GUI.Label(new Rect(Position2D.x, Position2D.y, 200, 200), ShowText,style);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
