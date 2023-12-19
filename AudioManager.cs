using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    [Header("========== Audio Source ==========")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource ambientsound;
    [SerializeField] public AudioSource SFXSource;
    


    [Header("========== Audio Clip ==========")]
    public AudioClip backgroundMusic;
    public AudioClip AmbientSound;
    public AudioClip AmbientSoundUndercoverRain;
    public AudioClip AmbientSoundInCave;
    public AudioClip Walk1;
    public AudioClip Walk2;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip GotHit;
    public AudioClip MonsterDead;
    public AudioClip playerDied;
    public AudioClip Healing;
    public AudioClip DummyHit;
    public AudioClip StatueActivated;
    public AudioClip RedSFX;
    public AudioClip GreenSFX;
    public AudioClip bossRoomSong;
    public AudioClip FlyingShootSFX;

    public bool isBossRoom = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("music")) {
            musicSource.volume = PlayerPrefs.GetFloat("music");
        }
        if (PlayerPrefs.HasKey("ambient")) {
            musicSource.volume = PlayerPrefs.GetFloat("ambient");
        }
        if (PlayerPrefs.HasKey("sfx")) {
            musicSource.volume = PlayerPrefs.GetFloat("sfx");
        }

        AmbientSoundRain();
        BGMInGame();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        SFXSource.pitch = Random.Range(1f, 1.2f);
    }

    public void AmbientSoundRain()
    {
        ambientsound.clip = AmbientSound;
        ambientsound.Play();
        ambientsound.loop = true;
    }

    public void AmbientUnderCoverRain() 
    {
        ambientsound.clip = AmbientSoundUndercoverRain;
        ambientsound.Play();
        ambientsound.loop = true;

    }

    public void InCave()
    {
        ambientsound.clip = AmbientSoundInCave;
        ambientsound.Play();
        ambientsound.loop = true;
    }

    public void BGMInGame()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void BGMInGameStop()
    {
        musicSource.Stop();
    }

    public void BGMInBossRoom()
    {
        musicSource.clip = bossRoomSong;
        musicSource.Play();
        musicSource.loop = true;

    }

    public void HealingVFX(AudioClip clip)
    { 
        SFXSource.PlayOneShot(clip);
    }
    
    public void DiedVFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
