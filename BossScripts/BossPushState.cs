using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPushState : State
{
    [Header("State Ref")]
    private BossAttackState bossAttackState;
    private BossIdleState bossIdleState;
    private BossChaseState bossChaseState;


    [Header("Ref")]
    private GameObject Player;
    private EnemyLookAtPlayer lookAtPlayer;
    private Attack attack;

    [Header("Variable")]
    private Rigidbody2D rb;
    private Transform player;


    void Start()
    {
        bossIdleState = GetComponent<BossIdleState>();
        lookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        bossChaseState = GetComponent<BossChaseState>();

        Player = GameObject.Find("Player");
        attack = Player.GetComponentInChildren<Attack>();
    }

    void Update()
    {
        
    }

    public override State RunCurrentState()
    {
        lookAtPlayer.LookAtPlayer();
        lookAtPlayer.StartCoroutine(lookAtPlayer.Condi3ATK());

        if (attack.isHit == true)
        {
            return bossChaseState;
        }
        else
        {
            return this;
        }
    }
}
