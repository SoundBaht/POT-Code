using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSFX : MonoBehaviour
{
    public AudioSource sFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    public void HoverSound()
    {
        sFX.PlayOneShot(hoverSFX);
    }
    public void ClickSound()
    {
        sFX.PlayOneShot(clickSFX);
    }


}
