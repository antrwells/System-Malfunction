using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Active = true;
    public GameEvent UseEvent = null;
    public string UseKey = "";
    public bool AlreadyUsed = false;
    public bool Reusable = false;
    public string UsableName = "";

    void Start()
    {
        
    }

    public virtual void Used()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public virtual void Use()
    {
        if (AlreadyUsed)
        {
        }
        else
        {
            EventSystem.AddEvent(UseEvent);
            if (Reusable == false)
            {
                AlreadyUsed = true;
            }
        }
    }

}
