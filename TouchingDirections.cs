using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D CastFilter;
    private BoxCollider2D touchingCol;
    Animator animator;
    [SerializeField]
    private float groundDistance = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;

    public bool IsGround
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }



    void Start()
    {
        
    }


    void FixedUpdate()
    {
       IsGround = touchingCol.Cast(Vector2.down, CastFilter, groundHits, groundDistance) > 0;
    }
}
