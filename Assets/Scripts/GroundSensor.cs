using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public static bool isGrounded;  // "static" es un añadido importante porque es como que te vincula con el otro script o no sé que putas :)
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6) //esto hay que hacerlo después de añadirle el lyer "ground" a las plataformas
        {
            isGrounded = true;
            PlayerControler.characterAnimator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
