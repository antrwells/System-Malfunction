using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{

    public Camera ZoneCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //on enter trigger
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            GameGlobals.SetCamera(ZoneCamera);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
