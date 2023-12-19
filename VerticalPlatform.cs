using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _palyerOnPlatform;


    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_palyerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatForm(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            _palyerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatForm(collision ,true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatForm(collision ,true);
    }
    
}
