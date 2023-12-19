using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 0.5f;


    void Start()
    {
        Destroy(gameObject , DestroyTime);
    }
}
