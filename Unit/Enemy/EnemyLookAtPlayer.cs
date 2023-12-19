using Spine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using System;
using Random = UnityEngine.Random;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    private Rigidbody2D rb;
    public float dashingVelocity = 22f;
    [SerializeField] private Slider slider;
    private Animator animator;
    private Attack attack;
    public bool Condi1Check = false;
    public bool Condi2Check = false;
    public Transform backCheck;
    public LayerMask BackLayer;
    private MiniBScript miniBScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject miniboss = GameObject.Find("Mini Boss");
        GameObject palyer = GameObject.Find("Player");
        attack = player.GetComponentInChildren<Attack>();
        animator =miniboss.GetComponent<Animator>();
        miniBScript = GetComponent<MiniBScript>();

    }

    public bool isStagger
    {
        get
        {
            return animator.GetBool("Stagger");
        }
        set
        {
            animator.SetBool("Stagger", value);
        }
    }

    private void Update()
    {
        if (isStagger == true)
        {
            animator.SetBool("Stagger" , true);
        }
        else if (isStagger == false)
        {
            animator.SetBool("Stagger" , false);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

            slider.direction = Slider.Direction.LeftToRight;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;

            slider.direction = Slider.Direction.RightToLeft;
        }
    }

    public void CallDelay()
    {
        StartCoroutine(DelayDash());
    }


    IEnumerator DelayDash()
    {
        yield return new WaitForSeconds(1.2f);
        if (isFlipped)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingVelocity, 0f);
        }
        else if (!isFlipped)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashingVelocity, 0f);
        }
    }

    public IEnumerator DashBack()
    {
        yield return new WaitForSeconds(0.2f);
        if (isFlipped)
        {
            animator.SetBool("Stagger",false);
            animator.SetTrigger("Evac");
            rb.velocity = new Vector2(-transform.localScale.x * 15f, 7f);
        }
        else if (!isFlipped)
        {
            animator.SetBool("Stagger", false);
            animator.SetTrigger("Evac");
            rb.velocity = new Vector2(transform.localScale.x * 15f, 7f);
        }
    }
    public IEnumerator Condi1ATK()
    {
        Condi1Check = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        LookAtPlayer();
        animator.SetTrigger("Attack2");
        CallDelay();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isStagger = true;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DashBack());
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(1f);
        isStagger = false;
        Condi1Check = false;
        attack.isHit = false;

    }

    


    public IEnumerator Condi2ATK()
    {
        if (attack.isHit == true)
        {
            StopCoroutine(Condi2ATK());
        }
        Condi2Check = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        LookAtPlayer();
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        LookAtPlayer();
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isStagger = true;
        yield return new WaitForSeconds(1.5f);
        isStagger = false;
        StartCoroutine(DashBack());
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(1f);
        Condi2Check = false;
    }

    private float min = 90f;
    public bool condi3Playing = false;
    public IEnumerator Condi3ATK()
    {
        Condi1Check = false;
        Condi2Check = false;
        yield return new WaitForSeconds(min);
        condi3Playing = true;
        isStagger = false;
        animator.SetTrigger("Attack2");
        CallDelay();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isStagger = true;
        yield return new WaitForSeconds(5f);
        isStagger = false;
        animator.SetTrigger("Attack2");
        CallDelay();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isStagger = true;
        yield return new WaitForSeconds(5f);
        isStagger = false;
        animator.SetTrigger("Attack2");
        CallDelay();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isStagger = true;
        yield return new WaitForSeconds(8f);
        isStagger = false;

    }

    public bool IsWall()
    {
        return Physics2D.OverlapCircle(backCheck.position, 0.2f, BackLayer);
    }
}
