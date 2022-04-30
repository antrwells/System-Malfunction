using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNPC : MonoBehaviour
{

    public string NPCName;
    public List<Converse> ConverseList = new List<Converse>();
    public int CurrentConverse = 0;
    private Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        //mAnimator.SetTrigger("Idle");

    }

    public void StartConverse()
    {
        if (CurrentConverse < ConverseList.Count)
        {
            Debug.Log("CurrentConverse:" + CurrentConverse);
            //ConverseSystem.SetConverse(ConverseList[CurrentConverse]);
            ConverseSys.CurrentConverse = ConverseList[CurrentConverse];
            Debug.Log("Done.");
            CurrentConverse++;
            Debug.Log("Started Conversation.");
            if (CurrentConverse == ConverseList.Count)
            {
                CurrentConverse = 0;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
