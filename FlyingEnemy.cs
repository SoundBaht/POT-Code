using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : StateMachineBehaviour
{
    public float speed = 4f;
    public float BackSpped = 5.5f;
    public float attackRange = 18f;
    private Transform player;
    private GameObject Player;
    private Rigidbody2D rb;
    private float distanceToDetectPlayer = 25f;
    private float playerIsTooClose = 10f;
    private EnemyFlyingLookAtPlayer enemyFlyingLook;
    public bool isGetingBack = false;
    public bool canShoot = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = GameObject.Find("Player");
        rb = animator.GetComponent<Rigidbody2D>();
        enemyFlyingLook = animator.GetComponent<EnemyFlyingLookAtPlayer>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= distanceToDetectPlayer)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

            if (Vector2.Distance(player.position, rb.position) >= attackRange)
            {
                rb.MovePosition(newPos);
            }

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                if (isGetingBack != true)
                {
                    enemyFlyingLook.FlyLookAtPlayer();
                }

                if (Vector2.Distance(player.position, rb.position) <= playerIsTooClose)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        isGetingBack = true;
                        canShoot = false;
                        enemyFlyingLook.FlyLookBack();
                    }
                    if (isGetingBack == true)
                    {
                        Vector2 newPos2 = Vector2.MoveTowards(rb.position, target, -BackSpped * Time.fixedDeltaTime);
                        rb.MovePosition(newPos2);

                    }
                }
                else if (Vector2.Distance(player.position, rb.position) > playerIsTooClose)
                {
                    isGetingBack = false;
                    canShoot = true;
                }
                else if (Vector2.Distance(rb.position, player.position) <= playerIsTooClose)
                {
                    isGetingBack = true;
                    canShoot = false;
                    enemyFlyingLook.FlyLookBack();
                }

                if (Vector2.Distance(player.position, rb.position) <= attackRange)
                {
                    if (isGetingBack != true)
                    {
                        enemyFlyingLook.FlyLookAtPlayer();
                        if (canShoot != false)
                        {
                            enemyFlyingLook.shoot();
                        }
                    }
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");

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
