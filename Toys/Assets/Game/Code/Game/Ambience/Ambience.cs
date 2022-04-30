using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{

    public AudioClip Music = null;
    public AudioClip ActionMusic = null;
    public AudioClip FearMusic = null;
    public AudioClip SuccessMusic = null;
    public AudioSource AudioSrc;
    public Vector2 ScreenPos;
    public string SceneName = "";
    public Font Font = null;
    bool LogoShown = false;
    public float showUntil = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (Music != null)
        {
            AudioSrc.clip = Music;
            AudioSrc.Play();
            showUntil = Time.time + 3.0f;
        }

        //float tt = Time.time;


    }

    // Update is called once per frame
    public void Clear()
    {
        Music = null;
        SceneName = "";
        ay = 90;

    }

    float fade = 0f;
    GUIStyle style = new GUIStyle();
    float ay = 50.0f;
    bool first = true;
    void OnGUI()
    {

        float sx = (int)((float)Screen.width * ScreenPos.x);
        float sy = (int)((float)Screen.height * ScreenPos.y);
        


        style.fontSize = 24;
        style.normal.textColor = new Color(1, 0.8f, 0.3f, 0.9f*fade);
        style.font = Font;

        if (Time.time < showUntil)
        {
            fade += (1.0f - fade) * 0.01f;
            sy = sy + ay * 0.6f;
            if (ay > 0)
            {
                ay = ay - 0.4f;
            }
            GUI.Label(new Rect(sx, sy, 290, 75), SceneName, style);

        }
        else
        {
            if (first)
            {
                first = false;
                ay = 0;
            }
            fade = fade * 0.97f;
            ay = ay + 1.0f;
            sy = sy + ay*0.4f;
            style.normal.textColor = new Color(1.0f, 0.8f, 0.3f, 0.9f*fade);
            GUI.Label(new Rect(sx, sy, 290, 75), SceneName, style);

        }

    }

    void Update()
    {
        
    }
}
