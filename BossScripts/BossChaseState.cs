using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : State
{
    [Header("State Ref")] 
    private BossAttackState bossAttackState;
    private BossIdleState bossIdleState;


    [Header("Scripts Ref")] 
    private EnemyLookAtPlayer lookAtPlayer;


    [Header("Variable")]
    private Rigidbody2D rb;
    private Transform player;


    void Start()
    {
        bossIdleState = GetComponent<BossIdleState>();
        lookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override State RunCurrentState()
    {
        return bossIdleState;
    }
}
