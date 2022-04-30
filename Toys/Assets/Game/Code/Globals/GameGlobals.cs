using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobals : MonoBehaviour
{


    public static Camera CurrentCamera;

    // Start is
    //   called before the first frame update

    public static void SetCamera(Camera cam)
    {
        CurrentCamera.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
      
        CurrentCamera = cam;
        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
