using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool MouseClick(Camera camera,out RaycastHit hit)
    {

     
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
           // Debug.Log("MouseClick hit geo.");
            return true;
            // Do something with the object that was hit by the raycast.
        }

        return false;
        
    }

}
