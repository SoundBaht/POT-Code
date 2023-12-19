using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Vector2 nextPosition;
    public Animator cameraTransitionAnim;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            other.transform.position = nextPosition;
            cameraTransitionAnim.SetTrigger("CamFaded");
        }
    }
}
