using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private PlayerControler playerScript;
    private bool interactable;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable)
        {
            playerScript.AddHealth();
            //SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.coinAudio);
            Destroy(gameObject);
        }
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = true;
            playerScript = collider.gameObject.GetComponent<PlayerControler>();
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
