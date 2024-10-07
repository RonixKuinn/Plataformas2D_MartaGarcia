using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public AudioClip coinAudio;
    public AudioClip jumpAudio;

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

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    /*public void CoinSFX()
    {
        audioSource.PlayOneShot(coinAudio);
    }*/
}

//para usar los Managers hay que hacer un empty por cada uno y añadirle el script correspondiente