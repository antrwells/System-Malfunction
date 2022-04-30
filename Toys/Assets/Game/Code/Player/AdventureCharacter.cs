using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCharacter : MonoBehaviour
{
    // Start is called before the first frame update

    public enum CharacterState
    {
        idle,
        walk,
        run,
        attack,
        stagger,
        none
    }

    UnityEngine.AI.NavMeshAgent agent;

    public CharacterState currentState = CharacterState.none;
    public float WalkSpeed = 3f;
    public float RunSpeed = 5f;
    public GameDoor currentDoor = null;
    public PlayerWalker Walker;

    public NormalHud Hud = null;

    //inventory
    public List<GameItem> Inventory = new List<GameItem>();
    public List<GameItem> Backpack = new List<GameItem>();
    private Vector3 GotoLocation = Vector3.zero;
        
    
    public Transform TestSphere;

    private Animator mAnimator;

    

    bool pclick = false;
    float pclick_time = 0;
   
    void Start()
    {

        mAnimator = GetComponent<Animator>();
       

    }

    void ToIdle()
    {
        currentState = CharacterState.idle;
        mAnimator.SetTrigger("Idle");
    }

    void ToWalk(Vector3 pos)
    {

        if (currentState == CharacterState.idle || currentState == CharacterState.run) { 
        currentState = CharacterState.walk;
            mAnimator.SetTrigger("Walk");
        }
        GotoLocation = pos;
        Walker.WalkTo(pos);
        
    }

    void ToRun(Vector3 pos)
    {

        if (currentState == CharacterState.idle || currentState == CharacterState.walk)
        {
            currentState = CharacterState.run;
            mAnimator.SetTrigger("Run");
        }
      
        GotoLocation = pos;
        Walker.RunTo(pos);
    }

    void FacePoint(Vector3 point)
    {
        //Transform new_Trans = new Transform

        point.y = transform.position.y;

        var prev_rot = transform.rotation;
        transform.LookAt(point, Vector3.up);
        var new_rot = transform.rotation;

        var final_rot = Quaternion.Slerp(prev_rot, new_rot, 0.02f);
        transform.rotation = final_rot;

            
    }
    float pdif = 0;
    int ndif_c = 0;
    void RunToPoint(Vector3 point)
    {
        

        if (Vector3.Distance(transform.position, point) > 0.4f)
        {
            ndif_c = 0;
            pdif = Vector3.Distance(transform.position, point);
            
            FacePoint(point);
            /*
            //move towards facing
            transform.position += transform.forward * RunSpeed * Time.deltaTime;
            */

            //transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * WalkSpeed);
        }
        else
        {
            ndif_c = 0;
            ToIdle();
        }
    }

    void WalkToPoint(Vector3 point)
    {

     
        if (Vector3.Distance(transform.position, point) > 0.3f)
        {
            pdif = Vector3.Distance(transform.position, point);
            ndif_c = 0;
            FacePoint(point);
            //move towards facing
            //transform.position += transform.forward * WalkSpeed * Time.deltaTime;


            //transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * WalkSpeed);
        }
        else
        {
            ndif_c = 0;
            ToIdle();
        }


    }

    //update animation
    void UpdateAnimation()
    {

        //Debug.Break();
        switch(currentState)
        {
            case CharacterState.none:

                ToIdle();

                break;
            case CharacterState.walk:

             //   FacePoint(GotoLocation);
                WalkToPoint(GotoLocation);
                //transform.position = Walker.transform.position;
                
                break;
            case CharacterState.run:

                RunToPoint(GotoLocation);
               // transform.position = Walker.transform.position;
                break;
        }

    }    


    void WalkToMouse()
    {
        RaycastHit hit;
        if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
        {

            //if double click


            //if far away enough
            if (Vector3.Distance(transform.position, hit.point) > 0.5f)
            {
                ToWalk(hit.point);
            }

            //ToWalk(hit.point);
            //TestSphere.position = hit.point;

        }

    }

    void RunToMouse()
    {
        RaycastHit hit;
        if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
        {

            //if double click


            //if far away enough
            if (Vector3.Distance(transform.position, hit.point) > 0.5f)
            {
                ToRun(hit.point);
            }

            //ToWalk(hit.point);
            //TestSphere.position = hit.point;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ConverseSys.CurrentConverse != null)
        {
            return;
        }
        /*        if (Vector3.Distance(agent.destination,transform.position)<1.5f){
        
            if (this.currentState != CharacterState.idle)
            {
                Debug.Log("Stopped forced");
                ToIdle();
            }                
        }
        */

        RaycastHit hit;

        NormalHud.CursMode = NormalHud.CursorMode.Normal;

        if (NormalHud.UsingUI == false)
        {


            if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
            {

                //check if close enough

                if (Vector3.Distance(transform.position, hit.point) < 3f)
                {

                    var npc = hit.transform.GetComponent<GameNPC>();

                    if (npc != null)
                    {
                        NormalHud.TalkToNPC = npc;
                        NormalHud.CursMode = NormalHud.CursorMode.Talk;
                        
                    }

                    var usable = hit.transform.GetComponent<Usable>();

                    if (usable != null)
                    {
                        NormalHud.CursMode = NormalHud.CursorMode.Interact;
                    }

                    var door = hit.transform.GetComponent<GameDoor>();
                    if (door != null)
                    {
                        NormalHud.CursMode = NormalHud.CursorMode.Door;
                        currentDoor = door;
                    }
                    else
                    {
                        currentDoor = null;
                    }

                }
            }

            if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
            {
                var item = hit.transform.GetComponent<GameItem>();

                //get distance between player and item
                if (item != null)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) < 1.5f)
                    {
                        //pick up item
                        // item.PickUp(this);
                        NormalHud.CursMode = NormalHud.CursorMode.PickUp;
                        NormalHud.PickUpItem = item;
                    }

                }
            }
                    if (Input.GetMouseButtonDown(1))
            {
                if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
                {

                    //check if close enough

                    if (Vector3.Distance(transform.position, hit.point) < 3f)
                    {

                        var npc = hit.transform.GetComponent<GameNPC>();

                        //talk to
                        if (npc != null)
                        {
                            npc.StartConverse();
                            Debug.Log("Started Converse(AC)");
                        }
                        var door = hit.transform.GetComponent<GameDoor>();
                        if (door != null)
                        {
                            door.Use();

                            if (door.open)
                            {
                                NormalHud.LogMsg("Opened Door.");
                            }
                            else
                            {
                                NormalHud.LogMsg("Closed Door.");
                            }
                        }
                        else
                        {
                         
                        }

                        var usable = hit.transform.GetComponent<Usable>();

                        if (usable != null)
                        {



                            if (NormalHud.SelectedItem != -1)
                            {
                                var cur_item = Inventory[NormalHud.SelectedItem];

                                if (usable.UseKey == cur_item.KeyID)
                                {
                                    if (usable.AlreadyUsed == false)
                                    {
                                        NormalHud.LogMsg("Used " + usable.UsableName);
                                    }
                                    else
                                    {
                                        NormalHud.LogMsg("Already used " + usable.UsableName);
                                    }
                                    usable.Use();
                                   
                                }
                                else
                                {
                                    NormalHud.LogMsg("This is not usable on this object." + usable.UsableName);
                                }
                            }
                            else
                            {

                                if (usable.UseKey == "")
                                {
                                    usable.Use();
                                    NormalHud.LogMsg("Used " + usable.UsableName);
                                }
                                else
                                {
                                    NormalHud.LogMsg(usable.UsableName+" This requires another object");
                                }
                            }

                        }
                    }
                }


                var item = hit.transform.GetComponent<GameItem>();

                //get distance between player and item
                if (item != null)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) < 1.5f)
                    {
                        //pick up item
                        // item.PickUp(this);
                      //  NormalHud.CursMode = NormalHud.CursorMode.PickUp;
                      
                        if (Input.GetMouseButtonDown(1))
                        {
                            Inventory.Add(item);
                            item.gameObject.SetActive(false);
                            NormalHud.LogMsg("Picked up " + item.ItemName);
                            return;
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    //walk to item
                    // ToWalk(hit.point);
                }

                if (item != null)
                {

                    Debug.Log("You clicked on an item.");

                }

            }


            if (Input.GetMouseButtonDown(0))
            {
                if (pclick)
                {
                    if (Time.time < (pclick_time + 0.3f))
                    {
                        RunToMouse();
                        Debug.Log("Running!");
                    }
                    pclick = false;

                }
                else
                {
                    pclick = true;
                    pclick_time = Time.time;
                    WalkToMouse();

                }
            }
            else
            {
                if (pclick)
                {
                    if (Time.time > pclick_time + 0.3f)
                    {
                        pclick = false;
                    }
                }
            }

        }

        //}

        UpdateAnimation();

        return;

    }

    private void OnGUI()
    {
        Hud.DrawInventory(Inventory);
        Hud.DrawCursor();
    }

}
