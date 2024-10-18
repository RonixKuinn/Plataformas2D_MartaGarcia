using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private PlayerControler playerScript;
    private bool interactable;
    [SerializeField]private int health = 1;
    
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
            if(playerScript.currentHealth < playerScript.maxHealth)
            {
                //playerScript.AddHealth(health);
            }
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
