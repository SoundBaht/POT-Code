using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBossRoom : MonoBehaviour
{
    private GameObject Boss;
    private Animator anim;
    public GameObject LockOBJ;
    

    void Start()
    {
        Boss = GameObject.Find("Mini Boss");
        anim = Boss.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Boss.GetComponent<MiniBScript>()._currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (anim != null)
        {
            anim.SetBool("Idle" , false);
        }

        if (other.CompareTag("Player"))
        {
            LockOBJ.SetActive(true);
        }
    }
}
