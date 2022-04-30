using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScene : MonoBehaviour
{


    public Camera BeginCamera;

    // Start is called before the first frame update
    void Start()
    {
        GameGlobals.CurrentCamera = BeginCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
