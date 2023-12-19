using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    [SerializeField] public GameObject interactText;
    private Animator anim;

    void Start()
    {
        interactText.SetActive(false);
        anim = interactText.gameObject.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactText.activeSelf == false)
            {
                interactText.SetActive(true);
                anim.SetBool("Exit" , false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactText.activeSelf == true) {
                StartCoroutine(ExitWait());
            }
        }

        IEnumerator ExitWait() {
            anim.SetBool("Exit", true);
            yield return new WaitForSeconds(.5f);
            interactText.SetActive(false);
        }
    }
}
