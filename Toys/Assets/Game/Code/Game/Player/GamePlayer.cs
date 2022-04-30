using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{

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

    public GameObject Sphere;

    private Vector3 GotoLocation = Vector3.zero;

    private Animator mAnimator;




    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (MouseFuncs.MouseClick(GameGlobals.CurrentCamera, out hit))
        {
            Sphere.transform.position = hit.point;
        }
    }
}
