using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip hurtAudio;
    public AudioClip dieAudio;
    public AudioClip atackAudio;
    public AudioClip enemyAudio;
    public AudioClip pauseAudio;
    //public AudioClip runAudio;
    public AudioClip mimicAudio;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    /*public void CoinSFX()
    {
        audioSource.PlayOneShot(coinAudio);
    }*/
}

//para usar los Managers hay que hacer un empty por cada uno y añadirle el script correspondiente