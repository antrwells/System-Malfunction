using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EdgeLogos : MonoBehaviour
{

    public Texture2D Background = null;
    public List<Texture2D> Logos = new List<Texture2D>();
    int currentLogo = -1;
    public float Fade = 0.0f;
    public int fadeMode = 0;
    public float FadeInTime = 1.0f;
    public float StayTime = 3.0f;
    public float FadeOutTime = 1.0f;
    public Vector2 LogoPos = new Vector2();
    public Vector2 LogoSize = new Vector2();
    public AudioSource Audio;
    public string GotoScene;
    bool done = false;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void NextLogo()
    {
        currentLogo++;
        if (currentLogo >= Logos.Count)
        {
            done = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Audio.isPlaying == false)
        {
            SceneManager.LoadScene(GotoScene);
        }

        if(currentLogo == -1)
        {
            NextLogo();
        }

        switch (fadeMode)
        {
            case 0:

                Fade += Time.deltaTime / FadeInTime;
                if (Fade >= 1)
                {
                    fadeMode = 1;
                }

                break;
            case 1:

                StayTime -= Time.deltaTime / StayTime;
                if(StayTime<=0)
                {
                    fadeMode = 2;
                }

                break;
            case 2:

                Fade -= Time.deltaTime / FadeOutTime;

                if (Fade <= 0)
                {
                    NextLogo();
                    Fade = 0;
                    fadeMode = 0;
                    StayTime = 3.0f;
                }
                break;
        }

    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
        if (done) return;

        var logo = Logos[currentLogo];
        var logo_rect = new Rect(LogoPos.x * Screen.width, LogoPos.y * Screen.height, LogoSize.x * Screen.width, LogoSize.y * Screen.height);

        logo_rect.x -= (1.0f - Fade) * Screen.width * 0.1f;
        logo_rect.y -= (1.0f - Fade) * Screen.height * 0.1f;

        logo_rect.width += (1.0f - Fade) * Screen.width * 0.2f;
        logo_rect.height += (1.0f - Fade) * Screen.height * 0.2f;


        GUI.color = new Color(1, 1, 1, Fade);

        GUI.DrawTexture(logo_rect, logo, ScaleMode.StretchToFill);
    }

}
