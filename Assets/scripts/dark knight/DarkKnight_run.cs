using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight_run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    public float attackRange;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //int choice = Random.Range(1, 3);
        //if (choice == 1) { player = GameObject.FindGameObjectWithTag("p1").transform; }
        //if(choice == 2) { player = GameObject.FindGameObjectWithTag("p2").transform; }
        player = GameObject.FindGameObjectWithTag("p1").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            rb.MovePosition(newPos);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
