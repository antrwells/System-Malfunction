using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverseSys : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture2D WhiteTex = null;
    public Texture2D TextBox = null;
    public Texture2D ReplyBox = null;

    public static Converse CurrentConverse = null;
    public ConverseItem CurrentItem = null;

    public Vector2 TextBoxPosition;
    public Vector2 TextBoxSize;

    public Vector2 PersonaBoxPosition;
    public Vector2 PersonaBoxSize;

    public float ResponseHeight;

    public Font TextFont;
    public float TextSize;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(CurrentConverse != null)
        {
            if(CurrentItem == null)
            {
                CurrentItem = CurrentConverse.Root;
            }
        }



    }

    bool NoClick = false;

    ConverseItem PrevItem = null;
    private void OnGUI()
    {

        if(CurrentConverse == null)
        {
            return;
        }
        var mouse_pos = Input.mousePosition;

        mouse_pos.y = Screen.height - mouse_pos.y;

        bool click = Input.GetMouseButtonDown(0);

        if (NoClick)
        {
            if (click) return;
            NoClick = false;
        }


        // Generate real text box coords.

        float tb_x = (float)(Screen.width) * (float)(TextBoxPosition.x);
        float tb_y = (float)(Screen.height) * (float)(TextBoxPosition.y);

        float tb_w = (float)(Screen.width) * (float)(TextBoxSize.x);
        float tb_h = (float)(Screen.height) * (float)(TextBoxSize.y);

        // Generate real persona box coords.

        float pb_x = (float)(Screen.width) * (float)(PersonaBoxPosition.x);
        float pb_y = (float)(Screen.height) * (float)(PersonaBoxPosition.y);

        float pb_w = (float)(Screen.width) * (float)(PersonaBoxSize.x);
        float pb_h = (float)(Screen.height) * (float)(PersonaBoxSize.y);


        //Generate final rect for text box.
        
        Rect textbox_rect = new Rect(tb_x, tb_y, tb_w, tb_h);

        //Generate final rect for persona box.

        Rect personabox_rect = new Rect(pb_x, pb_y, pb_w, pb_h);


        int new_ts = (int)((float)Screen.height * 0.035f);

        TextSize = new_ts;

        GUIStyle text_style = new GUIStyle();
        text_style.font = TextFont;
        text_style.fontSize = (int)TextSize;
        text_style.normal.textColor = new Color(1, 1, 1, 0.8f);



     

  //      Rect text_rect = new Rect()

//        Rect current_rect = new Rect(Screen.width / 2 - 450, Screen.height -350, 900, 300);

        if(CurrentItem != null)
        {

            if (CurrentItem != PrevItem)
            {
            
                if (CurrentItem.Speaker != null)
                {
                    var anim = CurrentItem.Speaker.GetComponent<Animator>();
                    anim.SetTrigger(CurrentItem.TriggerAnim);
                }
            }
            PrevItem = CurrentItem;
            Color prev_col = GUI.color;
            GUI.color = new Color(1, 1, 1, 0.7f);
            GUI.DrawTexture(textbox_rect, TextBox);
        

            textbox_rect.x += 10;
            textbox_rect.y += 40;

            if (CurrentItem != null)
            {
                text_style.normal.textColor = new Color(1, 0.7f, 0.4f, 0.8f);
                GUI.Label(new Rect(tb_x + pb_w + 35, tb_y + 10, 200, 45), CurrentItem.Persona.NickName, text_style);
                text_style.normal.textColor = new Color(1, 1, 1, 0.8f); 
            }

            GUI.DrawTexture(new Rect(textbox_rect.x, textbox_rect.y, textbox_rect.width-10, 4), WhiteTex);

            textbox_rect.y += 10;

            GUI.Label(textbox_rect, CurrentItem.Text, text_style);

            personabox_rect.x -= 12;

            GUI.DrawTexture(personabox_rect, TextBox);
            personabox_rect.x += 4;
            personabox_rect.y += 4;
            personabox_rect.width -= 8;
            personabox_rect.height -= 8;
            GUI.DrawTexture(personabox_rect, CurrentItem.Persona.Avatar.texture);

            if (CurrentItem.SubItems.Count == 0)
            {
                int res_x = (int)tb_x;
                int res_y = (int)tb_y;

                res_y += (int)tb_h + 20;

                float res_height = ResponseHeight * Screen.height;


                GUI.DrawTexture(new Rect(res_x, res_y, tb_w, res_height), ReplyBox);

                if (mouse_pos.x >= res_x && mouse_pos.x <= (res_x + tb_w) && mouse_pos.y >= res_y && mouse_pos.y <= res_y + res_height)
                {
                    GUI.DrawTexture(new Rect(res_x, res_y, tb_w, res_height), ReplyBox);

                    if (click)
                    {

                        CurrentItem = null;
                        CurrentConverse = null;

                    }
                }

                int text_x, text_y;

                text_x = res_x + 15;
                text_y = res_y + 10;
                GUI.Label(new Rect(text_x, text_y, tb_w, res_height),"OK", text_style);
                res_y += (int)res_height + 10;

                /*
                if (GUI.Button(new Rect(res_x, res_y, tb_w, 35), "OK"))
                {

                    CurrentItem = null;
                    CurrentConverse = null;

                }
                */


            } else
            if(CurrentItem.SubItems.Count == 1)
            {


                int res_x = (int)tb_x;
                int res_y = (int)tb_y;

                res_y += (int)tb_h + 20;

                float res_height = ResponseHeight * Screen.height;

                GUI.DrawTexture(new Rect(res_x, res_y, tb_w, res_height), ReplyBox);

                if (mouse_pos.x >= res_x && mouse_pos.x <= (res_x + tb_w) && mouse_pos.y >= res_y && mouse_pos.y <= res_y + res_height)
                {
                    GUI.DrawTexture(new Rect(res_x, res_y, tb_w, res_height), ReplyBox);

                    if (click)
                    {

                        CurrentItem = CurrentItem.SubItems[0];
                        Debug.Log("Next::::");
                        Debug.Log("Next Item:" + CurrentItem.Text);
                    

                        NoClick = true;

                        return;

                    }
                }
                int text_x, text_y;

                text_x = res_x + 15;
                text_y = res_y + 10;
                GUI.Label(new Rect(text_x, text_y, tb_w, res_height), "Continue", text_style);
                res_y += (int)res_height + 10;



                /*
                 * if (GUI.Button(new Rect(res_x, res_y, tb_w, 35), "Continue"))
                {

                    

                }
                */


            }
            else
            {

                int margin = (int)((float)(Screen.width) * 0.02f);

                int item_count = CurrentItem.SubItems.Count;

                //Debug.LogError("SubCount:" + item_count);

                int res_x = (int)tb_x;
                int res_y = (int)tb_y;

                res_y += (int)tb_h + 20;

                float res_height = ResponseHeight * Screen.height;

                for(int i = 0; i < item_count; i++)
                {

                    var sub_item = CurrentItem.SubItems[i];

                    // Debug.LogError("X:" + res_x +" Y:" + res_y + " W:" + tb_w + " H:" + res_height);
                    GUI.color = new Color(33, 33, 33, 0.7f);
                    GUI.DrawTexture(new Rect(res_x+margin, res_y, tb_w-(margin*2),res_height),ReplyBox);

                    if(mouse_pos.x>=res_x && mouse_pos.x<=(res_x+tb_w) && mouse_pos.y>=res_y && mouse_pos.y<=res_y+res_height)
                    {
                        GUI.color = new Color(1, 1, 1, 0.7f);
                        GUI.DrawTexture(new Rect(res_x+margin, res_y, tb_w-(margin*2), res_height), ReplyBox);

                        if (click)
                        {
                            Debug.Log("CLICKED!!!");
                            CurrentItem = sub_item;
                            NoClick = true;
                          

                            return;

                        }
                    }

                    int text_x, text_y;

                    text_x = res_x + 15;
                    text_y = res_y + 10;
                    GUI.Label(new Rect(text_x+margin, text_y, tb_w, res_height), sub_item.ShortText, text_style);
                    res_y += (int)res_height + 10;

                }

            }

            GUI.color = prev_col;


        }

    }

}
