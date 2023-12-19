using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderCoverRain : MonoBehaviour
{
    private AudioManager audiomanager;

    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            audiomanager.AmbientUnderCoverRain();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            audiomanager.AmbientSoundRain();
        }
    }
}
