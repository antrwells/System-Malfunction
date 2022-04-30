using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GotoSceneSignal : MonoBehaviour
{

    public string SceneName = "";

    public void GotoScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    // Start is
    //
    //
    // called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
