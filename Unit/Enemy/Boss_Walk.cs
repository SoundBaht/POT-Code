using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using JetBrains.Annotations;
using Spine;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Boss_Walk : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3.7f;
    public float attackDashRange = 7f;
    private Transform player;
    private GameObject Player;
    private Rigidbody2D rb;
    private EnemyLookAtPlayer lookAtPlayer;
    private bool CheckBool;
    private Attack attack;
    [SerializeField] private float distanceToDetectPlayer = 50f;
    public bool isHit = false;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = GameObject.Find("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        lookAtPlayer = animator.GetComponent<EnemyLookAtPlayer>();
        CheckBool = animator.GetBool("isAlive");
        attack = Player.GetComponentInChildren<Attack>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector2.Distance(player.position, rb.position) <= distanceToDetectPlayer)
        {
            lookAtPlayer.LookAtPlayer();
            lookAtPlayer.StartCoroutine(lookAtPlayer.Condi3ATK());

            if (lookAtPlayer.IsWall() && lookAtPlayer.Condi1Check == false && lookAtPlayer.Condi2Check == false && lookAtPlayer.condi3Playing != true)
            {
                animator.SetTrigger("Attack2");
                lookAtPlayer.CallDelay();
                lookAtPlayer.LookAtPlayer();
            }
            if (CheckBool && !lookAtPlayer.IsWall() && lookAtPlayer.condi3Playing != true)
            {
                if (Vector2.Distance(player.position, rb.position) >= attackDashRange && lookAtPlayer.Condi1Check == false && lookAtPlayer.Condi2Check == false)
                {
                    animator.SetTrigger("Attack2");
                    lookAtPlayer.CallDelay();
                    lookAtPlayer.LookAtPlayer();
                    animator.SetBool("Idle", false);

                }
                else if (attack.isHit == true && lookAtPlayer.Condi2Check == false && lookAtPlayer.condi3Playing != true)
                {
                    
                    lookAtPlayer.LookAtPlayer();
                    Vector2 target = new Vector2(player.position.x, rb.position.y);
                    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                    rb.MovePosition(newPos);
                    if (Vector2.Distance(player.position, rb.position) <= attackRange)
                    {
                        lookAtPlayer.StartCoroutine(lookAtPlayer.Condi1ATK());
                    }
                }
                else if (attack.isHit == false && lookAtPlayer.Condi1Check == false && lookAtPlayer.condi3Playing != true)
                {
                    lookAtPlayer.LookAtPlayer();
                    Vector2 target = new Vector2(player.position.x, rb.position.y);
                    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                    rb.MovePosition(newPos);

                    if (attack.IsAlive && !attack.isInvincible)
                    {
                        if (Vector2.Distance(player.position, rb.position) <= attackRange)
                        {
                            lookAtPlayer.StartCoroutine(lookAtPlayer.Condi2ATK());



                        }
                    }
                }
                else
                {
                    animator.SetTrigger("Attack2");
                    lookAtPlayer.CallDelay();
                    lookAtPlayer.LookAtPlayer();
                }

            }
        }
         
    }

    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");

    }




    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
