using System.Transactions;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;
using UnityEngine.InputSystem;

public class MiniBossScript : MonsterScript
{
    private Animator bossAnim;
    public Attack _attack;
    public PlayerScript playerScript;
    public HealthManager healthManager;
    public PlayerMovement playerMovement;
    private BoxCollider2D boss;
    private SkeletonMecanim skeletonMecanim;


    private void Start()
    {
        _currentHealth = _hp;
        bossAnim = GetComponent<Animator>();
        boss = GetComponent<BoxCollider2D>();
        skeletonMecanim = GetComponent<SkeletonMecanim>();
    }
    public override void TakeDamage(float _damage)
    {
        Debug.Log($"{this.name} was hit by {_damage} dmg");
        _currentHealth -= _damage;

        wasHitted = true;

        // enemyAnimator.SetTrigger("wasHitted"); 
        if (wasHitted && _currentHealth > 50)
        {
            StartCoroutine(Hurt());
        }

        else if (wasHitted && _currentHealth <= 50)
        {
            StartCoroutine(MoreHurt());
        }

        IEnumerator Hurt()
        {
            Color normalColor = skeletonMecanim.skeleton.GetColor();
            skeletonMecanim.skeleton.SetColor(Color.red);
            yield return new WaitForSeconds(.125f);
            skeletonMecanim.skeleton.SetColor(normalColor);
            wasHitted = false;
        }

        IEnumerator MoreHurt()
        {
            Color normalColor = skeletonMecanim.skeleton.GetColor();
            skeletonMecanim.skeleton.SetColor(Color.red);
            yield return new WaitForSeconds(.045f);
            skeletonMecanim.skeleton.SetColor(normalColor);
            yield return new WaitForSeconds(.045f);
            skeletonMecanim.skeleton.SetColor(Color.red);
            yield return new WaitForSeconds(.045f);
            skeletonMecanim.skeleton.SetColor(normalColor);
            wasHitted = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    { 
        if (coll.gameObject.CompareTag("Player"))
        {
            healthManager.TakeDamage(_atk);
            StartCoroutine(playerMovement.Knockback(0.002f, 0.2f, playerMovement.transform.position));
            _attack.isInvincible = true;
        }
    }

    public void Attack() {
        bossAnim.SetTrigger("attack");
    }

    public override void Die()
    {
        Debug.Log($"{this.name} Died!");
        StartCoroutine(Die());
        boss.isTrigger = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        IEnumerator Die() {
            bossAnim.SetBool("isAlive", false);
            yield return new WaitForSeconds(4f);
            Destroy(this.gameObject);
        }
    }

}
