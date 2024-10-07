using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool interactable;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable)
        {
            GameManager.instance.AddCoin();
            SoundManager.instance.PlaySFX(SoundManager.instance.coinAudio);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }

    
}