using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("WalkSound")] 
    public List<AudioClip> dirtFS;
    public List<AudioClip> rockFS;
    public List<AudioClip> wetDirtFS;
    public List<AudioClip> woodFS;

    [SerializeField]
    public AudioClip Walk;


    enum FSMaterail
    {
        Dirt,Rock,WetDirt,Wood
    }

    private AudioSource footstepSource;

    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        footstepSource.pitch = Random.Range(0.8f,1f);
        footstepSource.PlayOneShot(Walk);
    }

}
