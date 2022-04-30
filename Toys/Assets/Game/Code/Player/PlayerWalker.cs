using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalker : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    public float WalkSpeed = 3f;
    public float RunSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    public void WalkTo(Vector3 pos)
    {
        agent.speed = WalkSpeed;
        agent.SetDestination(pos);
    }

    public void RunTo(Vector3 pos)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
