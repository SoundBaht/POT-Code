using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BossIdleState : State
{
    [Header("State Reference")] 
    private BossPushState bossPushState;
    private BossChaseState bossChaseState;


    [Header("Scripts Reference")] 
    private EnemyLookAtPlayer enemyLookAtPlayer;


    [Header("Reference")] 
    private Rigidbody2D rb;
    private Transform player;


    [Header("Variable")] 
    private float distanceToDetectPlayer = 50f;
    public bool canDetect = false;



    void Start()
    {
        //========== Reference ==========
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;


        //========== Ref Scripts==========
        enemyLookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        bossChaseState = GetComponent<BossChaseState>();
        bossPushState = GetComponent<BossPushState>();



    }

    void Update()
    {
        
    }

    public override State RunCurrentState()
    {
        float DistancePlayer = Vector2.Distance(player.position, rb.position);

        if (DistancePlayer <= distanceToDetectPlayer)
        {
            canDetect = true;
        }
        else
        {
            canDetect = false;
        }

        if (canDetect)
        {
            return bossPushState;
        }
        else
        {
            return this;
        }

    }
}
