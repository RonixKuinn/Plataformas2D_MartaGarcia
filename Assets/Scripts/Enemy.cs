using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]private int healtPoints = 3;

    void Awake()
    { 
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SoundManager.instance.PlaySFX(audioSource, SoundManager.instance.mimicAudio);
    }

    void Update()
    {
        
    }

    public void TakeDamage()
        {
            healtPoints--;
            

            if(healtPoints <= 0)
            {
                Destroy(gameObject);
                SoundManager.instance.PlaySFX(audioSource, SoundManager.instance.enemyAudio);
            }
            
        }
}
