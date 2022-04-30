using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHud : MonoBehaviour
{

    public enum CursorMode
    {
        Normal,
        Attack,
        Move,
        Interact,
        None,
        PickUp,
        Examine,
        Door,Talk
    }

    public static bool UsingUI = false;

    public AdventureCharacter Player = null;

    public static CursorMode CursMode = CursorMode.Normal;
    public Font UIFont = null;
    public Texture2D CursorNormal = null;
    public Texture2D CursorPickUp = null;
    public Texture2D CursorTalk = null;

    public Vector2 InventoryPos = new Vector2();
    public Vector2 InventorySize = new Vector2();
    public Texture2D InventoryBox = null;
    public Vector2 LogPos = new Vector2();
    public Vector2 LogSize = new Vector2();
    public Texture2D LogBox = null;

    public static int SelectedItem = -1;
    public static GameItem PickUpItem;
    public static GameNPC TalkToNPC = null;
    public static List<string> Log = new List<string>();
    public static int LogY = 0;

    public static void LogMsg(string msg){
        Log.Add(msg);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        CursMode = CursorMode.Normal;

        if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
        {
            var item = hit.transform.GetComponent<GameItem>();

            //get distance between player and item
            if (item != null)
            {
                if (Vector3.Distance(transform.position, item.transform.position) < 1.5f)
                {
                    CursMode = CursorMode.PickUp;
                }

            }
        }

    }
    bool noCheck = false;
    public void DrawInventory(List<GameItem> inventory)
    {

        if (ConverseSys.CurrentConverse != null)
        {
            return;
        }
        UsingUI = false;

        var mouse_pos = Input.mousePosition;
        mouse_pos.y = Screen.height - mouse_pos.y;

        var inv_pos = new Vector2(InventoryPos.x * Screen.width, InventoryPos.y * Screen.height);
        var inv_size = new Vector2(InventorySize.x * Screen.width, InventorySize.y * Screen.height);

        GUI.color = new Color(1, 1, 1, 0.75f);

        GUI.DrawTexture(new Rect(inv_pos, inv_size), InventoryBox);

        
        

        float dx = inv_pos.x;
        float dy = inv_pos.y;

        float dw = 64;
        float dh = inv_size.y;

        if (Input.GetMouseButton(0)==false)
        {
            noCheck = false;
            
        }
       
        
        for (int i = 0; i < 24; i++)
        {
           // Debug.Log("item:" + i);
            GUI.color = new Color(1, 1, 1, 0.75f);
            GUI.DrawTexture(new Rect(dx, dy, 3, dh), InventoryBox);
            if(SelectedItem == i)
            {
                GUI.color = new Color(1, 0, 0, 1);
                GUI.DrawTexture(new Rect(dx, dy, dw, dh), InventoryBox);
            }
            
            if ((dx + dw) > (inv_pos.x + inv_size.x))
            {
                break;
            }
            if (mouse_pos.x > dx && mouse_pos.x < dx + dw && mouse_pos.y > dy && mouse_pos.y < dy + dh)
            {
                
                GUI.color = new Color(0, 1, 1, 1);
                GUI.DrawTexture(new Rect(dx, dy, dw, dh), InventoryBox);
                UsingUI = true;
                if (noCheck == false && Input.GetMouseButtonDown(0))
                {
                    if (SelectedItem == i)
                    {
                        SelectedItem = -1;
                    }
                    else
                    {
                        SelectedItem = i;
                    }
                        //  Debug.Log("Selected inventory item:"+i);
                    noCheck = true;
                  
                }
            }
            dx = dx + 64;
        }

        dx = inv_pos.x;

        //draw all items within inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.color = new Color(1, 1, 1, 0.8f);
            GUI.DrawTexture(new Rect(dx, dy, dw, dh), inventory[i].ItemView);
        }

    }

    public void DrawCursor()
    {

        Texture2D cursor = CursorNormal;

        float cw, ch;

        cw = Screen.width * 0.04f;
        ch = Screen.height * 0.06f;

        //draw cursor
        GUI.color = new Color(1, 1, 1, 0.65f);
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height-Input.mousePosition.y, cw, ch), cursor);


        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = new Color(1, 1, 1, 0.9f);
        style.font = UIFont;

        switch (CursMode)
        {
            case CursorMode.Interact:

                string id = "";

                if (SelectedItem != -1)
                {
                    id = Player.Inventory[SelectedItem].ItemName;
                }
                GUI.Label(new Rect(Input.mousePosition.x - cw, Screen.height - Input.mousePosition.y-ch, 64, 64), "Use "+id, style);

                break;
            case CursorMode.PickUp:

                GUI.Label(new Rect(Input.mousePosition.x-cw, Screen.height - Input.mousePosition.y-ch, 64, 64), "Pick Up "+PickUpItem.ItemName,style);

                break;
            case CursorMode.Talk:

                GUI.Label(new Rect(Input.mousePosition.x - cw, Screen.height - Input.mousePosition.y - ch, 64, 64), "Talk To " + TalkToNPC.NPCName, style);


                break;
            case CursorMode.Door:

                string door_state = "Close";

                if (Player.currentDoor.open == false)
                {
                    door_state = "Open";
                }
                

                GUI.Label(new Rect(Input.mousePosition.x - cw, Screen.height - Input.mousePosition.y - ch, 64, 64), door_state+" Door", style);

                break;
        }

    }
    
    private void OnGUI()
    {

        var log_rect = new Rect(LogPos.x * Screen.width, LogPos.y * Screen.height, LogSize.x * Screen.width, LogSize.y * Screen.height);


        //change inventory pos and size into screen space from screen ratio
        GUI.color = new Color(1, 1, 1, 0.75f);
        GUI.DrawTexture(log_rect, LogBox);

        var msg_rect = new Rect(LogPos.x * Screen.width, LogPos.y * Screen.height, LogSize.x * Screen.width, LogSize.y * Screen.height);

        msg_rect.x += Screen.width * 0.02f;
        msg_rect.y += Screen.height * 0.02f;

        int cur_y = 0;

        int max_line = 0;
        foreach (var log in Log)
        {
            if(cur_y<LogY)
            {
                cur_y++;
                continue;
            }
            GUI.Label(msg_rect, log);
            msg_rect.y += 25;
            if(msg_rect.y>=(log_rect.y+log_rect.height))
            {
                LogY++;
                  break;
            }
            cur_y++;
            max_line++;
        }
     

    }    
}
