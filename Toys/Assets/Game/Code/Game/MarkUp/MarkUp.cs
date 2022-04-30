using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkUp  : MonoBehaviour
{ 
    public string Text = "";
    public GameObject Target;
    public string EndTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.HasTag(EndTag))
        {
            EventSystem.ClearTag(EndTag);
        }
    }
}
